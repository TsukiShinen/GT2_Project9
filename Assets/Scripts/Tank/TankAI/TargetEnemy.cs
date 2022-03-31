using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class TargetEnemy : Node
{
    private Tank _tank;

    public TargetEnemy(Tank tank)
    {
        _tank = tank;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target)
        {
            TankActions.Target.Execute(_tank, target);
        }

        _state = NodeState.RUNNING;
        return _state;
    }
}
