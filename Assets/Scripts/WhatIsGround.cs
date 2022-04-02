using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatIsGround : MonoBehaviour
{
    [SerializeField] private GameParameters _parameters;

    public string GroundTag { get; private set; }

    void FixedUpdate()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.forward);
        foreach (RaycastHit2D item in hits)
        {
            if (item.collider.CompareTag(_parameters.TagTank)) { continue; }
            GroundTag = item.collider.tag;
            return;
        }
    }
}
