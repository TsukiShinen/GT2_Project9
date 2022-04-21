using UnityEngine;

public class ActionTarget : IAction
{
    public void Execute(params object[] args)
    {
        var tank = args[0] as Tank;
        if (tank == null) { return; }
        var target = args[1] as Transform;

        var life = target.GetComponentInChildren<LifeBar>();
        
        if (!life.IsAlive) return;
        
        tank.Attack.Target = target;
        tank.Attack.EnemyLife = life;
        if (!target) return;

        var direction = (target.position - tank.transform.position).normalized;
        
        // TODO : Add proper range
        tank.Movement.AddToPath(target.position - direction*3);
        
        
    }
}
