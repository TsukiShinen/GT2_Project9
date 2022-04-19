using UnityEngine;

public class WhatIsGround : MonoBehaviour
{
    [SerializeField] private GameParameters parameters;

    private Transform _transform;
    
    public string GroundTag { get; private set; }

    private void Awake()
    {
        _transform = transform;
    }

    void FixedUpdate()
    {
        var hits = Physics2D.RaycastAll(_transform.position, _transform.forward);
        foreach (var item in hits)
        {
            if (item.collider.CompareTag(parameters.TagTank)) { continue; }
            GroundTag = item.collider.tag;
            return;
        }
    }
}
