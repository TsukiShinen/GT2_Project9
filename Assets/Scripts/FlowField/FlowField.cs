using System.Collections.Generic;
using UnityEngine;

public class FlowField
{
    private readonly GameParameters _parameters;
    private readonly LayerMask _terrainMasks;

    public Cell[,] Grid { get; private set; }
    private Vector2Int _gridSize;
    private Cell _destinationCell;

    private readonly float _cellRadius;
    private readonly float _cellDiameter;

    public FlowField(Vector2Int gridSize, float cellSize, GameParameters parameters, LayerMask terrainMasks)
    {
        _gridSize = gridSize;
        _cellRadius = cellSize / 2f;
        _cellDiameter = cellSize;

        _parameters = parameters;
        _terrainMasks = terrainMasks;
    }

    public void CreateGrid()
    {
        Grid = new Cell[_gridSize.x, _gridSize.y];

        for (var x = 0; x < _gridSize.x; x++)
        {
            for (var y = 0; y < _gridSize.y; y++)
            {
                var worldPos = new Vector2(_cellDiameter * x + _cellRadius, _cellDiameter * y + _cellRadius);
                Grid[x, y] = new Cell(worldPos, new Vector2Int(x, y));
            }
        }
    }

    public void CreateCostField()
    {

        foreach (var cell in Grid)
        {
            var hit = Physics2D.Raycast(new Vector3(cell.WorldPosition.x, cell.WorldPosition.y, -10), Vector3.forward, 20f, _terrainMasks);
            if (hit.collider == null) { continue; }
            if (hit.collider.gameObject.layer == _parameters.LayerNavigationObstacleAsLayer)
            {
                cell.IncreaseCost(255);
            }
            else if(hit.collider.gameObject.layer == _parameters.GroundNormalAsLayer)
            {
                cell.IncreaseCost(1);
            } 
            else if (hit.collider.gameObject.layer == _parameters.LayerGroundSlowAsLayer)
            {
                cell.IncreaseCost(2);
            }
        }
    }

    public void CreateIntegrationField(Cell destinationCell)
    {
        ResetBestCost();
        _destinationCell = destinationCell;

        var cost = _destinationCell.Cost;
        _destinationCell.Cost = 0;
        _destinationCell.BestCost = 0;

        Queue<Cell> cellsToCheck = new Queue<Cell>();

        cellsToCheck.Enqueue(_destinationCell);

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

    private List<Cell> GetNeighborCells(Vector2Int nodeIndex, List<GridDirection> directions)
    {
        var neighbors = new List<Cell>();

        foreach (var direction in directions)
        {
            var newNeighbor = GetCellAtRelativePosition(nodeIndex, direction);
            if (newNeighbor == null) { continue; }
            neighbors.Add(newNeighbor);
        }
        
        return neighbors;
    }

    private Cell GetCellAtRelativePosition(Vector2Int originPosition, Vector2Int relativePosition)
    {
        var finalPosition = originPosition + relativePosition;

        if (finalPosition.x < 0 || finalPosition.x >= _gridSize.x || finalPosition.y < 0 || finalPosition.y >= _gridSize.y) { return null; }

        return Grid[finalPosition.x, finalPosition.y];
    }

    public Cell GetCellFromWorldPosition(Vector2 worldPosition)
    {
        var percentX = worldPosition.x / (_gridSize.x * _cellDiameter);
        var percentY = worldPosition.y / (_gridSize.y * _cellDiameter);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        var x = Mathf.Clamp(Mathf.FloorToInt((_gridSize.x) * percentX), 0, _gridSize.x - 1); 
        var y = Mathf.Clamp(Mathf.FloorToInt((_gridSize.y) * percentY), 0, _gridSize.y - 1);
        return Grid[x, y];
    }
}
