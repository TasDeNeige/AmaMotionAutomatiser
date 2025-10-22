//
// • Ama Motion Automatiser
// • [ Component-Specific code ]
// • By Amaryne Bréand
//

using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace AMA
{
    public class AMABasicComponent<T> : MonoBehaviour
    {
        [HideInInspector] public AMA.Curves curve = Curves.Linear;
        [HideInInspector] public AnimationCurve customCurve;
        [HideInInspector] public bool addFunctionOnStart;
        [HideInInspector] public UnityEvent startFunction;
        [HideInInspector] public bool addFunctionOnEnd;
        [HideInInspector] public UnityEvent endFunction;
        [HideInInspector] public bool addDelay;
        [HideInInspector][Tooltip("In seconds")] public float delay = 0f;

        public void AddMisc(ref AMAMain.MA<T> _ma)
        {
            // Add Delay
            if (addDelay) _ma.SetDelay(delay);
            // Add curves 
            if (curve != Curves.Linear) { if (curve == Curves.CUSTOM) _ma.SetCurve(customCurve); else _ma.SetCurve(curve); }
            // Func On Start
            if (addFunctionOnStart) _ma.OnStart(startFunction.Invoke);
            // Func On End
            if (addFunctionOnEnd) _ma.OnEnd(endFunction.Invoke);
        }
    }

#if UNITY_EDITOR
    public class AMAComponentEditor<T> : Editor
    {
        public SerializedProperty addFunctionOnStartProp, startFunctionProp;
        public SerializedProperty addFunctionOnEndProp, endFunctionProp;
        public SerializedProperty useCustomCurveProp, customCurveProp;

        /// <summary>
        /// Sets up component. Needs to be called in OnEnable()
        /// </summary>
        /// <param name="_serializedObject"></param>
        public void SetUpOnEnable(SerializedObject _serializedObject)
        {
            useCustomCurveProp = _serializedObject.FindProperty("curve");
            customCurveProp = _serializedObject.FindProperty("customCurve");

            addFunctionOnStartProp = _serializedObject.FindProperty("addFunctionOnStart");
            startFunctionProp = _serializedObject.FindProperty("startFunction");

            addFunctionOnEndProp = _serializedObject.FindProperty("addFunctionOnEnd");
            endFunctionProp = _serializedObject.FindProperty("endFunction");
        }

        /// <summary>
        /// Sets up component. Needs to be called in OnInspectorGUI()
        /// </summary>
        /// <param name="_serializedObject"></param>
        public void SetUpOnInspector(SerializedObject _serializedObject, UnityEngine.Texture banner = null)
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

        /// <summary>
        /// Draws generic Miscellaneous settings. Needs to be called in OnInspectorGUI()
        /// </summary>
        /// <param name="_serializedObject"></param>
        /// <param name="_script"></param>
        public void DrawMisc(SerializedObject _serializedObject, AMABasicComponent<T> _script)
        {
            EditorGUILayout.Space();
            GUILayout.Label("Miscellaneous", EditorStyles.boldLabel);

            // Curve
            #region Curve related
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(useCustomCurveProp, new GUIContent("Curve"));

            // Curve selection button
            if (GUILayout.Button("Select curve"))
            {
                AMACurveSelector.OpenCurveSelectionWindow();
                AMACurveSelector.OnSelect += CurveChange;
            }
            EditorGUILayout.EndHorizontal();
            if (useCustomCurveProp.intValue == (int)Curves.CUSTOM) EditorGUILayout.PropertyField(customCurveProp, new GUIContent("Custom curve"), true);
            #endregion

            // Delay
            _script.addDelay = EditorGUILayout.Toggle("Add Delay to Anim.", _script.addDelay);
            if (_script.addDelay) _script.delay = EditorGUILayout.FloatField("Delay", _script.delay);

            // Func on Start
            EditorGUILayout.PropertyField(addFunctionOnStartProp, new GUIContent("Add Func. on Anim. Start"));
            if (addFunctionOnStartProp.boolValue) EditorGUILayout.PropertyField(startFunctionProp, new GUIContent("Functions on Start"), true);

            // Func on End
            EditorGUILayout.PropertyField(addFunctionOnEndProp, new GUIContent("Add Func. on Anim. End"));
            if (addFunctionOnEndProp.boolValue) EditorGUILayout.PropertyField(endFunctionProp, new GUIContent("Functions on End"), true);
        }

        #region Tools
        // Used for Curve Selection window
        void CurveChange(Curves _curve)
        {
            AMABasicComponent<T> script = (AMABasicComponent<T>)target;
            script.curve = _curve;
        }
        #endregion
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
#endif
}