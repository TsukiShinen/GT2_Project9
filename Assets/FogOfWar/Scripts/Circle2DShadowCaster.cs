using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Circle2DShadowCaster : Collider2DShadowCaster
{
    [Range(3, 30)]
    [SerializeField] private int _samplesPoints = 20;

    public override Vector3[][] GetPoints()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        Vector3[] points = new Vector3[_samplesPoints];

        float stepAngle = 360 / _samplesPoints;

        for (int i = 0; i < _samplesPoints; i++)
        {
            float angle = stepAngle * i;
            points[i] = transform.TransformPoint(DirFromAngle(angle) * collider.radius);
        }

        return new Vector3[][] { points };

    }

    private Vector2 DirFromAngle(float angleInDegrees)
    {
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
