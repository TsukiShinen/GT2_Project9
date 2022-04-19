using UnityEngine;
using BehaviourTree;

public class Capture : ActionNode
{
    private Tank _tank;
    private Transform _capturePointTransform;

    public override void Init()
    {
        base.Init();
        _tank = GetData<Tank>("tank");
        _capturePointTransform = GetData<Transform>("point");
    }

    public override NodeState Evaluate()
    {
        TankActions.GoTo.Execute(_tank, _capturePointTransform.position);
        _state = NodeState.RUNNING;
        return _state;
    }
}
