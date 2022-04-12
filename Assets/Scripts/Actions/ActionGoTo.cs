using UnityEngine;

public class ActionGoTo : IAction
{
    public void Execute(params object[] args)
    {
        var tank = args[0] as Tank;
        if (tank == null) { return; }
        var positionToGo = (Vector3)args[1];
        
        if (positionToGo == tank.PositionToGo) return;
        tank.GoTo(positionToGo);
        tank.NextState = "GoTo";
    }
}
