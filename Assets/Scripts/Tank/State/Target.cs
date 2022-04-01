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
        if(Vector3.Distance(Entity.transform.position, Entity.Target.position) > 6f)
        {
            Entity.Move(Entity.Target.position);
        }
        else
        {
            Vector3 targetDir = Entity.Canon.position - Entity.Target.position;
            float angle = Vector2.SignedAngle(targetDir, Entity.Canon.up);

            if (angle > 1f || angle < -1f)
            {
                Entity.Canon.Rotate(new Vector3(0, 0, (Entity.GameParameters.TankTurnSpeed * -Mathf.Sign(angle)) * Time.deltaTime));
            }
            else if(Entity.CanShoot)
            {
                Entity.TimerShoot = Entity.GameParameters.TankShootDelay;
                Entity.StartCoroutine(Shoot(Entity));
            }
            
        }
        
    }

    public IState<Tank> Handle(Tank Entity)
    {
        if (Entity.NextState == "GoTo") { Entity.NextState = ""; return TankStates.Goto; }

        return this;
    }

    public IEnumerator Shoot(Tank Entity)
    {
        GameObject bullet = GameObject.Instantiate(Entity.Bullet, Entity.Canon.position, Quaternion.Euler(Entity.Canon.eulerAngles) * Quaternion.Euler(0, 0, 180f));
        Debug.Log("boom");
        yield return null;
    }
}