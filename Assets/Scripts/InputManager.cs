using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputEvent _inputEvent;
    [SerializeField] private GameParameters _parameters;

    private void Awake()
    {
        _inputEvent = new InputEvent(_parameters);
    }

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
