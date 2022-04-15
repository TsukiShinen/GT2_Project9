using System.Reflection;
using System.Linq;

using UnityEngine;
using UnityEditor;

#if UNITY_2021_1_OR_NEWER
using UnityEngine.Rendering.Universal;
#else
using UnityEngine.Experimental.Rendering.Universal;
#endif

[CustomEditor(typeof(Collider2DShadowCaster))]
public class Collider2DShadowCasterEditor : Editor
{
    static readonly FieldInfo _shapePathHash = typeof(ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly FieldInfo _shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);

    private static class Styles
    {
        public static GUIContent selfShadows = EditorGUIUtility.TrTextContent("Self Shadows", "When enabled, the Renderer casts shadows on itself.");
        public static GUIContent castsShadows = EditorGUIUtility.TrTextContent("Casts Shadows", "Specifies if this renderer will cast shadows");
        public static GUIContent sortingLayerPrefixLabel = EditorGUIUtility.TrTextContent("Target Sorting Layers", "Apply shadows to the specified sorting layers.");
    }

    private Collider2DShadowCaster _shadowCaster;
    private SerializedProperty _applyToSortingLayersProp;
    private SerializedProperty _castsShadowsProp;
    private SerializedProperty _selfShadowProp;
    private SortingLayerDropDown _sortingLayerDropDown;

    private void OnEnable()
    {
        _shadowCaster = (Collider2DShadowCaster)target;

        _castsShadowsProp = serializedObject.FindProperty("m_CastsShadows");
        _selfShadowProp = serializedObject.FindProperty("m_SelfShadows");
        _applyToSortingLayersProp = serializedObject.FindProperty("m_ApplyToSortingLayers");

        _sortingLayerDropDown = new SortingLayerDropDown();
        _sortingLayerDropDown.OnEnable(serializedObject, "m_ApplyToSortingLayers");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        using (var changedScope = new EditorGUI.ChangeCheckScope())
        {
            EditorGUILayout.PropertyField(_castsShadowsProp, Styles.castsShadows);
            EditorGUILayout.PropertyField(_selfShadowProp, Styles.selfShadows);
            if (changedScope.changed)
            {
                Generate();
            }
        }
        
        _sortingLayerDropDown.OnTargetSortingLayers(serializedObject, targets, Styles.sortingLayerPrefixLabel, (_) => Generate());

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate"))
        {
            Generate();
        }
        if (GUILayout.Button("Clear"))
        {
            Clear();
        }
        EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }

    protected void Generate()
    {
        Clear();

        Vector3[][] points = _shadowCaster.GetPoints();

        for (int i = 0; i < points.Length; i++)
        {
            GameObject shadowCaster = new GameObject($"shadow_caster_{i}");
            shadowCaster.transform.parent = _shadowCaster.transform;
            ShadowCaster2D shadowCasterComponent = shadowCaster.AddComponent<ShadowCaster2D>();
            shadowCasterComponent.castsShadows = _castsShadowsProp.boolValue;
            shadowCasterComponent.selfShadows = _selfShadowProp.boolValue;

            var obj = new SerializedObject(shadowCasterComponent);
            var prop = obj.FindProperty("m_ApplyToSortingLayers");

            prop.ClearArray();

            for (int j = 0; j < _applyToSortingLayersProp.arraySize; j++)
            {
                var elem = _applyToSortingLayersProp.GetArrayElementAtIndex(j);
                prop.InsertArrayElementAtIndex(j);
                prop.GetArrayElementAtIndex(j).intValue = elem.intValue;
            }

            obj.ApplyModifiedProperties();

            _shapePathField.SetValue(shadowCasterComponent, points[i]);
            _shapePathHash.SetValue(shadowCasterComponent, -1);
        }
    }

    protected void Clear()
    {
        var tempList = _shadowCaster.transform.Cast<Transform>().ToList();
        foreach (var child in tempList)
        {
            if (child.TryGetComponent(out ShadowCaster2D _))
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }
}