using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Selector : CompositeNode
    {
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        _state = NodeState.SUCCESS;
                        return _state;
                    case NodeState.RUNNING:
                        _state = NodeState.RUNNING;
                        return _state;
                    default:
                        continue;
                }
            }

            _state = NodeState.FAILURE;
            return _state;
        }
    }
}

