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
            // Add curves 
            if (curve != Curves.Linear) { if (curve == Curves.CUSTOM) _ma.SetCurve(customCurve); else _ma.SetCurve(curve); }
            // Func On Start
            if (addFunctionOnStart) _ma.OnStart(startFunction.Invoke);
            // Func On End
            if (addFunctionOnEnd) _ma.OnEnd(endFunction.Invoke);
            // Add Delay
            if (addDelay) _ma.SetDelay(delay);
        }
    }

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

            // Func on Start
            EditorGUILayout.PropertyField(addFunctionOnStartProp, new GUIContent("Add Func. on Anim. Start"));
            if (addFunctionOnStartProp.boolValue) EditorGUILayout.PropertyField(startFunctionProp, new GUIContent("Functions on Start"), true);

            // Func on End
            EditorGUILayout.PropertyField(addFunctionOnEndProp, new GUIContent("Add Func. on Anim. End"));
            if (addFunctionOnEndProp.boolValue) EditorGUILayout.PropertyField(endFunctionProp, new GUIContent("Functions on End"), true);

            // Delay
            _script.addDelay = EditorGUILayout.Toggle("Add Delay to Anim.", _script.addDelay);
            if (_script.addDelay) _script.delay = EditorGUILayout.FloatField("Delay", _script.delay);
        }

        #region Tools
        // Used for Curve Selection window
        void CurveChange(Curves _curve)
        {
            AMABasicComponent<T> script = (AMABasicComponent<T>)target;
            script.curve = _curve;
        }

        // Display Vector3 with fields greyed out according to selected axis
        public void DisplayVector3(string _nameToDisplay, ref Vector3 _vector, AMA.Axis _axis, AMAAnimation_Move _script)
        {
            EditorGUILayout.LabelField(_nameToDisplay);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Get Current Position"))
            {
                Vector3 currentPos = _script.GetCurrentPosition();

                switch (_axis)
                {
                    case Axis.All: _vector = currentPos; break;
                    case Axis.x: _vector.x = currentPos.x; break;
                    case Axis.y: _vector.y = currentPos.y; break;
                    case Axis.z: _vector.z = currentPos.z; break;
                }
            }

            float fieldWidth = (EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth) / 3f - 6;

            // Toggle X
            GUI.enabled = _axis == AMA.Axis.All ? true : _axis == AMA.Axis.x ? true : false;
            _vector.x = EditorGUILayout.FloatField(_vector.x, GUILayout.Width(fieldWidth));
            GUI.enabled = true;

            // Toggle Y
            GUI.enabled = _axis == AMA.Axis.All ? true : _axis == AMA.Axis.y ? true : false;
            _vector.y = EditorGUILayout.FloatField(_vector.y, GUILayout.Width(fieldWidth));
            GUI.enabled = true;

            // Toggle Z
            GUI.enabled = _axis == AMA.Axis.All ? true : _axis == AMA.Axis.z ? true : false;
            _vector.z = EditorGUILayout.FloatField(_vector.z, GUILayout.Width(fieldWidth));
            GUI.enabled = true;

            EditorGUILayout.EndHorizontal();
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
}