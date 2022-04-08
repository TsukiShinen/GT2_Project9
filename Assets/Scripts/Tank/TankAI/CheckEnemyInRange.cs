using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class CheckEnemyInRange : ActionNode
{
    private Transform _transform;
    private Tank _tank;

    public float DetectionRange;

    public CheckEnemyInRange(Transform transform, Tank tank)
    {
        _transform = transform;
        _tank = tank;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, DetectionRange);

            if (colliders.Length > 0)
            {
                foreach (Collider2D collider in colliders)
                {
                    if (!collider.CompareTag("Tank")) { continue; }
                    if (collider.GetComponent<Tank>().Team == _tank.Team) { continue; }

                    Parent.Parent.SetData("target", collider.transform);
                    _state = NodeState.SUCCESS;
                    return _state;
                }
            }

            _state = NodeState.FAILURE;
            return _state;
        }

        _state = NodeState.SUCCESS;
        return _state;
    }
}
