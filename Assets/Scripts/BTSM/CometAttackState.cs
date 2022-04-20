using UnityEngine;

public class CometAttackState : IState<BTSM>
{
	public IState<BTSM> Handle(BTSM entity)
	{
		return this;
	}

	public void Update(BTSM entity)
	{
		if (!entity.tank1.GetComponentInChildren<LifeBar>().IsAlive)
		{
			entity.tank1.GetComponent<TankBt>().ChangeTree(entity.tree2);
			entity.tank2.GetComponent<TankBt>().ChangeTree(entity.tree1);
			(entity.tank2, entity.tank1) = (entity.tank1, entity.tank2);
		}
	}

	public void FixedUpdate(BTSM entity)
	{

	}

	public void Enter(BTSM entity)
	{

	}

	public void Exit(BTSM entity)
	{

	}

	public void ReplaceTank()
    {

    }
}