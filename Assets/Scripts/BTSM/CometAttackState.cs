using UnityEngine;

public class CometAttackState : IState<BTSM>
{
	public IState<BTSM> Handle(BTSM entity)
	{
		return this;
	}

	public void Update(BTSM entity)
	{
		if (entity.tank1 == null)
		{
			var tank = entity.tank2;
			if(entity.tank2 != null)
            {
				tank = entity.tank2;
				entity.tank2 = null;
			}
			else
            {
				tank = entity.tank3;
				entity.tank3 = null;

			}

			if(tank != null) {
				tank.GetComponent<TankBT>().ChangeTree(entity.tree1);
			}
			entity.tank1 = tank;
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