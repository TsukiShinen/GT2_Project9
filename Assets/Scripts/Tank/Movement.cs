using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public float speed;

	[SerializeField] private GameParameters parameters;
	[SerializeField] private GridController grid;
	
	public Vector3 PositionToGo { get; set; }

	public void Move()
	{
		if (Vector2.Distance(PositionToGo, transform.position) > 0.1f) { return; }
		
		
		var cellBelow = grid.CurrentFlowField.GetCellFromWorldPosition(transform.position);
		
		Vector2 targetDir = cellBelow.BestDirection.Vector;
		if(cellBelow.BestDirection == GridDirection.None)
		{
			targetDir = PositionToGo - transform.position;
		}

		var angle = Vector2.SignedAngle(targetDir, transform.up);
		if (Mathf.Abs(angle) > 1f)
		{
			transform.Rotate(new Vector3(0, 0, (parameters.TankTurnSpeed * -Mathf.Sign(angle)) * Time.deltaTime));
		}
		else
		{
			transform.position += transform.up * speed * Time.deltaTime;
		}
	}
}