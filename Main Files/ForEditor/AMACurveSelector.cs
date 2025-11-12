//
// • Ama Motion Automatiser
// • [ Curve Selector ]
// • By Amaryne Bréand
//

using AMA;
using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class AMACurveSelector : EditorWindow
{
    const int buttonPerRow = 4;
    const float buttonSize = 160f;
    const float padding = 5f;

    Curves[] allCurves;
    int nbCurves;
    Vector2 scrollPos;
    Texture2D[] previews;

    public static event Action<Curves> OnSelect;
    GUIStyle labelStyle;
    GUIStyle buttonStyle;

    public static void OpenCurveSelectionWindow()
    {
        GetWindow<AMACurveSelector>("Curve selection").Init();
    }

    void Init()
    {
        // Get all curves
        allCurves = (Curves[])Enum.GetValues(typeof(Curves));
        nbCurves = allCurves.Length;

        // Get previews
        previews = new Texture2D[nbCurves];

        for (int i =  0; i < nbCurves; i++)
        {
            // Load preview images
            previews[i] = Resources.Load<Texture2D>($"CurvesPreview/{allCurves[i]}");
        }

        #region Styles
        // Set up button style
        buttonStyle = new GUIStyle(GUI.skin.button)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 11
        };

        // Set up label style
        labelStyle = new GUIStyle(EditorStyles.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 10,
            wordWrap = true
        };
        #endregion
    }

    private void OnGUI()
    {
        // Indication label
        GUILayout.Space(10);
        GUILayout.Label("Select a curve", EditorStyles.boldLabel);
        GUILayout.Space(10);

        // Set up buttons
        int startIndex = 2;
        int endIndex = nbCurves - 2;
        int visibleCount = endIndex - startIndex + 1; // Nb buttons to draw
        int rows = Mathf.CeilToInt(visibleCount / (float)buttonPerRow);
        int currentIndex = startIndex;

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos); // Needed to scroll

        #region First row
        EditorGUILayout.BeginHorizontal();
        DrawButton(allCurves[0]); // Draw Linear curve
        DrawButton(allCurves[1]); // Draw Constant curve
        GUILayout.FlexibleSpace();
        DrawButton(allCurves[nbCurves - 1]); // Draw Custom curve
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(padding * 2);
        #endregion

        for (int y = 0; y < rows; y++)
        {
            EditorGUILayout.BeginHorizontal();

            // Display buttons 4 by 4
            for (int x = 0; x < buttonPerRow; x++)
            {
                //int index = y * buttonPerRow + x;
                if (currentIndex > endIndex) break;

                Curves curve = allCurves[currentIndex];
                DrawButton(curve);
                currentIndex++;
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.Space(padding);
        }

        EditorGUILayout.EndScrollView();
    }

    void DrawButton(Curves _curve)
    {
        // Draw button content (image + label)
        GUILayout.BeginVertical(GUILayout.Width(buttonSize), GUILayout.Height(buttonSize + 20)); // +20 for label

        // Image button
        if (previews[(int)_curve] != null)
        {
            if (GUILayout.Button(previews[(int)_curve], GUILayout.Width(buttonSize), GUILayout.Height(buttonSize)))
            {
                OnCurveSelected(_curve);
            }
        }
        // If no image
        else
        {
            if (GUILayout.Button(_curve.ToString(), GUILayout.Width(buttonSize), GUILayout.Height(buttonSize)))
            {
                OnCurveSelected(_curve);
            }
        }

        // Display curve name below
        GUILayout.Label(_curve.ToString(), labelStyle, GUILayout.Width(buttonSize));

        GUILayout.EndVertical();
    }


    private void OnCurveSelected(Curves _selectedCurve)
    {
        OnSelect?.Invoke(_selectedCurve); // Apply changes
        OnSelect = null;

        GetWindow<AMACurveSelector>().Close();
    }
}
#endif