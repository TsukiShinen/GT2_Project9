using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu(menuName = "Behaviour Tree/Tree")]
    public class Tree : ScriptableObject
    {
        [HideInInspector] public Node root;

        public List<Node> nodes = new List<Node>();

        private void Update()
        {
            if (root != null)
                root.Evaluate();
        }

        public Node CreateNode(System.Type type)
        {
            var node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);
            
            AssetDatabase.AddObjectToAsset(node,  this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            var node = parent as Node;
            
            if (node)
            {
                if (node.Children == null)
                {
                    node.Children = new List<Node>();
                }
                node.Children.Add(child);
                child.Parent = node;
            }
        }
        
        public void RemoveChild(Node parent, Node child)
        {
            var node = parent as Node;
            if (node)
            {
                node.Children.Remove(child);
                child.Parent = null;
            }
        }
        
        public List<Node> GetChildren(Node parent)
        {
            var children = new List<Node>();
            var node = parent as Node;
            if (node && node.Children != null)
            {
                children.AddRange(node.Children);
            }

            return children;
        }
    }
}
