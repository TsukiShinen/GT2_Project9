using System;
using System.Collections.Generic;
using System.Linq;
using BehaviourTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Node = BehaviourTree.Node;
using Tree = BehaviourTree.Tree;

public class BehaviourTreeView : GraphView
{
    public Action<NodeView> OnNodeSelected;
    public new class UxmlFactory :  UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits>  { }
    private Tree _tree;
    
    public BehaviourTreeView()
    {
        Insert(0, new GridBackground());
        
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
         
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTreeEditor.uss");
        styleSheets.Add(styleSheet);

        Undo.undoRedoPerformed += OnUndoRedo;
    }

    private void OnUndoRedo()
    {
        PopulateView(_tree);
        AssetDatabase.SaveAssets();
    }

    private NodeView FindNodeView(Node node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }

    internal void PopulateView(Tree tree)
    {
        this._tree = tree;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (tree.root == null)
        {
            tree.root = tree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }
        
        // Nodes
        tree.nodes.ForEach(n => CreateNodeView(n));
        
        //Edges
        tree.nodes.ForEach(n =>
        {
            var children = tree.GetChildren(n);
            if (children == null)
            {
                children = new List<Node>();
            }

            children.ForEach(c =>
            {
                var parentView = FindNodeView(n);
                var childView = FindNodeView(c);

                var edge = parentView.Output.ConnectTo(childView.Input);
                AddElement(edge);
            });
        });
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort =>
            endPort.direction != startPort.direction &&
            endPort.node != startPort.node).ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                var nodeView = elem as NodeView;
                if (nodeView != null)
                {
                    _tree.DeleteNode(nodeView.Node);
                }
                
                var edge = elem as Edge;
                if (edge != null)
                {
                    var parentView = edge.output.node as NodeView;
                    var childView = edge.input.node as NodeView;
                    _tree.RemoveChild(parentView.Node, childView.Node);
                }
            });
        }
        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                var parentView = edge.output.node as NodeView;
                var childView = edge.input.node as NodeView;
                
                _tree.AddChild(parentView.Node, childView.Node);
            });
        }

        return graphViewChange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);
        {
            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
            }
            types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
            }
            types = TypeCache.GetTypesDerivedFrom<DecorateNode>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
            }
        }
    }

    private void CreateNode(System.Type type)
    {
        var node = _tree.CreateNode(type);
        CreateNodeView(node);
    }

    private void CreateNodeView(BehaviourTree.Node node)
    {
        var nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }
}
