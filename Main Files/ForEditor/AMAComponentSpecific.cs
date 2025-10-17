//
// • Ama Motion Automatiser
// • [ Component-Specific code ]
// • By Amaryne Bréand
//

using System.Linq;
using UnityEditor;
using UnityEngine;

public class AMAComponentEditor : Editor
{
    /// <summary>
    /// Sets up component. Needs to be called in OnInspectorGUI()
    /// </summary>
    /// <param name="_serializedObject"></param>
    public void ComponentSetUp(SerializedObject _serializedObject, UnityEngine.Texture banner = null)
    {
        _serializedObject.DrawInspectorExcept("m_Script");

        // Draw banner
        if (banner != null)
        {
            float imageWidth = EditorGUIUtility.currentViewWidth;
            float imageHeight = imageWidth * banner.height / banner.width;
            Rect rect = GUILayoutUtility.GetRect(imageWidth, imageHeight);
            GUI.DrawTexture(rect, banner, ScaleMode.ScaleToFit);
        }
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