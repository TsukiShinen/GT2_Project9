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
        Vector3 targetDir = Entity.PositionToGo - Entity.transform.position;
        float angle = Vector3.Angle(targetDir, Entity.transform.up);
        if (angle > 1f)
        {
            Entity.transform.Rotate(new Vector3(0, 0, 1));
        } 
        else
        {
            Entity.transform.position += Entity.transform.up * Entity.Speed * Time.deltaTime;
        }
    }
}