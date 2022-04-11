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

        public void Update()
        {
            if (root != null)
                root.Evaluate();
        }

        public Node CreateNode(System.Type type)
        {
            var node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            
            Undo.RecordObject(this, "Behaviour Tree (Create Child)");
            nodes.Add(node);
            
            AssetDatabase.AddObjectToAsset(node,  this);
            Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (Create Node)");
            
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node)
        {
            Undo.RecordObject(this, "Behaviour Tree (Delete Child)");
            nodes.Remove(node);
            // AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node);
            
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            var node = parent as Node;
            
            if (node)
            {
                Undo.RecordObject(node, "Behaviour Tree (Add Child)");
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
                Undo.RecordObject(node, "Behaviour Tree (Remove Child)");
                node.Children.Remove(child);
                child.Parent = null;
                EditorUtility.SetDirty(node);
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

        public Tree Clone()
        {
            var tree = Instantiate(this);
            tree.root = root.Clone(null);
            return tree;
        }
    }
}
