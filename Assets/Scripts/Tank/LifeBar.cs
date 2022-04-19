using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    public GameParameters parameters;
    private Tank _myTank;
    public float LifePoints { get; private set; }
    private bool isAlive => LifePoints > 0;
   
    SpriteRenderer spriteRenderer;
    Camera mainCamera;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        _myTank = GetComponentInParent<Tank>();
        LifePoints = parameters.TankHealth;
    }

    public void TakeDamage(float damage)
    {
        LifePoints -= damage;
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.position = _myTank.transform.position + new Vector3(0, 2, 0);
        UpdateParams();
        if (isAlive) { return; }
        StartCoroutine(_myTank.TankDeath());
    }

    private void UpdateParams()
    {
        float fill = LifePoints / parameters.TankHealth;
        spriteRenderer.material.SetFloat("_Fill", fill);
    }

}
