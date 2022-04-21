using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
	public class AStar : PathFinding
	{
        List<Cell> cellsChecked;
        public override Queue<Vector3> GeneratePath(Vector2 startingPosition, Vector2 destinationPosition)
        {
            var nextCellToGoal = new Dictionary<Cell, Cell>();//Determines for each Cell where you need to go to reach the goal. Key=Cell, Value=Direction to Goal
            var costToReachCell = new Dictionary<Cell, int>();//Total Movement Cost to reach the Cell
            cellsChecked = new List<Cell>();

            StartingCell = GetCellFromWorldPosition(startingPosition);
            DestinationCell = GetCellFromWorldPosition(destinationPosition);

            var frontier = new PriorityQueue<Cell>();
            frontier.Enqueue(DestinationCell, 0);
            costToReachCell[DestinationCell] = 0;

            while (frontier.Count > 0)
            {
                var curCell = frontier.Dequeue();
                if (curCell == StartingCell)
                    break;

                foreach (var neighbor in GetNeighborCells(curCell.GridIndex, GridDirection.CardinalAndInterCardinalDirections))
                {
                    cellsChecked.Add(neighbor);
                    var newCost = costToReachCell[curCell] + neighbor.Cost;
                    if (!(costToReachCell.ContainsKey(neighbor) == false || newCost < costToReachCell[neighbor])) continue;
                    if (neighbor.Cost >= 255) continue;
                    
                    costToReachCell[neighbor] = newCost;
                    var priority = newCost + Distance(neighbor, StartingCell);
                    frontier.Enqueue(neighbor, priority);
                    nextCellToGoal[neighbor] = curCell;
                }
            }

            //Get the Path

            Path = new Queue<Vector3>();
            //check if Cell is reachable
            if (nextCellToGoal.ContainsKey(StartingCell) == false)
            {
                return null;
            }
            
            var pathCell = StartingCell;
            while (DestinationCell != pathCell)
            {
                pathCell = nextCellToGoal[pathCell];
                Path.Enqueue(pathCell.WorldPosition);
            }
            return Path;

            int Distance(Cell c1, Cell c2)
            {
                return (int) (Mathf.Abs(c1.WorldPosition.x - c2.WorldPosition.x) + Mathf.Abs(c1.WorldPosition.y - c2.WorldPosition.y));
            }

        }
#if UNITY_EDITOR
        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            foreach(var cell in cellsChecked)
            {
                Gizmos.DrawCube(cell.WorldPosition, new Vector3(CellSize,CellSize));
            }
            Gizmos.color = Color.red;
            foreach (var position in Path)
            {
                Gizmos.DrawCube(position, new Vector3(CellSize, CellSize));
            }
        }
#endif
    }
}