using UnityEngine;

public class ActionGoTo : IAction
{
    public void Execute(params object[] args)
    {
        var tank = args[0] as Tank;
        if (tank == null) { return; }
        var positionToGo = (Vector3)args[1];

        if (tank.Movement.PositionToGo == positionToGo) { return; }
        tank.Movement.LoadPathFinding(positionToGo);
        tank.NextState = "GoTo";
    }
}
