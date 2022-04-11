using BehaviourTree;
using UnityEngine;

public class TankBt : BehaviourTreeRunner
{
	[SerializeField] private Transform point;
	
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
	}
}
