using System.Collections;
using System.Collections.Generic;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS, 
        FAILURE
    }
    public class Node
    {
        protected NodeState _state;

        public Node Parent;
        protected List<Node> _children;

        private Dictionary<string, object> _contextData = new Dictionary<string, object>();

        public Node()
        {
            Parent = null;
        }
        public Node(List<Node> children)
        {
            _children = new List<Node>();
            foreach (Node child in children)
                _Attach(child);
        }

        private void _Attach(Node node)
        {
            node.Parent = this;
            _children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            _contextData[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (_contextData.TryGetValue(key, out value))
                return value;

            Node node = Parent;
            while(node!= null)
            {
                value = node.GetData(key);
                if(value != null)
                    return value;
                node = node.Parent;
            }

            return null;
        }

        public bool ClearData(string key)
        {
            if (_contextData.ContainsKey(key))
            {
                _contextData.Remove(key);
                return true;
            }

            Node node = Parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.Parent;
            }

            return false;
        }
    }
}
