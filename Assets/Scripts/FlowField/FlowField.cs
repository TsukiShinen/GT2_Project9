using System.Collections.Generic;
using UnityEngine;

public class FlowField
{
    private readonly GameParameters _parameters;
    private readonly LayerMask _terrainMasks;

    public readonly Cell[,] Grid;
    private Vector2Int _gridSize;
    private Cell _destinationCell;

    private readonly float _cellRadius;
    private readonly float _cellDiameter;

    public FlowField(Cell[,] grid, Vector2Int gridSize, float cellSize, GameParameters parameters, LayerMask terrainMasks)
    {
        Grid = grid;
        _gridSize = gridSize;
        _cellRadius = cellSize / 2f;
        _cellDiameter = cellSize;

        _parameters = parameters;
        _terrainMasks = terrainMasks;
    }

    public void CreateIntegrationField(Cell destinationCell)
    {
        ResetBestCost();
        _destinationCell = destinationCell;

        var cost = _destinationCell.Cost;
        _destinationCell.Cost = 0;
        _destinationCell.BestCost = 0;

        var cellsToCheck = new Queue<Cell>();

        cellsToCheck.Enqueue(_destinationCell);

        while(cellsToCheck.Count > 0)
        {
            var cell = cellsToCheck.Dequeue();
            var neighbors = GridController.GetNeighborCells(Grid, cell.GridIndex, GridDirection.CardinalDirections);
            foreach (var neighbor in neighbors)
            {
                if (neighbor.Cost == byte.MaxValue) { continue; }
                if (neighbor.Cost + cell.BestCost >= neighbor.BestCost) { continue; }
                
                neighbor.BestCost = (ushort)(neighbor.Cost + cell.BestCost);
                cellsToCheck.Enqueue(neighbor);
            }
        }
        _destinationCell.BestDirection = GridDirection.None;
        _destinationCell.Cost = cost;
    }

    private void ResetBestCost()
    {
        for (var x = 0; x < _gridSize.x; x++)
        {
            for (var y = 0; y < _gridSize.y; y++)
            {
                Grid[x, y].BestCost = ushort.MaxValue;
            }
        }
    }

    public void CreateFlowField()
    {
        foreach(var cell in Grid)
        {
            var neighbors = GridController.GetNeighborCells(Grid, cell.GridIndex, GridDirection.AllDirection);

            int bestCost = cell.BestCost;

            foreach (var neighbor in neighbors)
            {
                if (neighbor.BestCost >= bestCost) { continue; }
                    
                bestCost = neighbor.BestCost;
                cell.BestDirection = GridDirection.GetDirectionFromV2I(neighbor.GridIndex - cell.GridIndex);
            }
        }
    }

    
}
