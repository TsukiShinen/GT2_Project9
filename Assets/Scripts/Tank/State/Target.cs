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
        if(Vector2.Distance(Entity.transform.position, Entity.Target.position) > 6f)
        {
            Entity.Move(Entity.Target.position);
        }
        else
        {
            Vector2 targetDir = Entity.canon.position - Entity.Target.position;
            float angle = Vector2.SignedAngle(targetDir, Entity.canon.up);

            if (angle > 1f || angle < -1f)
            {
                Entity.canon.Rotate(new Vector3(0, 0, (Entity.parameters.TankTurnSpeed * -Mathf.Sign(angle)) * Time.deltaTime));
            }
            else if(Entity.CanShoot)
            {
                Entity.TimerShoot = Entity.parameters.TankShootDelay;
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
        GameObject bullet = GameObject.Instantiate(Entity.bullet, Entity.canon.position, Quaternion.Euler(Entity.canon.eulerAngles) * Quaternion.Euler(0, 0, 180f));
        Debug.Log("boom");
        yield return null;
    }
}