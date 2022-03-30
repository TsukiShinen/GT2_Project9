using UnityEngine;

public class Goto : IState<Tank>
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

    public IState<Tank> Handle(Tank Entity)
    {
        if (Entity.NextState == "Target") { Entity.NextState = ""; return Entity.States.Target; }

        return this;
    }

    public void Update(Tank Entity)
    {
        Entity.Move(Entity.PositionToGo);
    }
}