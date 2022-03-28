using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputEvent _inputEvent;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _inputEvent.OnLeftClick();
        }
        if (Input.GetMouseButtonDown(1))
        {
            _inputEvent.OnRightClick();
        }
    }
}
