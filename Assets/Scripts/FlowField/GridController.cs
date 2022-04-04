using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private GameParameters parameters;
    [SerializeField] private LayerMask terrainMasks;

    public Vector2Int gridSize;
    public float cellSize = 1f;
    public FlowField CurrentFlowField;

    private void InitializeFlowField()
    {
        CurrentFlowField = new FlowField(gridSize, cellSize, parameters, terrainMasks);
        CurrentFlowField.CreateGrid();
    }

    private void Start()
    {
        InitializeFlowField();
        CurrentFlowField.CreateCostField();
    }

    public void GenerateFlowField(Vector2 position)
    {
        var destinationCell = CurrentFlowField.GetCellFromWorldPosition(position);
        CurrentFlowField.CreateIntegrationField(destinationCell);
        CurrentFlowField.CreateFlowField();
    }

    #region Gizmos

    private void OnDrawGizmos()
    {
        if (CurrentFlowField != null)
        {
            //DrawGrid(Color.green);

            var style = new GUIStyle(GUI.skin.label);
            style.alignment = TextAnchor.MiddleCenter;

            foreach (var cell in CurrentFlowField.Grid)
            {
                //Handles.Label(cell.WorldPosition, cell.Cost.ToString(), style);
                var to = cell.WorldPosition + new Vector2(cell.BestDirection.Vector.x, cell.BestDirection.Vector.y) / 2;
                Gizmos.DrawLine(cell.WorldPosition, new Vector3(to.x, to.y));
            }
        }
        else
        {
            //DrawGrid(Color.yellow);
        }
    }

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
    #endregion
}
