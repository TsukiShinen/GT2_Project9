using System.Collections.Generic;
using UnityEngine;

public class InputEvent
{
    private readonly ICollection<Tank> _selectedTanks;
    private readonly GameParameters _parameters;
    private readonly Team _playerTeam;
    
    
    private bool _drag = false;
    private Vector2 _startingMousePosition;
    private Rect _rect;
    
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
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            UnSelectTank();
        }
        var hit = Physics2D.Raycast(MousePosition, Vector3.forward);
        if (hit.collider == null) { return; }

        SelectedTank(hit);
        
        _drag = true;
        _startingMousePosition = Input.mousePosition;
    }

    public void OnLeftClickRelease()
    {
        _drag = false;

        var hits = Physics2D.BoxCastAll(Camera.main.ScreenToWorldPoint(_rect.position), _rect.size, 0f, Vector3.forward);
        foreach (var hit in hits)
        {
            if (!hit.collider.CompareTag(_parameters.TagTank)) { continue; }
            SelectedTank(hit);
        }
    }

    private void SelectedTank(RaycastHit2D hit)
    {
        if (!hit.collider.CompareTag(_parameters.TagTank)) { return; }
        var tank = hit.collider.gameObject.GetComponent<Tank>();
        if (tank.Team != _playerTeam) { return; }
        tank.SelectionCircle.SetActive(true);
        _selectedTanks.Add(tank);
    }

    private void UnSelectTank()
    {
        foreach(Tank tank in _selectedTanks)
        {
            tank.SelectionCircle.SetActive(false);
        }
        _selectedTanks.Clear();
    }

    private void Action(RaycastHit2D hit)
    {
        foreach (var tank in _selectedTanks)
        {
            if (hit.collider.CompareTag(_parameters.TagTank))
            {
                if (hit.collider.GetComponent<Tank>().Team == _playerTeam) { return; }
                TankActions.Target.Execute(tank, hit.collider.gameObject.transform);
            }
            else
            {
                TankActions.GoTo.Execute(tank, (Vector3)MousePosition);
            }
        }
    }

    public void OnGUI()
    {
        if (_drag)
        {
            _rect = Utils.GetScreenRect(_startingMousePosition, Input.mousePosition);
            Utils.DrawScreenRect(_rect, new Color(.5f, .5f, .5f, .5f));
            Utils.DrawScreenRectBorder(_rect, 0.1f, Color.grey);
        }
    }
}
