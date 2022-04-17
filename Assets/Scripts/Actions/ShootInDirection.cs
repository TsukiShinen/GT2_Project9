using UnityEngine;

public class ShootInDirection : IAction
{
	public void Execute(params object[] args)
	{
		var tank = args[0] as Tank;
		if (tank == null) { return; }
		var position = (Vector2)args[1];

		var target = new GameObject();
		target.transform.position = position;
		
		tank.Attack.Target = target.transform;
	}
}
