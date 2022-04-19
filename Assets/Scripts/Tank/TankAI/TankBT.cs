using BehaviourTree;
using UnityEngine;

public class TankBt : BehaviourTreeRunner
{
	[SerializeField] private Transform point;
	[SerializeField] private LayerMask tankMask;
	
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
	}
}
