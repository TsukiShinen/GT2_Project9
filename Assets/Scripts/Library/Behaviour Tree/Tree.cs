using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
            if (node == null) return null;
            node.name = type.Name;

#if UNITY_EDITOR
            node.guid = GUID.Generate().ToString();
            Undo.RecordObject(this, "Behaviour Tree (Create Child)");
#endif

            nodes.Add(node);

#if UNITY_EDITOR
            AssetDatabase.AddObjectToAsset(node, this);
            Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (Create Node)");

            AssetDatabase.SaveAssets();
#endif
            return node;
        }

        public void DeleteNode(Node node)
        {
#if UNITY_EDITOR
            Undo.RecordObject(this, "Behaviour Tree (Delete Child)");
#endif
            nodes.Remove(node);
            
#if UNITY_EDITOR
            Undo.DestroyObjectImmediate(node);
            
            AssetDatabase.SaveAssets();
#endif
        }

        public void AddChild(Node parent, Node child)
        {
            var node = parent as Node;

            if (!node) return;
#if UNITY_EDITOR
            Undo.RecordObject(node, "Behaviour Tree (Add Child)");
#endif
            node.Children ??= new List<Node>();
            node.Children.Add(child);
            child.Parent = node;
        }
        
        public void RemoveChild(Node parent, Node child)
        {
            var node = parent as Node;
            if (!node) return;
#if UNITY_EDITOR
            Undo.RecordObject(node, "Behaviour Tree (Remove Child)");
#endif
            node.Children.Remove(child);
            child.Parent = null;
#if UNITY_EDITOR
            EditorUtility.SetDirty(node);
#endif
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
