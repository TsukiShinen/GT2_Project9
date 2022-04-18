using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameParameters parameters;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask layerTank;

    private float _timer;

    private Vector3 _originPosition;
    private Collider2D _myTank;

    private bool _isExploding = false;

    public void Init(Vector2 originPosition, Vector2 impactPosition)
    {
        _originPosition = originPosition;

        _timer = parameters.TankShellDuration;

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

        transform.position += transform.up * Time.deltaTime * parameters.TankShellSpeed;
        
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == _myTank) return;
        
        Explode();
    }

    private void Explode()
    {
        _isExploding = true;
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, parameters.TankShellDamageFalloff, layerTank.value);
        foreach (var hit in hits)
        {
            if(hit == _myTank) { continue; }
            var damage = (1 - (Vector3.Distance(hit.transform.position, transform.position) / parameters.TankShellDamageFalloff)) * parameters.TankShellDamage;
            damage = Mathf.Clamp(damage, 0, Mathf.Infinity);
            hit.GetComponent<Tank>().LifePoints -= damage;
            Debug.Log(hit.name + hit.GetComponent<Tank>().LifePoints);
        }
        
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        animator.SetTrigger("Explosion");
        yield return new WaitForSeconds(0.333f);
        Destroy(gameObject);
    }
}
