using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private readonly GameParameters _parameters;
    private readonly LayerMask _terrainMasks;

    public readonly Cell[,] Grid;
    public List<Cell> CellsFound;
    public List<Cell> Path;

    private List<Cell> _cells;
    private List<Cell> _cellsChecked;

    private Vector2Int _gridSize;
    private Cell _destinationCell;
    private Cell _origin;

    private readonly float _cellRadius;
    private readonly float _cellDiameter;

    public AStar(Cell[,] grid, Vector2Int gridSize, float cellSize, Cell destinationCell)
    {
        Grid = grid;
        _gridSize = gridSize;
        _cellRadius = cellSize / 2f;
        _cellDiameter = cellSize;
        _destinationCell = destinationCell;
        CellsFound = new List<Cell>();
        _cellsChecked = new List<Cell>();
        Path = new List<Cell>();
}

    public void CreateAStar(Cell initalPos)
    {
        _cells = GridController.GetNeighborCells(Grid, initalPos.GridIndex, GridDirection.CardinalAndInterCardinalDirections);
        _origin = initalPos;
        CellsFound.Add(Grid[initalPos.GridIndex.x, initalPos.GridIndex.y]);
        Loop(initalPos.GridIndex);
    }

    public float CheckHTile(Cell cell)
    {
        float cost = 0;
        Vector2 position = Vector2.zero;
        Vector2 distance = new Vector2(Mathf.Abs(cell.GridIndex.x - _destinationCell.GridIndex.x), Mathf.Abs(cell.GridIndex.y - _destinationCell.GridIndex.y));
        while (position.x < distance.x && position.y < distance.y)
        {
            position.x += 1;
            position.y += 1;
            cost += 14;
        }
        if (position.x < distance.x)
        {
            cost += 10 * (distance.x - position.x);
        }
        if (position.y < distance.y)
        {
            cost += 10 * (distance.y - position.y);
        }
        return cost;
    }

    public float CheckGTile(Cell cell, Vector2Int initalPos)
    {
        float cost = 0;
        Vector2 position = Vector2.zero;
        Vector2 distance = new Vector2(Mathf.Abs(cell.GridIndex.x - initalPos.x), Mathf.Abs(cell.GridIndex.y - initalPos.y));
        while (position.x < distance.x && position.y < distance.y)
        {
            position.x += 1;
            position.y += 1;
            cost += 14;
        }
        if (position.x < distance.x)
        {
            cost += 10 * (distance.x - position.x);
        }
        if (position.y < distance.y)
        {
            cost += 10 * (distance.y - position.y);
        }
        return cost;
    }

    public void AddToList(Cell cell)
    {
        List<Cell> tmpList = GridController.GetNeighborCells(Grid, cell.GridIndex, GridDirection.CardinalAndInterCardinalDirections);
        foreach (Cell neighborcell in tmpList)
        {
            if (!_cells.Contains(neighborcell) && neighborcell.Cost < 255 && !_cellsChecked.Contains(neighborcell))
            {
                _cells.Add(neighborcell);
            }
        }
    }

    public void AddToList2(Cell cell)
    {
        List<Cell> tmpList = GridController.GetNeighborCells(Grid, cell.GridIndex, GridDirection.CardinalAndInterCardinalDirections);
        foreach (Cell neighborcell in tmpList)
        {
            if (!_cells.Contains(neighborcell) && neighborcell.Cost < 255 && !_cellsChecked.Contains(neighborcell) && CellsFound.Contains(neighborcell))
            {
                _cells.Add(neighborcell);
            }
        }
    }

    public Cell FindMinimalCell(Vector2Int initalPos)
    {
        float cost = Mathf.Infinity;
        int index = 0;
        for (var x = 0; x < _cells.Count; x++)
        {
            if (CheckHTile(_cells[x]) + CheckGTile(_cells[x], initalPos) < cost)
            {
                cost = CheckHTile(_cells[x]) + CheckGTile(_cells[x], initalPos);
                index = x;
            }
        }
        Cell tmpCell = _cells[index];
        return tmpCell;
    }
    public void Loop(Vector2Int initalPos)
    {
        Cell tmpCell = FindMinimalCell(initalPos);
        _cells.Remove(tmpCell);
        _cellsChecked.Add(tmpCell);
        AddToList(tmpCell);
        CellsFound.Add(tmpCell);
        if (tmpCell.GridIndex == _destinationCell.GridIndex)
        {
            _destinationCell = _origin;
            _cells.Clear();
            _cellsChecked.Clear();
            AddToList2(tmpCell);
            FinalLoop(tmpCell.GridIndex);

        }
        else
        {
            Loop(tmpCell.GridIndex);
        }
    }

    public void FinalLoop(Vector2Int initalPos)
    {
        if(initalPos == new Vector2Int(2, 2))
        {
            Debug.Log("test");
        }
        Cell tmpCell = FindMinimalCell(initalPos);
        _cells.Remove(tmpCell);
        _cellsChecked.Add(tmpCell);
        
        AddToList2(tmpCell);
        Path.Add(tmpCell);
        if (tmpCell.GridIndex != _destinationCell.GridIndex)
        {
            FinalLoop(tmpCell.GridIndex);
        }
        else
        {
            Path.Reverse();
        }
    }
}
