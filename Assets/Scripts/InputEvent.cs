using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvent
{
    private Tank _selectedTank;
    private GameParameters _parameters;

    private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public InputEvent(GameParameters parameters)
    {
        _parameters = parameters;
    }

    public void OnRightClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(MousePosition, Vector3.forward);
        if (hit.collider == null) { return; }

        if (_selectedTank) { 
            Action(hit);
            return;
        }
        SelectedTank(hit);
    }

    public void OnLeftClick()
    {
        UnSelectTank();
    }

    private void SelectedTank(RaycastHit2D hit)
    {
        if (!hit.collider.CompareTag(_parameters.TagTank)) { return; }
        Debug.Log("Select");
        _selectedTank = hit.collider.gameObject.GetComponent<Tank>();
    }

    private void UnSelectTank()
    {
        _selectedTank = null;
    }

    private void Action(RaycastHit2D hit)
    {
        if (hit.collider.CompareTag(_parameters.TagTank)) {
            // _selectedTank.Attack(hit.collider.gameObject); 
        } else
        {
            Debug.Log("Move");
            _selectedTank.GoTo(MousePosition); 
        }
        UnSelectTank();
    }
}
