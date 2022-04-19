using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TargetEnemy : ActionNode
{
    private Tank _tank;

    public override void Init()
    {
        base.Init();
        _tank = GetData<Tank>("tank");
    }

    public override NodeState Evaluate()
    {
        var target = GetData<Transform>("target");
        if (target)
        {
            TankActions.Target.Execute(_tank, target);
        }

        _state = NodeState.RUNNING;
        return _state;
    }
}