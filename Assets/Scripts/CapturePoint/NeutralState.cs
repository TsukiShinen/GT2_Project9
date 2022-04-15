using UnityEngine;

public class NeutralState : IState<CapturePointFsm>
{
	public IState<CapturePointFsm> Handle(CapturePointFsm entity)
	{
		if (entity.TankPerTeamIn.Count == 1) return entity.CaptureS;
		if (entity.TankPerTeamIn.Count > 1) return entity.ConflictS;

		return this;
	}

	public void Update(CapturePointFsm entity)
	{
		if (entity.score.Progression < 0) return;
			
		entity.score.Progression -= Time.deltaTime * 100/entity.score.timeToScore;
			
		if (entity.score.Progression < 0) 
			entity.score.Progression = 0;
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