using UnityEditor;

[CustomEditor(typeof(Circle2DShadowCaster))]
public class Circle2DShadowCasterEditor : Collider2DShadowCasterEditor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        using (var changedScope = new EditorGUI.ChangeCheckScope())
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_samplesPoints"));

            if (changedScope.changed)
            {
                serializedObject.ApplyModifiedProperties();
                Generate();
            }
        }

        EditorGUILayout.Space();

        base.OnInspectorGUI();
    }
}