using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float Speed { get; set; }

	[SerializeField] private GameParameters parameters;
	[SerializeField] private GridController gridController;
	
	public Vector3 PositionToGo { get; private set; }

	private Cell[,] _grid;

	private float DistanceFromPoint => Vector2.Distance(PositionToGo, transform.position);

	public void LoadPathFinding(Vector3 positionToGo)
	{
		PositionToGo = positionToGo;
		_grid = gridController.GenerateFlowField(PositionToGo);
	}

	public void Move()
	{
		if (DistanceFromPoint < 0.5f) { return; }
		
		var cellBelow = GridController.GetCellFromWorldPosition(_grid, gridController.cellSize, transform.position);
		
		Vector2 targetDir = cellBelow.BestDirection.Vector;
		if(Vector2.Distance(PositionToGo, transform.position) < 1f)
		{
			targetDir = PositionToGo - transform.position;
		}

		var angle = Vector2.SignedAngle(targetDir, transform.up);
		
		if (Mathf.Abs(angle) > 1f)
		{
			Turn(angle, targetDir);
		}
		else
		{
			Translate();
		}
	}

	private void Translate()
	{
		Vector2 oldPos = transform.position;
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

	public void MoveWithoutPathFinding(Vector3 target)
	{
		if (Vector2.Distance(target, transform.position) < 0.1f) { return; }
		
		var cellBelow = GridController.GetCellFromWorldPosition(_grid, gridController.cellSize, transform.position);
		
		Vector2 targetDir = target - transform.position;

		var angle = Vector2.SignedAngle(targetDir, transform.up);
		if (Mathf.Abs(angle) > 1f)
		{
			transform.Rotate(new Vector3(0, 0, (parameters.TankTurnSpeed * -Mathf.Sign(angle)) * Time.deltaTime));
		}
		else
		{
			transform.position += transform.up * Speed * Time.deltaTime;
		}
	}
}