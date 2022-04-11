using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

using BehaviourTree;

public class CheckEnemyInRange : ActionNode
{
    private Transform _transform;
    private Tank _tank;

    public float detectionRange;

    public override void Init()
    {
        base.Init();
        _tank = GetData("tank") as Tank;
        _transform = GetData("transform") as Transform;
    }

    public override NodeState Evaluate()
    {
        var t = GetData("target");
        if (t == null)
        {
            var colliders = Physics2D.OverlapCircleAll(_transform.position, detectionRange);

            if (colliders.Length > 0)
            {
                foreach (var collider in colliders)
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
