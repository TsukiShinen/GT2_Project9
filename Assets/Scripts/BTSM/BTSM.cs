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
			tank1.GetComponent<TankBT>().tankMask = tankMask;
			tank1.GetComponent<TankBT>().point = point;
			tank1.GetComponent<TankBT>().ChangeTree(tree1);
		}
		else if(tank2 == null)
        {
			tank2 = tank;
			tank2.GetComponent<TankBT>().tankMask = tankMask;
			tank2.GetComponent<TankBT>().point = point;
			tank2.GetComponent<TankBT>().waypoints = waypointsTank2;
			tank2.GetComponent<TankBT>().ChangeTree(tree2);
		}
		else if (tank3 == null)
		{
			tank3 = tank;
			tank3.GetComponent<TankBT>().tankMask = tankMask;
			tank3.GetComponent<TankBT>().point = point;
			tank3.GetComponent<TankBT>().waypoints = waypointsTank3;
			tank3.GetComponent<TankBT>().ChangeTree(tree3);
		}
		else if (!tank1.GetComponentInChildren<LifeBar>().IsAlive)
        {
			tank1 = tank;
			tank1.GetComponent<TankBT>().tankMask = tankMask;
			tank1.GetComponent<TankBT>().point = point;
			tank1.GetComponent<TankBT>().ChangeTree(tree1);
		}
		else if (!tank2.GetComponentInChildren<LifeBar>().IsAlive)
        {
			tank2 = tank;
			tank2.GetComponent<TankBT>().tankMask = tankMask;
			tank2.GetComponent<TankBT>().point = point;
			tank2.GetComponent<TankBT>().waypoints = waypointsTank2;
			tank2.GetComponent<TankBT>().ChangeTree(tree2);
		}
		else if (!tank3.GetComponentInChildren<LifeBar>().IsAlive)
        {
			tank3 = tank;
			tank3.GetComponent<TankBT>().tankMask = tankMask;
			tank3.GetComponent<TankBT>().point = point;
			tank3.GetComponent<TankBT>().waypoints = waypointsTank3;
			tank3.GetComponent<TankBT>().ChangeTree(tree3);
		}
	}
}