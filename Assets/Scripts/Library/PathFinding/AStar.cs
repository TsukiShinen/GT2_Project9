using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
	public class AStar : PathFinding
	{
        private List<Cell> _cellsFound;
        private List<Cell> _cells;
        private List<Cell> _cellsChecked;

        private Cell _origin;
        private Cell _destination;
        private List<Vector3> _path;
        public override Queue<Vector3> GeneratePath(Vector2 startingPosition, Vector2 destinationPosition)
		{
			CreateAStar(startingPosition, destinationPosition);
            return Path;
		}

        public void CreateAStar(Vector2 startingPosition, Vector2 destinationPosition)
        {
            _cellsFound = new List<Cell>();
            _cellsChecked = new List<Cell>();
            _path = new List<Vector3>();

            var initialPos = GetCellFromWorldPosition(startingPosition);
            _cells = GetNeighborCells(initialPos.GridIndex, GridDirection.CardinalAndInterCardinalDirections);
            _origin = initialPos;
            _destination = GetCellFromWorldPosition(destinationPosition);
            _cellsFound.Add(Grid[initialPos.GridIndex.x, initialPos.GridIndex.y]);
            InitialToDest(initialPos.GridIndex);
        }

        private float CheckHTile(Cell cell)
        {
            float cost = 0;
            var position = Vector2.zero;
            var distance = new Vector2(Mathf.Abs(cell.GridIndex.x - _destination.GridIndex.x), Mathf.Abs(cell.GridIndex.y - _destination.GridIndex.y));
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
            var tmpList = GetNeighborCells(cell.GridIndex, GridDirection.CardinalAndInterCardinalDirections);
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
            var tmpList = GetNeighborCells(cell.GridIndex, GridDirection.CardinalAndInterCardinalDirections);
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
        private void InitialToDest(Vector2Int initialPos)
        {
            var tmpCell = FindMinimalCell(initialPos);
            _cells.Remove(tmpCell);
            _cellsChecked.Add(tmpCell);
            AddToList(tmpCell);
            _cellsFound.Add(tmpCell);
            if (tmpCell.GridIndex == _destination.GridIndex)
            {
                _destination = _origin;
                _cells.Clear();
                _cellsChecked.Clear();
                AddToList2(tmpCell);
                DestToInitial(tmpCell.GridIndex);

            }
            else
            {
                InitialToDest(tmpCell.GridIndex);
            }
        }

        private void DestToInitial(Vector2Int initialPos)
        {
            if (initialPos == new Vector2Int(2, 2))
            {
                Debug.Log("test");
            }
            var tmpCell = FindMinimalCell(initialPos);
            _cells.Remove(tmpCell);
            _cellsChecked.Add(tmpCell);

            AddToList2(tmpCell);
            _path.Add(tmpCell.WorldPosition);
            if (tmpCell.GridIndex != _destination.GridIndex)
            {
                DestToInitial(tmpCell.GridIndex);
            }
            else
            {
                _path.Reverse();
                Path = new Queue<Vector3>(_path);
            }
        }
    }
}