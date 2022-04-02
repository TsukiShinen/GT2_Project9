using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowField
{
    private GameParameters _parameters;
    private LayerMask _terrainMasks;

    public Cell[,] Grid { get; private set; }
    public Vector2Int GridSize { get; private set; }
    public float CellRadius { get; private set; }
    public Cell DestinationCell;

    private float _cellDiameter;

    public FlowField(Vector2Int gridSize, float cellSize, GameParameters parameters, LayerMask terrainMasks)
    {
        GridSize = gridSize;
        CellRadius = cellSize / 2f;
        _cellDiameter = cellSize;

        _parameters = parameters;
        _terrainMasks = terrainMasks;
    }

    public void CreateGrid()
    {
        Grid = new Cell[GridSize.x, GridSize.y];

        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                Vector2 worldPos = new Vector2(_cellDiameter * x + CellRadius, _cellDiameter * y + CellRadius);
                Grid[x, y] = new Cell(worldPos, new Vector2Int(x, y));
            }
        }
    }

    public void CreateCostField()
    {

        foreach (Cell cell in Grid)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector3(cell.WorldPosition.x, cell.WorldPosition.y, -10), Vector3.forward, 20f, _terrainMasks);
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
        DestinationCell = destinationCell;

        byte cost = DestinationCell.Cost;
        DestinationCell.Cost = 0;
        DestinationCell.bestCost = 0;

        Queue<Cell> cellsToCheck = new Queue<Cell>();

        cellsToCheck.Enqueue(DestinationCell);

        while(cellsToCheck.Count > 0)
        {
            Cell cell = cellsToCheck.Dequeue();
            List<Cell> neighbors = GetNeightborCells(cell.GridIndex, GridDirection.CaridinalDirections);
            foreach (Cell neighbor in neighbors)
            {
                if (neighbor.Cost == byte.MaxValue) { continue; }
                if (neighbor.Cost + cell.bestCost < neighbor.bestCost)
                {
                    neighbor.bestCost = (ushort)(neighbor.Cost + cell.bestCost);
                    cellsToCheck.Enqueue(neighbor);
                }
            }
        }

        DestinationCell.Cost = cost;
    }

    public void CreateFlowField()
    {
        foreach(Cell cell in Grid)
        {
            List<Cell> neightbors = GetNeightborCells(cell.GridIndex, GridDirection.AllDirection);

            int bestCost = cell.bestCost;

            foreach (Cell neighbor in neightbors)
            {
                if (neighbor.bestCost < bestCost)
                {
                    bestCost = neighbor.bestCost;
                    cell.BestDirection = GridDirection.GetDirectionFromV2I(neighbor.GridIndex - cell.GridIndex);
                }
            }
        }
    }

    private List<Cell> GetNeightborCells(Vector2Int nodeIndex, List<GridDirection> directions)
    {
        List<Cell> neightbors = new List<Cell>();

        foreach (GridDirection direction in directions)
        {
            Cell newNeightbor = GetCellAtRelativePosition(nodeIndex, direction);
            if (newNeightbor != null)
            {
                neightbors.Add(newNeightbor);
            }
        }
        return neightbors;
    }

    private Cell GetCellAtRelativePosition(Vector2Int originPosition, Vector2Int relativePosition)
    {
        Vector2Int finalPosition = originPosition + relativePosition;

        if (finalPosition.x < 0 || finalPosition.x >= GridSize.x || finalPosition.y < 0 || finalPosition.y >= GridSize.y) { return null; }

        return Grid[finalPosition.x, finalPosition.y];
    }

    public Cell GetCellFromWorldPosition(Vector2 worldPosition)
    {
        float percentX = worldPosition.x / (GridSize.x * _cellDiameter);
        float percentY = worldPosition.y / (GridSize.y * _cellDiameter);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.Clamp(Mathf.FloorToInt((GridSize.x) * percentX), 0, GridSize.x - 1); 
        int y = Mathf.Clamp(Mathf.FloorToInt((GridSize.y) * percentY), 0, GridSize.y - 1);
        return Grid[x, y];
    }
}
