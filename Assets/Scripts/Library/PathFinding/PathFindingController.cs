using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
	public enum Algo
	{
		AStar,
		Dijkstra,
		FlowField,
	}
	
	[CreateAssetMenu(menuName = "PathFinding")]
	public class PathFindingController : ScriptableObject
	{
		[SerializeField] private GameParameters parameters;
		[SerializeField] private LayerMask terrainMasks;
		[Space(10)]
		public Vector2Int gridSize;
		public float cellSize = 1f;
		[Space(10)]
		[SerializeField] private Algo currentAlgo;
		
		private Cell[,] _grid;
		private PathFinding _currentPathFinding;

		private float CellRadius => cellSize / 2f;

		private PathFinding _getPathFinding(Algo algo) =>
			algo switch
			{
				Algo.Dijkstra => new Dijkstra(),
				Algo.AStar => new AStar(),
				Algo.FlowField => new FlowField(),
				_ => throw new ArgumentOutOfRangeException(nameof(algo), algo, null)
			};

		public void Init()
		{
			CreateGrid();
			CreateCostField();

			_currentPathFinding = _getPathFinding(currentAlgo);
			
			_currentPathFinding.Initialize(_grid, cellSize);
		}

		#region CreateGrid
		
		private void CreateGrid()
		{
			_grid = new Cell[gridSize.x, gridSize.y];

			for (var x = 0; x < gridSize.x; x++)
			{
				for (var y = 0; y < gridSize.y; y++)
				{
					var worldPos = new Vector2(cellSize * x + CellRadius, cellSize * y + CellRadius);
					_grid[x, y] = new Cell(worldPos, new Vector2Int(x, y));
				}
			}
		}

		private void CreateCostField()
		{

			foreach (var cell in _grid)
			{
				var origin = new Vector3(cell.WorldPosition.x, cell.WorldPosition.y, -10);
				var size = new Vector2(cellSize -0.1f, cellSize-0.1f);
				var hit = Physics2D.BoxCast(origin, size, 0f, Vector3.forward, 20f, terrainMasks);
            
				if (hit.collider == null) { continue; }
				if (hit.collider.gameObject.layer == parameters.LayerNavigationObstacleAsLayer)
				{
					cell.IncreaseCost(255);
				}
				else if(hit.collider.gameObject.layer == parameters.GroundNormalAsLayer)
				{
					cell.IncreaseCost(1);
				} 
				else if (hit.collider.gameObject.layer == parameters.LayerGroundSlowAsLayer)
				{
					cell.IncreaseCost(2);
				}
			}
		}

		#endregion

		public Queue<Vector3> GeneratePath(Vector2 startingPosition, Vector2 destinationPosition)
		{
			return _currentPathFinding.GeneratePath(startingPosition, destinationPosition);
		}

		public Queue<Vector3> GetPath()
		{
			return _currentPathFinding.GetPath();
		}
	}
}