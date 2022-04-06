using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 OriginPosition;
    [SerializeField] private GameParameters _gameParameters;
    [SerializeField] private Animator _explosion;
    private float Range = 8f;
    public Collider2D _myTank;
    public LayerMask LayerTank;
    private bool isExploding = false;

    void Start()
    {
        OriginPosition = transform.position;
    }

    public void SetTank(Collider2D collider)
    {
        _myTank = collider;
    }

    void Update()
    {
        if(isExploding) { return; }
        transform.position += transform.up * Time.deltaTime * _gameParameters.TankShellSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider != _myTank)
        {
            isExploding = true;
            Explode();
        }
    }

    public void Explode()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _gameParameters.TankShellDamageFalloff, LayerTank.value);

        for (var i = 0; i < hitColliders.Length; i++)
        {
            float damage = (1 - (Vector3.Distance(hitColliders[i].transform.position, transform.position) / _gameParameters.TankShellDamageFalloff)) * _gameParameters.TankShellDamage;
            damage = Mathf.Clamp(damage, 0, Mathf.Infinity);
            Debug.Log($"to : {hitColliders[i].name}, Damage : {damage}");
        }
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        _explosion.SetTrigger("Explosion");
        yield return new WaitForSeconds(0.333f);
        Destroy(gameObject);
    }
}
