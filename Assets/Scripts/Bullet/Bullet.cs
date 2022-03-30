using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 OriginPosition;
    [SerializeField] private GameParameters _gameParameters;
    private float Range = 3f;
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
            Destroy(gameObject);
        }
    }
}
