using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
	public class AStar : PathFinding
	{
        public override Queue<Vector3> GeneratePath(Vector2 startingPosition, Vector2 destinationPosition)
        {
            Dictionary<Cell, Cell> NextCellToGoal = new Dictionary<Cell, Cell>();//Determines for each Cell where you need to go to reach the goal. Key=Cell, Value=Direction to Goal
            Dictionary<Cell, int> costToReachCell = new Dictionary<Cell, int>();//Total Movement Cost to reach the Cell

            StartingCell = GetCellFromWorldPosition(startingPosition);
            DestinationCell = GetCellFromWorldPosition(destinationPosition);

            PriorityQueue<Cell> frontier = new PriorityQueue<Cell>();
            frontier.Enqueue(DestinationCell, 0);
            costToReachCell[DestinationCell] = 0;

            while (frontier.Count > 0)
            {
                Cell curCell = frontier.Dequeue();
                if (curCell == StartingCell)
                    break;

                foreach (Cell neighbor in GetNeighborCells(curCell.GridIndex, GridDirection.CardinalAndInterCardinalDirections))
                {
                    int newCost = costToReachCell[curCell] + neighbor.Cost;
                    if (costToReachCell.ContainsKey(neighbor) == false || newCost < costToReachCell[neighbor])
                    {
                        if (neighbor.Cost < 255)
                        {
                            costToReachCell[neighbor] = newCost;
                            int priority = newCost + Distance(neighbor, StartingCell);
                            frontier.Enqueue(neighbor, priority);
                            NextCellToGoal[neighbor] = curCell;
                            //neighbor._Text = costToReachCell[neighbor].ToString();
                        }
                    }
                }
            }

            //Get the Path

            //check if Cell is reachable
            if (NextCellToGoal.ContainsKey(StartingCell) == false)
            {
                return null;
            }

            Queue<Vector3> path = new Queue<Vector3>();
            Cell pathCell = StartingCell;
            while (DestinationCell != pathCell)
            {
                pathCell = NextCellToGoal[pathCell];
                path.Enqueue(pathCell.WorldPosition);
            }
            return path;

            int Distance(Cell c1, Cell c2)
            {
                return (int) (Mathf.Abs(c1.WorldPosition.x - c2.WorldPosition.x) + Mathf.Abs(c1.WorldPosition.y - c2.WorldPosition.y));
            }

        }
    }
}