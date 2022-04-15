using UnityEngine;

public class ConflictState : IState<CapturePointFsm>
{
	public IState<CapturePointFsm> Handle(CapturePointFsm entity)
	{
		
		if (entity.TankPerTeamIn.Count == 0) return entity.NeutralS;
		if (entity.TankPerTeamIn.Count == 1) return entity.CaptureS;

		return this;
	}

	public void Update(CapturePointFsm entity)
	{
		
	}

	public void FixedUpdate(CapturePointFsm entity)
	{
		
	}

	public void Enter(CapturePointFsm entity)
	{
		
	}

	public void Exit(CapturePointFsm entity)
	{
		
	}
}