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
        if (Vector3.Distance(Entity.PositionToGo, Entity.transform.position) > 0.1f)
        {
            Vector3 targetDir = Entity.PositionToGo - Entity.transform.position;
            float angle = Vector2.SignedAngle(targetDir, Entity.transform.up);
            if (angle > 1f || angle < -1f)
            {
                Entity.transform.Rotate(new Vector3(0, 0, 1f*-Mathf.Sign(angle)));
            }
            else
            {
                Entity.transform.position += Entity.transform.up * Entity.Speed * Time.deltaTime;
            }
        }
    }
}