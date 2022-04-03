using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGoTo : IAction
{
    public void Execute(params object[] args)
    {
        Tank tank = args[0] as Tank;
        if (tank == null) { return; }
        Vector3 positionToGo = (Vector3)args[1];

        if (tank.PositionToGo == positionToGo) { return; }
        tank.PositionToGo = positionToGo;
        tank.GridController.GenerateFlowField(positionToGo);
        tank.NextState = "GoTo";
    }
}
