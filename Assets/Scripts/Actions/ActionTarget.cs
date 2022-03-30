using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTarget : IAction
{
    public void Execute(params object[] args)
    {
        Tank tank = args[0] as Tank;
        if (tank == null) { return; }
        Transform target = args[1] as Transform;

        tank.Target = target;
        tank.NextState = "Target";
    }
}
