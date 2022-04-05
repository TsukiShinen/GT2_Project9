using UnityEngine;
using System.Collections;
public class Target : IState<Tank>
{
    public void Enter(Tank Entity)
    {

    }

    public void Exit(Tank Entity)
    {

    }

    public void FixedUpdate(Tank Entity)
    {

    }

    public void Update(Tank Entity)
    {
        if(Vector2.Distance(Entity.transform.position, Entity.Attack.Target.position) > 6f)
        {
            // Add move to Entity.Target.position
            Entity.Movement.Move();
        }
        else
        {
            Entity.Attack.Aim();
            Entity.Attack.ShootUpdate();
        }
        
    }

    public IState<Tank> Handle(Tank Entity)
    {
        if (Entity.NextState == "GoTo") { Entity.NextState = ""; return TankStates.Goto; }

        return this;
    }
}