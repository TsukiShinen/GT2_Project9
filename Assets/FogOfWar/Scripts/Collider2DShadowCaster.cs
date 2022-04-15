using UnityEngine;

public abstract class Collider2DShadowCaster : MonoBehaviour
{
    [SerializeField] private bool m_CastsShadows = true;
    [SerializeField] private bool m_SelfShadows = true;
    [SerializeField] int[] m_ApplyToSortingLayers = null;

    public abstract Vector3[][] GetPoints();
}
