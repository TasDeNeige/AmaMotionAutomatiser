//
// • Ama Motion Automatiser
// • [ Animation Move Component ]
// • By Amaryne Bréand
//

using AMA;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class AMAAnimation_Move : MonoBehaviour
{
    public enum Space { World, Local };

    #region In inspector
    [Header("Main settings")]
    [HideInInspector] public AMA.Axis axisToAnimate = AMA.Axis.All;
    [HideInInspector] public Vector3 endValue = Vector3.one;
    [HideInInspector, Tooltip("In seconds")] public float animationDuration = 1f;
    [HideInInspector] public Space space = Space.World;
    [HideInInspector] public bool playAnimationOnStart = false;

    [Header("Miscellaneous")]
    [HideInInspector] public AMA.Curves curve = Curves.Linear;
    [HideInInspector] public AnimationCurve customCurve;
    [HideInInspector] public bool addFunctionOnStart;
    [HideInInspector] public UnityEvent startFunction;
    [HideInInspector] public bool addFunctionOnEnd;
    [HideInInspector] public UnityEvent endFunction;
    [HideInInspector] public bool addFromValue;
    [HideInInspector] public Vector3 fromValue = Vector3.zero;
    [HideInInspector] public bool addDelay;
    [HideInInspector][Tooltip("In seconds")] public float delay = 0f;
    #endregion

    void Start()
    {
        if (playAnimationOnStart) PlayAnimation();
    }

    public void PlayAnimation()
    {
        // Create animation
        AMAMain.MA<Vector3> newMA;

        // Depending on space
        if (space == Space.Local) newMA = transform.AMAlocalMove(axisToAnimate, endValue, animationDuration);
        else newMA = transform.AMAmove(axisToAnimate, endValue, animationDuration);

        // Add curves 
        if (curve != Curves.Linear) { if (curve == Curves.CUSTOM) newMA.SetCurve(customCurve); else newMA.SetCurve(curve); }
        // Func On Start
        if (addFunctionOnStart) newMA.OnStart(startFunction.Invoke);
        // Func On End
        if (addFunctionOnEnd) newMA.OnEnd(endFunction.Invoke);
        // From Value
        if (addFromValue) newMA.From(fromValue);
        // Add Delay
        if (addDelay) newMA.SetDelay(delay);
    }

    public Vector3 GetCurrentPosition() { return transform.position; }
}

[CustomEditor(typeof(AMAAnimation_Move))]
class AMAAnimationMoveEditor : AMAComponentEditor
{
    SerializedProperty axisToAnimateProp;
    SerializedProperty spaceProp;
    SerializedProperty addFunctionOnStartProp, startFunctionProp;
    SerializedProperty addFunctionOnEndProp, endFunctionProp;
    SerializedProperty useCustomCurveProp, customCurveProp;
    Texture banner;
    string bannerPath = "AMA_AnimationComponentBanner";

    #region Editor
    void OnEnable()
    {
        // Link serialized properties to their names in the target class
        axisToAnimateProp = serializedObject.FindProperty("axisToAnimate");
        spaceProp = serializedObject.FindProperty("space");

        useCustomCurveProp = serializedObject.FindProperty("curve");
        customCurveProp = serializedObject.FindProperty("customCurve");

        addFunctionOnStartProp = serializedObject.FindProperty("addFunctionOnStart");
        startFunctionProp = serializedObject.FindProperty("startFunction");

        addFunctionOnEndProp = serializedObject.FindProperty("addFunctionOnEnd");
        endFunctionProp = serializedObject.FindProperty("endFunction");

        // Load banner
        banner = (Texture)Resources.Load(bannerPath, typeof(Texture));
    }

    public override void OnInspectorGUI()
    {
        AMAAnimation_Move script = (AMAAnimation_Move)target;

        ComponentSetUp(serializedObject, banner);

        #region Components drawing
        #region Main Settings
        EditorGUILayout.PropertyField(axisToAnimateProp, new GUIContent("Axis to animate"), true);
        DisplayVector3("End value:", ref script.endValue, script.axisToAnimate, script);
        script.animationDuration = EditorGUILayout.FloatField("Anim. Duration", script.animationDuration);
        EditorGUILayout.PropertyField(spaceProp, new GUIContent("Space"), true);
        script.playAnimationOnStart = EditorGUILayout.Toggle("Play anim. on Start()", script.playAnimationOnStart);
        #endregion

        #region SerializedProperties
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(useCustomCurveProp, new GUIContent("Curve"));

        // Curve selection button
        EditorGUILayout.Space();
        if (GUILayout.Button("Select curve"))
        {
            AMACurveSelector.OpenCurveSelectionWindow();
            AMACurveSelector.OnSelect += CurveChange;
        }
        EditorGUILayout.EndHorizontal();
        if (useCustomCurveProp.intValue == (int)Curves.CUSTOM) EditorGUILayout.PropertyField(customCurveProp, new GUIContent("Custom curve"), true);

        EditorGUILayout.PropertyField(addFunctionOnStartProp, new GUIContent("Add Func. on Anim. Start"));
        if (addFunctionOnStartProp.boolValue) EditorGUILayout.PropertyField(startFunctionProp, new GUIContent("Functions on Start"), true);

        EditorGUILayout.PropertyField(addFunctionOnEndProp, new GUIContent("Add Func. on Anim. End"));
        if (addFunctionOnEndProp.boolValue) EditorGUILayout.PropertyField(endFunctionProp, new GUIContent("Functions on End"), true);

        // Apply serialized changes
        serializedObject.ApplyModifiedProperties();
        #endregion

        #region Manually controlled fields
        script.addDelay = EditorGUILayout.Toggle("Add Delay to Anim.", script.addDelay);
        if (script.addDelay) script.delay = EditorGUILayout.FloatField("Delay", script.delay);

        script.addFromValue = EditorGUILayout.Toggle("Change Anim. starting value", script.addFromValue);
        if (script.addFromValue) DisplayVector3("Starting value:", ref script.fromValue, script.axisToAnimate, script);
        #endregion
        #endregion
    }
    #endregion

    #region Method
    void CurveChange(Curves _curve)
    {
        AMAAnimation_Move script = (AMAAnimation_Move)target; // uh
        script.curve = _curve;
    }

    void DisplayVector3(string _nameToDisplay, ref Vector3 _vector, AMA.Axis _axis, AMAAnimation_Move _script)
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