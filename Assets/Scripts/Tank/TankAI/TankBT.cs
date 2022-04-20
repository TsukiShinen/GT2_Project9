using BehaviourTree;
using UnityEngine;

public class TankBt : BehaviourTreeRunner
{
	public Transform point;
	public LayerMask tankMask;
	public Transform[] waypoints;
	private Tank _tank;

	private void Awake()
	{
		_tank = GetComponent<Tank>();
	}

	protected override void LoadData()
	{
		tree.root.SetData("tank", _tank);
		tree.root.SetData("transform", transform);
		tree.root.SetData("point", point);
		tree.root.SetData("tankFilter", tankMask);
		tree.root.SetData("waypointsPoint", waypoints);
	}

    public void ChangeTree(BehaviourTree.Tree newTree)
    {
        tree = newTree;
		Init();
    }
}
