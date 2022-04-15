using System;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField] private GameParameters parameters;
	public float Speed { get; set; }

	private float DistanceFromPositionToGo => Vector2.Distance(_positionToGo, transform.position);
	private bool ArrivedAtWaypoint => DistanceFromPositionToGo < 0.5f;
	public bool ArrivedAtDestination => ArrivedAtWaypoint && _waypoints.Count == 0;

	private Vector3 _positionToGo;
	private Queue<Vector3> _waypoints;

	private void Awake()
	{
		_positionToGo = transform.position;
		_waypoints = new Queue<Vector3>();
	}

	public void SetPath(Queue<Vector3> lstWaypoint)
	{
		_waypoints = lstWaypoint;
		_positionToGo = _waypoints.Dequeue();
	}

	public void AddToPath(Vector3 waypoint)
	{
		_waypoints.Enqueue(waypoint);
	}

	public void ClearPath()
	{
		_waypoints.Clear();
	}

	private void Update()
	{
		if (ArrivedAtWaypoint && _waypoints.Count > 0)
		{
			_positionToGo = _waypoints.Dequeue();
		}
		
		Move(_positionToGo);
	}

	private void Move(Vector3 target)
	{
		if (DistanceFromPositionToGo < 0.1f) { return; }
		
		Vector2 targetDir = target - transform.position;

		var angle = Vector2.SignedAngle(targetDir, transform.up);
		if (Mathf.Abs(angle) > 1f)
		{
			Turn(angle, targetDir);
		}
		else
		{
			MoveForward();
		}
	}

	private void MoveForward()
	{
		transform.position += transform.up * Speed * Time.deltaTime;
	}

	private void Turn(float angle, Vector2 targetDir)
	{
		transform.Rotate(new Vector3(0, 0, (parameters.TankTurnSpeed * -Mathf.Sign(angle)) * Time.deltaTime));
		if (Mathf.Abs(angle) < Mathf.Abs(Vector2.SignedAngle(targetDir, transform.up)))
		{
			transform.up = targetDir;
		}
	}
}