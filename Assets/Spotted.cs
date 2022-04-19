using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotted : MonoBehaviour
{
    public GameObject Graphics;
    
    public void SpriteOn()
    {
        Graphics.SetActive(true);
    }

    public void SpriteOff()
    {
        Graphics.SetActive(false);
    }
}
