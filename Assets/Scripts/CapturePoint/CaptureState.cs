using UnityEngine;

public class CaptureState : IState<CapturePointFsm>
{
	public IState<CapturePointFsm> Handle(CapturePointFsm entity)
	{
		if (entity.TankPerTeamIn.Count == 0) return entity.NeutralS;
		if (entity.TankPerTeamIn.Count > 1) return entity.ConflictS;

		return this;
	}

	public void Update(CapturePointFsm entity)
	{
		Team team = null;
		foreach (var item in entity.TankPerTeamIn)
		{
			team = item.Key;
		}
		
		if (entity.score.TeamScoring == null) { entity.score.TeamScoring = team; }

		if (entity.score.TeamScoring != team)
		{
			CounterCapture(entity, team);
		} else
		{
			Capture(entity, team);
		}
	}

	private void CounterCapture(CapturePointFsm entity, Team team)
	{
		entity.score.Progression -= Time.deltaTime * 100/entity.score.timeToScore;
		if (entity.score.Progression <= 0)
		{
			entity.score.Progression = 0;
			entity.score.TeamScoring = team;
		}
	}

	private void Capture(CapturePointFsm entity, Team team)
	{
		if (entity.score.Progression < 100)
		{
			entity.score.Progression += Time.deltaTime * 100 / entity.score.timeToScore;
		} else
		{
			entity.score.Progression = 100;
			if (team == entity.score.playerScore.team)
			{
				entity.score.playerScore.score += Time.deltaTime;
			} else
			{
				entity.score.enemyScore.score += Time.deltaTime;
			}
		}
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