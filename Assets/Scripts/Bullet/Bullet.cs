using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameParameters parameters;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask layerTank;

    private float _timer;

    private Vector3 _originPosition;
    private Collider2D _myTank;
    private Transform _transform;

    private bool _isExploding;
    
    private static readonly int Explosion1 = Animator.StringToHash("Explosion");

    public void Init(Vector2 originPosition, Vector2 impactPosition)
    {
        _originPosition = originPosition;

        _timer = parameters.TankShellDuration;
        _transform = transform;
        
        SetImpactPoint(impactPosition);
    }

    public void SetTank(Collider2D other)
    {
        _myTank = other;
    }

    private void SetImpactPoint(Vector2 impactPosition)
    {
        var distance = Vector2.Distance(_originPosition, impactPosition);

        var timer = distance / parameters.TankShellSpeed;
        if (timer < _timer) _timer = timer;
    }

    private void Update()
    {
        if(_isExploding) { return; }

        _transform.position += _transform.up * (Time.deltaTime * parameters.TankShellSpeed);
        
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == _myTank) return;

        if (!other.CompareTag(parameters.TagTank)) return;
        
        Explode();
    }

    private void Explode()
    {
        _isExploding = true;
        
        var hits = Physics2D.OverlapCircleAll(transform.position, parameters.TankShellDamageFalloff, layerTank.value);
        foreach (var hit in hits)
        {
            if(hit == _myTank) { continue; }
            var damage = (1 - (Vector3.Distance(hit.transform.position, transform.position) / parameters.TankShellDamageFalloff)) * parameters.TankShellDamage;
            damage = Mathf.Clamp(damage, 0, Mathf.Infinity);
            hit.GetComponentInChildren<LifeBar>().TakeDamage(damage);
        }
        
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        animator.SetTrigger(Explosion1);
        yield return new WaitForSeconds(0.333f);
        Destroy(gameObject);
    }
}
