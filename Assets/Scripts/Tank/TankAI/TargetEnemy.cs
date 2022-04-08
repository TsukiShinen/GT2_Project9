using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TargetEnemy : ActionNode
{
    private Tank _tank;

    public TargetEnemy(Tank tank)
    {
        this._tank = tank;
    }

    public override NodeState Evaluate()
    {
        var target = (Transform)GetData("target");
        if (target)
        {
            TankActions.Target.Execute(_tank, target);
        }

        _state = NodeState.RUNNING;
        return _state;
    }
}