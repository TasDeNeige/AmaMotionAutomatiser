using System.Linq;
using UnityEditor;

public class AMAComponentEditor : Editor
{
    /// <summary>
    /// Sets up component. Needs to be called in OnInspectorGUI()
    /// </summary>
    /// <param name="_serializedObject"></param>
    public void ComponentSetUp(SerializedObject _serializedObject)
    {
        _serializedObject.DrawInspectorExcept("m_Script");
    }
}

static class AMADrawInspectorExcept
{
    public static void DrawInspectorExcept(this SerializedObject serializedObject, string fieldToSkip)
    {
        serializedObject.DrawInspectorExcept(new string[1] { fieldToSkip });
    }

    public static void DrawInspectorExcept(this SerializedObject serializedObject, string[] fieldsToSkip)
    {
        serializedObject.Update();
        SerializedProperty prop = serializedObject.GetIterator();
        if (prop.NextVisible(true))
        {
            do
            {
                if (fieldsToSkip.Any(prop.name.Contains))
                    continue;

                EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name), true);
            }
            while (prop.NextVisible(false));
        }
        serializedObject.ApplyModifiedProperties();
    }
}