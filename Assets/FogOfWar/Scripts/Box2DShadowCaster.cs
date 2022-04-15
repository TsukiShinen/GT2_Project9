using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Box2DShadowCaster : Collider2DShadowCaster
{
    public override Vector3[][] GetPoints()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        Vector3[] points = new Vector3[4];
        points[0] = new Vector3(collider.bounds.center.x - collider.bounds.extents.x, collider.bounds.center.y - collider.bounds.extents.y);
        points[1] = new Vector3(collider.bounds.center.x - collider.bounds.extents.x, collider.bounds.center.y + collider.bounds.extents.y);
        points[2] = new Vector3(collider.bounds.center.x + collider.bounds.extents.x, collider.bounds.center.y + collider.bounds.extents.y);
        points[3] = new Vector3(collider.bounds.center.x + collider.bounds.extents.x, collider.bounds.center.y - collider.bounds.extents.y);
        return new Vector3[][] { points };

    }
}
