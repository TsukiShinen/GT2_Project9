using UnityEngine;
using BehaviourTree;

public class Waipoints : ActionNode
{
    private Tank _tank;
    private int index = 0;
    private Transform[] _waypointsTransform;

    public override void Init()
    {
        base.Init();
        _tank = GetData<Tank>("tank");
        _waypointsTransform = GetData<Transform[]>("waypointsPoint");
    }

    public override NodeState Evaluate()
    {
        TankActions.GoTo.Execute(_tank, _waypointsTransform[index].position);
        if(_tank.transform.position == _waypointsTransform[index].position)
        {
            index++;
            if(index >= _waypointsTransform.Length)
            {
                index = 0;
            }
        }
        _state = NodeState.RUNNING;
        return _state;
    }
}
