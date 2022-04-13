/*using System.Collections.Generic;
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
    [SerializeField] private DebugShow debugShow;
#endif

    private float CellDiameter => cellSize;
    private float CellRadius => cellSize / 2f;
    
    
    public FlowField _currentFlowField;

    //A*
    //private AStar _currentAstar;

    private void InitializeFlowField()
    {
        _currentFlowField = new FlowField(_grid, gridSize, cellSize, parameters, terrainMasks);
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

    #region Gizmos

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!activateDebug) { return; }
        
        if (_currentFlowField != null)
        {
            var style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;

            switch (debugShow)
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
*/