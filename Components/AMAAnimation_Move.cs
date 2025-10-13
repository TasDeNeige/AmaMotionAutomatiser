using AMA;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class AMAAnimation_Move : MonoBehaviour
{
    #region In inspector
    [Header("Main settings")]
    [SerializeField] public AMA.Axis axisToAnimate = Axis.All;
    [SerializeField] public Vector3 endValue = Vector3.one;
    [SerializeField, Tooltip("In seconds")] public float animationDuration = 1f;
    [SerializeField] public bool playAnimationOnStart = false;

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

    Transform objToTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objToTransform = transform;

        if (playAnimationOnStart) PlayAnimation();
    }

    public void PlayAnimation()
    {
        // Create animation
        AMAMain.MA<Vector3> newMA = transform.AMAmove(axisToAnimate, endValue, animationDuration);

        /* Add curves */    if (curve != Curves.Linear) { if (curve == Curves.CUSTOM) newMA.SetCurve(customCurve); else newMA.SetCurve(curve); }
        /* Func On Start */ if (addFunctionOnStart) newMA.OnStart(startFunction.Invoke);
        /* Func On End */   if (addFunctionOnEnd) newMA.OnEnd(endFunction.Invoke);
        /* From Value */    if (addFromValue) newMA.From(fromValue);
        /* Add Delay */     if (addDelay) newMA.SetDelay(delay);
    }

    public void TestFunc()
    {
        Debug.Log("Test func called");
    }
}

[CustomEditor(typeof(AMAAnimation_Move))]
public class AMAAnimationEditor : Editor
{
    SerializedProperty addFunctionOnStartProp, startFunctionProp;
    SerializedProperty addFunctionOnEndProp, endFunctionProp;
    SerializedProperty useCustomCurveProp, customCurveProp;

    void OnEnable()
    {
        // Link serialized properties to their names in the target class
        useCustomCurveProp = serializedObject.FindProperty("curve");
        customCurveProp = serializedObject.FindProperty("customCurve");

        addFunctionOnStartProp = serializedObject.FindProperty("addFunctionOnStart");
        startFunctionProp = serializedObject.FindProperty("startFunction");

        addFunctionOnEndProp = serializedObject.FindProperty("addFunctionOnEnd");
        endFunctionProp = serializedObject.FindProperty("endFunction");
    }

    public override void OnInspectorGUI()
    {
        AMAAnimation_Move script = (AMAAnimation_Move)target;

        serializedObject.Update();
        DrawDefaultInspector();

        #region SerializedProperties
        EditorGUILayout.PropertyField(useCustomCurveProp, new GUIContent("Curve"));
        if (useCustomCurveProp.intValue == (int)Curves.CUSTOM) EditorGUILayout.PropertyField(customCurveProp, new GUIContent("Custom curve"), true);

        EditorGUILayout.PropertyField(addFunctionOnStartProp, new GUIContent("Add Function on Anim. Start"));
        if (addFunctionOnStartProp.boolValue) EditorGUILayout.PropertyField(startFunctionProp, new GUIContent("Functions on Start"), true);

        EditorGUILayout.PropertyField(addFunctionOnEndProp, new GUIContent("Add Function on Anim. End"));
        if (addFunctionOnEndProp.boolValue) EditorGUILayout.PropertyField(endFunctionProp, new GUIContent("Functions on End"), true);

        // Apply serialized changes
        serializedObject.ApplyModifiedProperties();
        #endregion

        #region Manually controlled fields
        script.addDelay = EditorGUILayout.Toggle("Add Delay to Anim.", script.addDelay);
        if (script.addDelay) script.delay = EditorGUILayout.FloatField("Delay", script.delay);

        script.addFromValue = EditorGUILayout.Toggle("Change Anim. starting point", script.addFromValue);
        if (script.addFromValue) script.fromValue = EditorGUILayout.Vector3Field("From Value", script.fromValue);
        #endregion
    }
}
