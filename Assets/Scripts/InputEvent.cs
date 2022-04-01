using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvent
{
    private Tank _selectedTank;
    private GameParameters _parameters;
    private Team _playerTeam;

    private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public InputEvent(GameParameters parameters, Team playerTeam)
    {
        _parameters = parameters;
        _playerTeam = playerTeam;
    }

    public void OnRightClick()
    {
        Debug.Log("click");
        RaycastHit2D hit = Physics2D.Raycast(MousePosition, Vector3.forward);
        if (hit.collider == null) { return; }
        Debug.Log("found : " + hit.collider.name) ;

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
        Tank tank = hit.collider.gameObject.GetComponent<Tank>();
        if (tank.Team != _playerTeam) { return; }
        _selectedTank = tank;
    }

    private void UnSelectTank()
    {
        _selectedTank = null;
    }

    private void Action(RaycastHit2D hit)
    {
        if (hit.collider.CompareTag(_parameters.TagTank))
        {
            TankActions.Target.Execute(_selectedTank, hit.collider.gameObject.transform);
        } else
        {
            TankActions.GoTo.Execute(_selectedTank, (Vector3)MousePosition);
        }
    }
}
