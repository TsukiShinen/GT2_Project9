using System;
using System.Collections.Generic;
using UnityEngine;

public class BTSM : StateMachine<BTSM>
{
	public Tank tank1;
	public Tank tank2;
	public Tank tank3;

	public Transform point;
	public LayerMask tankMask;
	public Transform[] waypointsTank2;
	public Transform[] waypointsTank3;

	public BehaviourTree.Tree tree1;
	public BehaviourTree.Tree tree2;
	public BehaviourTree.Tree tree3;
	public CometAttackState CAS { get; set; }

	private void Awake()
	{
		Init(this);

		CAS = new CometAttackState();

		ChangeState(CAS);
	}

    private void OnEnable()
    {
		Spawn.Instance.OnRedTankCreated += OnTankCreated;
	}
    private void OnTankCreated(Tank tank)
    {
		if(tank1 == null)
        {
			tank1 = tank;
			tank1.GetComponent<TankBt>().tankMask = tankMask;
			tank1.GetComponent<TankBt>().point = point;
			tank1.GetComponent<TankBt>().ChangeTree(tree1);
		}
		else if(tank2 == null)
        {
			tank2 = tank;
			tank2.GetComponent<TankBt>().tankMask = tankMask;
			tank2.GetComponent<TankBt>().point = point;
			tank2.GetComponent<TankBt>().waypoints = waypointsTank2;
			tank2.GetComponent<TankBt>().ChangeTree(tree2);
		}
		else if (tank3 == null)
		{
			tank3 = tank;
			tank3.GetComponent<TankBt>().tankMask = tankMask;
			tank3.GetComponent<TankBt>().point = point;
			tank3.GetComponent<TankBt>().waypoints = waypointsTank3;
			tank3.GetComponent<TankBt>().ChangeTree(tree3);
		}
		else if (!tank1.GetComponentInChildren<LifeBar>().IsAlive)
        {
			tank1 = tank;
			tank1.GetComponent<TankBt>().tankMask = tankMask;
			tank1.GetComponent<TankBt>().point = point;
			tank1.GetComponent<TankBt>().ChangeTree(tree1);
		}
		else if (!tank2.GetComponentInChildren<LifeBar>().IsAlive)
        {
			tank2 = tank;
			tank2.GetComponent<TankBt>().tankMask = tankMask;
			tank2.GetComponent<TankBt>().point = point;
			tank2.GetComponent<TankBt>().waypoints = waypointsTank2;
			tank2.GetComponent<TankBt>().ChangeTree(tree2);
		}
		else if (!tank3.GetComponentInChildren<LifeBar>().IsAlive)
        {
			tank3 = tank;
			tank3.GetComponent<TankBt>().tankMask = tankMask;
			tank3.GetComponent<TankBt>().point = point;
			tank3.GetComponent<TankBt>().waypoints = waypointsTank3;
			tank3.GetComponent<TankBt>().ChangeTree(tree3);
		}
	}
}