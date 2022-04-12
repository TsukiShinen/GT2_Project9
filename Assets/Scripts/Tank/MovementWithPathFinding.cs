using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithPathFinding : Movement
{
    private List<Vector3> _arrayWaipoint;
    public override void Move()
    {
        SimpleMove(_arrayWaipoint[0]);

        if (Vector2.Distance(_arrayWaipoint[0], transform.position) < 0.5f)
        {
            _arrayWaipoint.RemoveAt(0);
        }
    }
}
