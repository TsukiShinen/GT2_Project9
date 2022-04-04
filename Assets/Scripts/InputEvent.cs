using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEvent
{
    private ICollection<Tank> _selectedTanks;
    private GameParameters _parameters;
    private Team _playerTeam;

    private Vector3 _mousePosStart;

    private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    private bool hasTanksSelected => _selectedTanks.Count > 0;

    public InputEvent(GameParameters parameters, Team playerTeam)
    {
        _parameters = parameters;
        _playerTeam = playerTeam;
        _selectedTanks = new List<Tank>();
    }

    public void OnRightClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(MousePosition, Vector3.forward);
        if (hit.collider == null) { return; }

        if (!hasTanksSelected) { return; }

        Action(hit);
    }

    public void OnLeftClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(MousePosition, Vector3.forward);
        if (hit.collider == null) { return; }

        if (!hasTanksSelected)
            SelectedTank(hit);
        else 
            UnSelectTank();
    }

    public void OnLeftClickStay()
    {

    }

    public void OnLeftClickRelease()
    {

    }

    private void SelectedTank(RaycastHit2D hit)
    {
        if (!hit.collider.CompareTag(_parameters.TagTank)) { return; }
        Tank tank = hit.collider.gameObject.GetComponent<Tank>();
        if (tank.Team != _playerTeam) { return; }
        _selectedTanks.Add(tank);
    }

    private void UnSelectTank()
    {
        _selectedTanks.Clear();
    }

    private void Action(RaycastHit2D hit)
    {
        foreach (Tank tank in _selectedTanks)
        {
            if (hit.collider.CompareTag(_parameters.TagTank))
            {
                if (tank.Team == _playerTeam) { return; }
                TankActions.Target.Execute(tank, hit.collider.gameObject.transform);
            }
            else
            {
                TankActions.GoTo.Execute(tank, (Vector3)MousePosition);
            }
        }
    }
}
