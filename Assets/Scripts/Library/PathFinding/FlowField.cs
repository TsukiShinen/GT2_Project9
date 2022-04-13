using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
	public class FlowField : PathFinding
	{
		public override Queue<Vector3> GeneratePath(Vector2 startingPosition, Vector2 destinationPosition)
        {
            var destinationCell = GetCellFromWorldPosition(destinationPosition);
            if (destinationCell == DestinationCell) return Path;
            
            DestinationCell = destinationCell;
            StartingCell = GetCellFromWorldPosition(startingPosition);
			CreateIntegrationField(destinationCell);
            CreateFlowField();
            CreatePath();
        
            return Path;
		}
		
        private void CreateIntegrationField(Cell destinationCell)
        {
            ResetBestCost();
    
            var cost = DestinationCell.Cost;
            DestinationCell.Cost = 0;
            DestinationCell.BestCost = 0;
    
            var cellsToCheck = new Queue<Cell>();
    
            cellsToCheck.Enqueue(DestinationCell);
    
            while(cellsToCheck.Count > 0)
            {
                var cell = cellsToCheck.Dequeue();
                var neighbors = GetNeighborCells(cell.GridIndex, GridDirection.CardinalDirections);
                foreach (var neighbor in neighbors)
                {
                    if (neighbor.Cost == byte.MaxValue) { continue; }
                    if (neighbor.Cost + cell.BestCost >= neighbor.BestCost) { continue; }
                    
                    neighbor.BestCost = (ushort)(neighbor.Cost + cell.BestCost);
                    cellsToCheck.Enqueue(neighbor);
                }
            }
            DestinationCell.BestDirection = GridDirection.None;
            DestinationCell.Cost = cost;
        }
    
        private void ResetBestCost()
        {
            for (var x = 0; x < GridSize.x; x++)
            {
                for (var y = 0; y < GridSize.y; y++)
                {
                    Grid[x, y].BestCost = ushort.MaxValue;
                }
            }
        }
    
        private void CreateFlowField()
        {
            foreach(var cell in Grid)
            {
                var neighbors = GetNeighborCells(cell.GridIndex, GridDirection.AllDirection);
    
                int bestCost = cell.BestCost;
    
                foreach (var neighbor in neighbors)
                {
                    if (neighbor.BestCost >= bestCost) { continue; }
                        
                    bestCost = neighbor.BestCost;
                    cell.BestDirection = GridDirection.GetDirectionFromV2I(neighbor.GridIndex - cell.GridIndex);
                }
            }
        }

        private void CreatePath()
        {
            Path = new Queue<Vector3>();
            var position = StartingCell.WorldPosition;
        
            var arrived = false;
            while (!arrived)
            {
                var cell = GetCellFromWorldPosition(position);
                var dir = new Vector2(cell.BestDirection.Vector.x, cell.BestDirection.Vector.y) * CellSize;
            
                var newPosition = cell.WorldPosition + dir;
                if (position == newPosition)
                {
                    arrived = true;
                }
                else
                {
                    Path.Enqueue(newPosition);
                    position = newPosition;
                }
            }
        }
	}
}