/*using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private readonly Cell[,] _grid;
    private readonly List<Cell> _cellsFound;
    private readonly List<Cell> _path;

    private List<Cell> _cells;
    private readonly List<Cell> _cellsChecked;

    private Cell _destinationCell;
    private Cell _origin;

    public AStar(Cell[,] grid, Cell destinationCell)
    {
        _grid = grid;
        _destinationCell = destinationCell;
        _cellsFound = new List<Cell>();
        _cellsChecked = new List<Cell>();
        _path = new List<Cell>();
}

    public void CreateAStar(Cell initialPos)
    {
        _cells = GridController.GetNeighborCells(_grid, initialPos.GridIndex, GridDirection.CardinalAndInterCardinalDirections);
        _origin = initialPos;
        _cellsFound.Add(_grid[initialPos.GridIndex.x, initialPos.GridIndex.y]);
        Loop(initialPos.GridIndex);
    }

    private float CheckHTile(Cell cell)
    {
        float cost = 0;
        var position = Vector2.zero;
        var distance = new Vector2(Mathf.Abs(cell.GridIndex.x - _destinationCell.GridIndex.x), Mathf.Abs(cell.GridIndex.y - _destinationCell.GridIndex.y));
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

    private float CheckGTile(Cell cell, Vector2Int initialPos)
    {
        float cost = 0;
        var position = Vector2.zero;
        var distance = new Vector2(Mathf.Abs(cell.GridIndex.x - initialPos.x), Mathf.Abs(cell.GridIndex.y - initialPos.y));
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

    private void AddToList(Cell cell)
    {
        var tmpList = GridController.GetNeighborCells(_grid, cell.GridIndex, GridDirection.CardinalAndInterCardinalDirections);
        foreach (var neighborCell in tmpList)
        {
            if (!_cells.Contains(neighborCell) && neighborCell.Cost < 255 && !_cellsChecked.Contains(neighborCell))
            {
                _cells.Add(neighborCell);
            }
        }
    }

    private void AddToList2(Cell cell)
    {
        var tmpList = GridController.GetNeighborCells(_grid, cell.GridIndex, GridDirection.CardinalAndInterCardinalDirections);
        foreach (var neighborCell in tmpList)
        {
            if (!_cells.Contains(neighborCell) && neighborCell.Cost < 255 && !_cellsChecked.Contains(neighborCell) && _cellsFound.Contains(neighborCell))
            {
                _cells.Add(neighborCell);
            }
        }
    }

    private Cell FindMinimalCell(Vector2Int initialPos)
    {
        var cost = Mathf.Infinity;
        var index = 0;
        for (var x = 0; x < _cells.Count; x++)
        {
            if (CheckHTile(_cells[x]) + CheckGTile(_cells[x], initialPos) < cost)
            {
                cost = CheckHTile(_cells[x]) + CheckGTile(_cells[x], initialPos);
                index = x;
            }
        }
        var tmpCell = _cells[index];
        return tmpCell;
    }
    private void Loop(Vector2Int initialPos)
    {
        var tmpCell = FindMinimalCell(initialPos);
        _cells.Remove(tmpCell);
        _cellsChecked.Add(tmpCell);
        AddToList(tmpCell);
        _cellsFound.Add(tmpCell);
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

    private void FinalLoop(Vector2Int initialPos)
    {
        if(initialPos == new Vector2Int(2, 2))
        {
            Debug.Log("test");
        }
        var tmpCell = FindMinimalCell(initialPos);
        _cells.Remove(tmpCell);
        _cellsChecked.Add(tmpCell);
        
        AddToList2(tmpCell);
        _path.Add(tmpCell);
        if (tmpCell.GridIndex != _destinationCell.GridIndex)
        {
            FinalLoop(tmpCell.GridIndex);
        }
        else
        {
            _path.Reverse();
        }
    }
}
*/