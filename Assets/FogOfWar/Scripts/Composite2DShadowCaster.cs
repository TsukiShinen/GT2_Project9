using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CompositeCollider2D))]
public class Composite2DShadowCaster : Collider2DShadowCaster
{
    public override Vector3[][] GetPoints()
    {
        CompositeCollider2D collider = GetComponent<CompositeCollider2D>();

        Vector3[][] points = new Vector3[collider.pathCount][];

        for (int i = 0; i < points.Length; i++)
        {
            Vector2[] pathVertices = new Vector2[collider.GetPathPointCount(i)];
            collider.GetPath(i, pathVertices);
            points[i] = pathVertices.Select(v2 => (Vector3)v2).ToArray();
        }
        
        return points;
    }
}
