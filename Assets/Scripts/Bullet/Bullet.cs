using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 OriginPosition;
    [SerializeField] private GameParameters _gameParameters;
    private float Range = 3f;

    public LayerMask LayerTank;
    // Start is called before the first frame update
    void Start()
    {
        OriginPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * _gameParameters.TankShellSpeed;
        if (Vector3.Distance(transform.position, OriginPosition) > Range)
        {
            Explode();
        }
    }

    public void Explode()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _gameParameters.TankShellDamageFalloff, LayerTank.value);

        for (var i = 0; i < hitColliders.Length; i++)
        {
            float damage = (Vector3.Distance(hitColliders[i].transform.position, transform.position) / _gameParameters.TankShellDamageFalloff) * _gameParameters.TankShellDamage;
            Debug.Log($"to : {hitColliders[i].name}, Damage : {damage}");
        }
        Destroy(gameObject);
    }
}
