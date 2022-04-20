using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotted : MonoBehaviour
{
    public SpriteRenderer[] Graphics;
    
    public void SpriteOn()
    {
        foreach(var sprite in Graphics)
        {
            sprite.enabled = true;
        }
    }

    public void SpriteOff()
    {
        foreach (var sprite in Graphics)
        {
            sprite.enabled = false;
        }
    }
}
