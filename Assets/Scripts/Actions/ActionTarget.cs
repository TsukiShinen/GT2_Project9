using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTarget : IAction
{
    public void Execute(params object[] args)
    {
        var tank = args[0] as Tank;
        if (tank == null) { return; }
        var target = args[1] as Transform;

        tank.Attack.Target = target;
        
        tank.NextState = "Target";
    }
}
