using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private GameParameters _parameters;
    [SerializeField] private LayerMask _terrainMasks;

    public Vector2Int GridSize;
    public float CellSize = 1f;
    public FlowField currentFlowField;

    private void InitializeFlowField()
    {
        currentFlowField = new FlowField(GridSize, CellSize, _parameters, _terrainMasks);
        currentFlowField.CreateGrid();
    }

    private void Start()
    {
    }

    public void GenerateFlowField()
    {
        InitializeFlowField();

        currentFlowField.CreateCostField();

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Cell destinationCell = currentFlowField.GetCellFromWorldPosition(mousePosition);
        currentFlowField.CreateIntegrationField(destinationCell);
        currentFlowField.CreateFlowField();
    }

    #region Helpers

    private void OnDrawGizmos()
    {
        if (currentFlowField != null)
        {
            DrawGrid(Color.green);

            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;

            foreach (Cell cell in currentFlowField.Grid)
            {
                Handles.Label(cell.WorldPosition, cell.bestCost.ToString(), style);
            }
        } else
        {
            DrawGrid(Color.yellow);
        }
    }

    private void DrawGrid(Color color)
    {
        Gizmos.color = color;
        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                Vector2 center = new Vector2(CellSize * x + CellSize / 2, CellSize * y + CellSize / 2);
                Vector2 size = Vector2.one * CellSize;
                Gizmos.DrawWireCube(center, size);
            }
        }
    }
    #endregion
}
