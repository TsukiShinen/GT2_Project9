using System.Collections.Generic;
using UnityEngine;

public class InputEvent
{
    private readonly ICollection<Tank> _selectedTanks;
    private readonly GameParameters _parameters;
    private readonly Team _playerTeam;
    private bool _drag = false;

    private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    private bool HasTanksSelected => _selectedTanks.Count > 0;

    public InputEvent(GameParameters parameters, Team playerTeam)
    {
        _parameters = parameters;
        _playerTeam = playerTeam;
        _selectedTanks = new List<Tank>();
    }

    public void OnRightClick()
    {
        var hit = Physics2D.Raycast(MousePosition, Vector3.forward);
        if (hit.collider == null) { return; }

        if (!HasTanksSelected) { return; }

        Action(hit);
    }

    public void OnLeftClick()
    {
        RaycastHit2D hit = Physics2D.Raycast(MousePosition, Vector3.forward);
        if (hit.collider == null) { return; }

        if (!HasTanksSelected)
            SelectedTank(hit);
        else 
            UnSelectTank();
        _drag = true;
    }

    public void OnLeftClickRelease()
    {
        _drag = false;
    }

    private void SelectedTank(RaycastHit2D hit)
    {
        if (!hit.collider.CompareTag(_parameters.TagTank)) { return; }
        var tank = hit.collider.gameObject.GetComponent<Tank>();
        if (tank.Team != _playerTeam) { return; }
        _selectedTanks.Add(tank);
    }

    private void UnSelectTank()
    {
        _selectedTanks.Clear();
    }

    private void Action(RaycastHit2D hit)
    {
        foreach (var tank in _selectedTanks)
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
