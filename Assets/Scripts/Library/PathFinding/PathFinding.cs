using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
	/// <summary>
	/// Base of the pathfinding
	/// </summary>
	public abstract class PathFinding
	{
		protected Cell[,] Grid { get; private set; }
		protected Vector2Int GridSize { get; private set; }
		protected float CellSize { get; private set; }

		protected Cell StartingCell;
		protected Cell DestinationCell;

		protected Queue<Vector3> Path;

		/// <summary>
		/// Create and return the Path 
		/// </summary>
		/// <param name="startingPosition"></param>
		/// <param name="destinationPosition"></param>
		/// <returns></returns>
		public abstract Queue<Vector3> GeneratePath(Vector2 startingPosition, Vector2 destinationPosition);

#if UNITY_EDITOR
		public abstract void OnDrawGizmos();
#endif

		/// <summary>
		/// Return the Path already created
		/// </summary>
		/// <returns></returns>
		public Queue<Vector3> GetPath()
		{
			return Path;
		}

		public void Initialize(Cell[,] baseGrid, float cellSize)
		{
			Grid = baseGrid;
			CellSize = cellSize;
			GridSize = new Vector2Int(Grid.GetLength(0), Grid.GetLength(1));
			Path = new Queue<Vector3>();
		}

		#region GetCells
		
		/// <summary>
		/// Get all neighbor cells from a list of positions
		/// </summary>
		/// <param name="cellIndex"></param>
		/// <param name="directions"></param>
		/// <returns></returns>
		protected List<Cell> GetNeighborCells(Vector2Int cellIndex, List<GridDirection> directions)
		{
			var neighbors = new List<Cell>();

			foreach (var direction in directions)
			{
				var newNeighbor = GetCellAtRelativePosition(cellIndex, direction);
				if (newNeighbor == null) { continue; }
				neighbors.Add(newNeighbor);
			}
        
			return neighbors;
		}

		/// <summary>
		/// Return the cell from the index
		/// </summary>
		/// <param name="originPosition"></param>
		/// <param name="relativePosition"></param>
		/// <returns></returns>
		protected Cell GetCellAtRelativePosition(Vector2Int originPosition, Vector2Int relativePosition)
		{
			var finalPosition = originPosition + relativePosition;

			if (finalPosition.x < 0 || finalPosition.x >= Grid.GetLength(0) || finalPosition.y < 0 || finalPosition.y >= Grid.GetLength(1)) { return null; }

			return Grid[finalPosition.x, finalPosition.y];
		}
    
		/// <summary>
		/// Return the cell from the world position
		/// </summary>
		/// <param name="worldPosition"></param>
		/// <returns></returns>
		protected Cell GetCellFromWorldPosition(Vector2 worldPosition)
		{
			var percentX = worldPosition.x / (GridSize.x * CellSize);
			var percentY = worldPosition.y / (GridSize.y * CellSize);

			percentX = Mathf.Clamp01(percentX);
			percentY = Mathf.Clamp01(percentY);

			var x = Mathf.Clamp(Mathf.FloorToInt((GridSize.x) * percentX), 0, GridSize.x - 1); 
			var y = Mathf.Clamp(Mathf.FloorToInt((GridSize.y) * percentY), 0, GridSize.y - 1);
			return Grid[x, y];
		}

		#endregion
	}
}