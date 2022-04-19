using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS, 
        FAILURE
    }
    public abstract class Node : ScriptableObject
    {
        protected NodeState _state;
        
        public Node Parent;
        public List<Node> Children = new List<Node>();

        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;

        private Dictionary<string, object> _contextData;

        public Node()
        {
            Parent = null;
        }
        public Node(List<Node> children)
        {
            Children = new List<Node>();
            foreach (Node child in children)
                _Attach(child);
        }

        public virtual void Init()
        {
            foreach (var child in Children)
            {
                child.Init();
            }
        }

        private void _Attach(Node node)
        {
            node.Parent = this;
            Children.Add(node);
        }

        public abstract NodeState Evaluate();

        public void SetData(string key, object value)
        {
            if (_contextData == null)
            {
                _contextData = new Dictionary<string, object>();
            }
            _contextData[key] = value;
        }

        protected T GetData<T>(string key)
        {
            if (_contextData == null)
            {
                _contextData = new Dictionary<string, object>();
            }

            if (_contextData.TryGetValue(key, out var value))
            {
                if (value is T returnValue)
                {
                    return returnValue;
                }
                else
                {
                    Debug.LogError($"value is not of type {nameof(T)}");
                    return default;
                }
            }
            var node = Parent;
            while(node != null)
            {
                value = node.GetData<T>(key);
                if(value != null)
                    return (T)value;
                node = node.Parent;
            }

            return default;
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
    
        public Node Clone(Node n)
        {
            var node= Instantiate(this);
            node.Children = Children.ConvertAll(c => c.Clone(node));
            node.Parent = n;
            return node;
        }
    }
}
