using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class Capture : ActionNode
{
    private Tank _tank;
    private Transform _capturePointTransform;

    public Capture(Tank tank, Transform capturePointTransform)
    {
        _tank = tank;
        _capturePointTransform = capturePointTransform;
    }

    public override NodeState Evaluate()
    {
        TankActions.GoTo.Execute(_tank, _capturePointTransform.position);
        _state = NodeState.RUNNING;
        return _state;
    }
}
