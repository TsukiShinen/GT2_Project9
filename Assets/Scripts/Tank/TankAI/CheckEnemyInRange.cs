using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class CheckEnemyInRange : ActionNode
{
    private Transform _transform;
    private Tank _tank;
    private ContactFilter2D _filter;

    public float detectionRange;

    private List<Collider2D> _results = new List<Collider2D>();

    public override void Init()
    {
        base.Init();
        _tank = GetData<Tank>("tank");
        _transform = GetData<Transform>("transform");
        var layerMask = GetData<LayerMask>("tankFilter");
        _filter = new ContactFilter2D();
        _filter.SetLayerMask(layerMask);
    }

    public override NodeState Evaluate()
    {
        var t = GetData<Transform>("target");
        if (t == null)
        {
            var size = Physics2D.OverlapCircle(_transform.position, detectionRange, _filter, _results);

            for (var i = 0; i < size; i++)
            {
                var collider = _results[i];
                if (!collider.CompareTag("Tank")) { continue; }
                if (collider.GetComponent<Tank>().Team == _tank.Team) { continue; }

                Parent.Parent.SetData("target", collider.transform);
                _state = NodeState.SUCCESS;
                return _state;
            }

            _state = NodeState.FAILURE;
            return _state;
        }

        _state = NodeState.SUCCESS;
        return _state;
    }
}
