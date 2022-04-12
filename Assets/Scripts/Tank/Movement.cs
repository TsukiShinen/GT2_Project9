using System;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
	public float Speed { get; set; }

	[SerializeField] private GameParameters parameters;
	//private GridController _gridController;
	
	public Vector3 PositionToGo { get; set; }
	public bool IsMoving => MustMove;

	//private Cell[,] _grid;

	protected float DistanceFromPoint => Vector2.Distance(PositionToGo, transform.position);
	protected bool MustMove => DistanceFromPoint > 0.5f;

	private void Awake()
	{
		//_gridController = FindObjectOfType<GridController>();
	}

	/*public void LoadPathFinding(Vector3 positionToGo)
	{
		PositionToGo = positionToGo;
		_grid = _gridController.GenerateFlowField(PositionToGo);
		IsMoving = false;
	}*/

	public abstract void Move();

	/*public void Move()
	{
		if (DistanceFromPoint < 0.5f) { IsMoving = false; return; }

		IsMoving = true;
		var cellBelow = GridController.GetCellFromWorldPosition(_grid, _gridController.cellSize, transform.position);
		
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
	}*/

	public void SimpleMove(Vector3 target)
	{
		if (!MustMove) { return; }
		
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

	protected void MoveForward()
	{
		transform.position += transform.up * Speed * Time.deltaTime;
	}

	protected void Turn(float angle, Vector2 targetDir)
	{
		transform.Rotate(new Vector3(0, 0, (parameters.TankTurnSpeed * -Mathf.Sign(angle)) * Time.deltaTime));
		if (Mathf.Abs(angle) < Mathf.Abs(Vector2.SignedAngle(targetDir, transform.up)))
		{
			transform.up = targetDir;
		}
	}
}