//
// • Ama Motion Automatiser
// • [ Curve Selector ]
// • By Amaryne Bréand
//

using AMA;
using System;
using UnityEditor;
using UnityEngine;

public class AMACurveSelector : EditorWindow
{
    const int buttonPerRow = 4;
    const float buttonSize = 160f;
    const float padding = 5f;

    Curves[] allCurves;
    Vector2 scrollPos;

    public static event Action<Curves> OnSelect;

    public static void OpenCurveSelectionWindow()
    {
        GetWindow<AMACurveSelector>("Curve selection").Init();
    }

    void Init()
    {
        // Get all curves
        allCurves = (Curves[])Enum.GetValues(typeof(Curves));
    }

    private void OnGUI()
    {
        // Label - indication
        GUILayout.Space(10);
        GUILayout.Label("Select a curve", EditorStyles.boldLabel);
        GUILayout.Space(10);

        // Set up buttons
        int nbCurves = allCurves.Length;
        int rows = Mathf.CeilToInt(nbCurves / (float)buttonPerRow);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos); // Needed to scroll
        // Display all buttons
        for (int y = 0; y < rows; y++)
        {
            EditorGUILayout.BeginHorizontal();

            // Display buttons 4 by 4
            for (int x = 0; x < buttonPerRow; x++)
            {
                int index = y * buttonPerRow + x;
                if (index >= nbCurves) break;

                Curves curve = allCurves[index];

                // Set up button style
                GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 11
                };

                // When button is pressed
                if (GUILayout.Button(curve.ToString(), buttonStyle, GUILayout.Width(buttonSize), GUILayout.Height(buttonSize)))
                {
                    OnCurveSelected(curve);
                }
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.Space(padding);
        }
        EditorGUILayout.EndScrollView();
    }

    private void OnCurveSelected(Curves _selectedCurve)
    {
        OnSelect?.Invoke(_selectedCurve); // Apply changes
        OnSelect = null;

        GetWindow<AMACurveSelector>().Close();
    }
}