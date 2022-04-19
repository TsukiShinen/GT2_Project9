using UnityEngine;

public class ActionTarget : IAction
{
    public void Execute(params object[] args)
    {
        var tank = args[0] as Tank;
        if (tank == null) { return; }
        var target = args[1] as Transform;

        tank.Attack.Target = target;
        if (!target) return;

        var direction = (target.position - tank.transform.position).normalized;
        
        // TODO : Add proper range
        tank.Movement.AddToPath(target.position - direction*3);
    }
}
