using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public enum DebugShow
{
    Cost,
    BestCost,
    Algo
}
#endif

public class GridController : MonoBehaviour
{
    [SerializeField] private GameParameters parameters;
    [SerializeField] private LayerMask terrainMasks;

    public Vector2Int gridSize;
    public float cellSize = 1f;

#if UNITY_EDITOR
    [Header("Debug")] 
    [SerializeField] private bool activateDebug;
    [SerializeField] private DebugShow debugshow;
#endif

    private Cell[,] _grid;
    private float CellDiameter => cellSize;
    private float CellRadius => cellSize / 2f;
    
    
    private FlowField _currentFlowField;

    //A*
    //private AStar _currentAstar;

    private void InitializeFlowField()
    {
        _currentFlowField = new FlowField(_grid, gridSize, cellSize, parameters, terrainMasks);
    }
    
    private void CreateGrid()
    {
        _grid = new Cell[gridSize.x, gridSize.y];

        for (var x = 0; x < gridSize.x; x++)
        {
            for (var y = 0; y < gridSize.y; y++)
            {
                var worldPos = new Vector2(CellDiameter * x + CellRadius, CellDiameter * y + CellRadius);
                _grid[x, y] = new Cell(worldPos, new Vector2Int(x, y));
            }
        }
    }

    private void CreateCostField()
    {

        foreach (var cell in _grid)
        {
            var origin = new Vector3(cell.WorldPosition.x, cell.WorldPosition.y, -10);
            var size = new Vector2(CellDiameter -0.1f, CellDiameter-0.1f);
            var hit = Physics2D.BoxCast(origin, size, 0f, Vector3.forward, 20f, terrainMasks);
            
            if (hit.collider == null) { continue; }
            if (hit.collider.gameObject.layer == parameters.LayerNavigationObstacleAsLayer)
            {
                cell.IncreaseCost(255);
            }
            else if(hit.collider.gameObject.layer == parameters.GroundNormalAsLayer)
            {
                cell.IncreaseCost(1);
            } 
            else if (hit.collider.gameObject.layer == parameters.LayerGroundSlowAsLayer)
            {
                cell.IncreaseCost(2);
            }
        }
    }

    private void Start()
    {
        CreateGrid();
        CreateCostField();
        InitializeFlowField();

        //A*
        //_currentAstar = new AStar(_grid, gridSize, cellSize, new Cell(new Vector2(CellDiameter * 11 + CellRadius, CellDiameter * 11 + CellRadius), new Vector2Int(11, 11)));
        //_currentAstar.CreateAStar(new Cell(new Vector2(CellDiameter * 0 + CellRadius, CellDiameter * 0 + CellRadius), new Vector2Int(0, 0)));
    }

    public Cell[,] GenerateFlowField(Vector2 position)
    {

        var destinationCell = GetCellFromWorldPosition(_grid, cellSize, position);
        _currentFlowField.CreateIntegrationField(destinationCell);
        _currentFlowField.CreateFlowField();
        return _currentFlowField.Grid;
    }


    public static List<Cell> GetNeighborCells(Cell[,] grid, Vector2Int nodeIndex, List<GridDirection> directions)
    {
        var neighbors = new List<Cell>();

        foreach (var direction in directions)
        {
            var newNeighbor = GetCellAtRelativePosition(grid, nodeIndex, direction);
            if (newNeighbor == null) { continue; }
            neighbors.Add(newNeighbor);
        }
        
        return neighbors;
    }

    private static Cell GetCellAtRelativePosition(Cell[,] grid, Vector2Int originPosition, Vector2Int relativePosition)
    {
        var finalPosition = originPosition + relativePosition;

        if (finalPosition.x < 0 || finalPosition.x >= grid.GetLength(0) || finalPosition.y < 0 || finalPosition.y >= grid.GetLength(1)) { return null; }

        return grid[finalPosition.x, finalPosition.y];
    }
    
    public static Cell GetCellFromWorldPosition(Cell[,] grid, float cellSize, Vector2 worldPosition)
    {
        var percentX = worldPosition.x / (grid.GetLength(0) * cellSize);
        var percentY = worldPosition.y / (grid.GetLength(1) * cellSize);

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        var x = Mathf.Clamp(Mathf.FloorToInt((grid.GetLength(0)) * percentX), 0, grid.GetLength(0) - 1); 
        var y = Mathf.Clamp(Mathf.FloorToInt((grid.GetLength(1)) * percentY), 0, grid.GetLength(1) - 1);
        return grid[x, y];
    }

    #region Gizmos

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!activateDebug) { return; }
        
        if (_currentFlowField != null)
        {
            var style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;

            switch (debugshow)
            {
                case DebugShow.Cost:
                    DrawGrid(Color.green);
                    foreach (var cell in _currentFlowField.Grid)
                    {
                        Handles.Label(cell.WorldPosition, cell.Cost.ToString(), style);
                    }
                    break;
                case DebugShow.BestCost:
                    DrawGrid(Color.green);
                    foreach (var cell in _currentFlowField.Grid)
                    {
                        Handles.Label(cell.WorldPosition, cell.BestCost.ToString(), style);
                    }
                    break;
                case DebugShow.Algo:
                    foreach (var cell in _currentFlowField.Grid)
                    {
                        var to = cell.WorldPosition + new Vector2(cell.BestDirection.Vector.x, cell.BestDirection.Vector.y) / 2;
                        Gizmos.DrawLine(cell.WorldPosition, to);
                    }
                    break;
            }
        }
        else
        {
            DrawGrid(Color.yellow);
            
        }

        
    }

    // A*
    //private void DrawPath(Color color)
    //{
    //    if (_currentAstar != null)
    //    {
    //        foreach (var cell in _currentAstar.Path)
    //        {
    //            var center = new Vector2(cellSize * cell.GridIndex.x + cellSize / 2, cellSize * cell.GridIndex.y + cellSize / 2);
    //            var size = Vector2.one * cellSize;
    //            Gizmos.DrawCube(center, size);
    //        }
    //    }
    //}

    private void DrawGrid(Color color)
    {
        Gizmos.color = color;
        for (var x = 0; x < gridSize.x; x++)
        {
            for (var y = 0; y < gridSize.y; y++)
            {
                var center = new Vector2(cellSize * x + cellSize / 2, cellSize * y + cellSize / 2);
                var size = Vector2.one * cellSize;
                Gizmos.DrawWireCube(center, size);
            }
        }
    }
#endif
    #endregion
}
