using System;
using System.Collections.Generic;
using UnityEngine;

public class CapturePointFsm : StateMachine<CapturePointFsm>
{
	public GameParameters parameters;
	public Score score;
	
	public Dictionary<Team, int> TankPerTeamIn { get; set; }
	
	public NeutralState NeutralS { get; set; }
	public CaptureState CaptureS { get; set; }
	public ConflictState ConflictS { get; set; }
	
	private void Awake()
	{
		Init(this);
		TankPerTeamIn = new Dictionary<Team, int>();

		NeutralS = new NeutralState();
		CaptureS = new CaptureState();
		ConflictS = new ConflictState();
		
		ChangeState(NeutralS);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision == null) { return; }
		if (!collision.gameObject.CompareTag(parameters.TagTank)) { return; }
		var team = collision.gameObject.GetComponent<Tank>().Team;
		if (TankPerTeamIn.ContainsKey(team))
		{
			TankPerTeamIn[team] += 1;
		}
		else
		{
			TankPerTeamIn.Add(team, 1);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision == null) { return; }
		if (!collision.gameObject.CompareTag(parameters.TagTank)) { return; }
		var team = collision.gameObject.GetComponent<Tank>().Team;
		TankPerTeamIn[team] -= 1;
		if (TankPerTeamIn[team] == 0)
		{
			TankPerTeamIn.Remove(team);
		}
	}
}