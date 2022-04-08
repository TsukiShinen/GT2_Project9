using UnityEngine;

public class ActionStop : IAction
{
	public void Execute(params object[] args)
	{
		var tank = args[0] as Tank;
		if (tank == null) { return; }
		
		tank.NextState = "Idle";
	}
}