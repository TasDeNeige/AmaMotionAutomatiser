using AMA;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class AMAAnimation_Move : MonoBehaviour
{
    #region In inspector
    [Header("Main settings")]
    [HideInInspector] public AMA.Axis axisToAnimate = Axis.All;
    [HideInInspector] public Vector3 endValue = Vector3.one;
    [HideInInspector, Tooltip("In seconds")] public float animationDuration = 1f;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playAnimationOnStart) PlayAnimation();
    }

    public void PlayAnimation()
    {
        // Create animation
        AMAMain.MA<Vector3> newMA = transform.AMAmove(axisToAnimate, endValue, animationDuration);

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

    public void TestFunc()
    {
        Debug.Log("Test func called");
    }

    public Curves GetCurveToUpdate() { return curve; }
}

[CustomEditor(typeof(AMAAnimation_Move))]
public class AMAAnimationMoveEditor : AMAComponentEditor
{
    SerializedProperty axisToAnimateProp;
    SerializedProperty addFunctionOnStartProp, startFunctionProp;
    SerializedProperty addFunctionOnEndProp, endFunctionProp;
    SerializedProperty useCustomCurveProp, customCurveProp;
    Texture banner;
    string bannerPath = "AMA_AnimationComponentBanner";

    void OnEnable()
    {
        // Link serialized properties to their names in the target class
        axisToAnimateProp = serializedObject.FindProperty("axisToAnimate");

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

        // Draw banner
        if (banner != null)
        {
            float imageWidth = EditorGUIUtility.currentViewWidth;
            float imageHeight = imageWidth * banner.height / banner.width;
            Rect rect = GUILayoutUtility.GetRect(imageWidth, imageHeight);
            GUI.DrawTexture(rect, banner, ScaleMode.ScaleToFit);
        }

        ComponentSetUp(serializedObject);

        #region Components drawing
        #region Main Settings
        EditorGUILayout.PropertyField(axisToAnimateProp, new GUIContent("Axis to animate"), true);

        #region End value
        EditorGUILayout.LabelField("End Value");

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(EditorGUIUtility.labelWidth); // Align to right
        float fieldWidth = (EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth) / 3f - 6;

        // Toggle X
        GUI.enabled = script.axisToAnimate == Axis.All ? true : script.axisToAnimate == Axis.x ? true : false;
        script.endValue.x = EditorGUILayout.FloatField(script.endValue.x, GUILayout.Width(fieldWidth));
        GUI.enabled = true;

        // Toggle Y
        GUI.enabled = script.axisToAnimate == Axis.All ? true : script.axisToAnimate == Axis.y ? true : false;
        script.endValue.y = EditorGUILayout.FloatField(script.endValue.y, GUILayout.Width(fieldWidth));
        GUI.enabled = true;

        // Toggle Z
        GUI.enabled = script.axisToAnimate == Axis.All ? true : script.axisToAnimate == Axis.z ? true : false;
        script.endValue.z = EditorGUILayout.FloatField(script.endValue.z, GUILayout.Width(fieldWidth));
        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();
        #endregion
        #endregion

        // Curve selection button
        if (GUILayout.Button("Select curve"))
        {
            AMACurveSelector.OpenCurveSelectionWindow(ref script.curve);
        }

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
        #endregion
    }
}