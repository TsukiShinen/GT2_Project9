using BehaviourTree;
using System.Collections.Generic;

/* class TankBT : Tree
{
    [UnityEngine.SerializeField] private Tank _tank;
    [UnityEngine.SerializeField] private UnityEngine.Transform _capturePointTransform;
    public static float DetectionRange = 5f;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInRange(transform, _tank),
                new TargetEnemy(_tank)
            }),
            new Capture(_tank, _capturePointTransform)
        });

        return root;
    }
}*/
