using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatIsGround : MonoBehaviour
{
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward);    
        Debug.Log(hit.collider.name);
    }
}
