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
        if(Vector3.Distance(Entity.transform.position, Entity.Target.position) > 3f)
        {
            Entity.Move(Entity.Target.position);
        }

        if(Entity.TimerShoot <= 0 && Vector3.Distance(Entity.transform.position, Entity.Target.position) < 3f)
        {
            Entity.TimerShoot = Entity.GameParameters.TankShootDelay;
            Entity.StartCoroutine(Shoot());
        }
        
    }

    public IState<Tank> Handle(Tank Entity)
    {
        if (Entity.NextState == "GoTo") { Entity.NextState = ""; return Entity.States.Goto; }

        return this;
    }

    public IEnumerator Shoot()
    {
        Debug.Log("boom");
        yield return null;
    }
}