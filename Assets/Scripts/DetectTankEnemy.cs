using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DetectTankEnemy : MonoBehaviour
{
    public LayerMask Layer;
    public GameParameters GameParameters;
    public Team EnemyTeam;
    Vector3 direction;

    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = GameParameters.TankVisionRange;
        GetComponent<Light2D>().pointLightInnerRadius = GameParameters.TankVisionRange - (GameParameters.TankVisionRange * 0.1f);
        GetComponent<Light2D>().pointLightOuterRadius = GameParameters.TankVisionRange;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag(GameParameters.TagTank)) { return; }
        if (collision.gameObject.GetComponent<Tank>().Team.name == EnemyTeam.name)
        {
            direction = collision.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, GameParameters.TankVisionRange, Layer);

            if (hit.collider == null)
            {
                collision.GetComponent<Spotted>().SpriteOn();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(GameParameters.TagTank)) { return; }
        if (collision.gameObject.GetComponent<Tank>().Team.name == EnemyTeam.name)
        {
            collision.GetComponent<Spotted>().SpriteOff();
        }  
    }

}
