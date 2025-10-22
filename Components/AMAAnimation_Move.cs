//
// • Ama Motion Automatiser
// • [ Animation Move Component ]
// • By Amaryne Bréand
//

using AMA;
using UnityEditor;
using UnityEngine;

public class AMAAnimation_Move : AMABasicComponent<Vector3>
{
    public enum Space { World, Local };

    #region In inspector
    [Header("Main settings")]
    [HideInInspector] public AMA.Axis axisToAnimate = AMA.Axis.All;
    [HideInInspector] public Vector3 endValue = Vector3.one;
    [HideInInspector, Tooltip("In seconds")] public float animationDuration = 1f;
    [HideInInspector] public Space space = Space.World;
    [HideInInspector] public bool addCustomTransform = false;
    [HideInInspector] public Transform customTransform;
    [HideInInspector] public bool playAnimationOnStart = false;

    [Header("Miscellaneous")]
    [HideInInspector] public bool addFromValue;
    [HideInInspector] public Vector3 fromValue = Vector3.zero;
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
        switch(space)
        {
            case Space.World: newMA = (addCustomTransform ? customTransform : transform).AMAmove(axisToAnimate, endValue, animationDuration); break;
            case Space.Local: newMA = (addCustomTransform ? customTransform : transform).AMAlocalMove(axisToAnimate, endValue, animationDuration); break;
            default: newMA = (addCustomTransform ? customTransform : transform).AMAmove(axisToAnimate, endValue, animationDuration); break;
        }

        AddMisc(ref newMA);
        // From Value
        if (addFromValue) newMA.From(fromValue);
    }

    public Vector3 GetCurrentPosition()
    {
        // Depending on space
        switch (space)
        {
            case Space.World: return transform.position; break;
            case Space.Local: return transform.localPosition; break;
            default: return transform.position; break;
        }
    }
}

[CustomEditor(typeof(AMAAnimation_Move))]
class AMAAnimationMoveEditor : AMAComponentEditor<Vector3>
{
    SerializedProperty axisToAnimateProp;
    SerializedProperty spaceProp;
    SerializedProperty customTransformProp;

    Texture banner;
    string bannerPath = "AMA_AnimationComponentBanner";

    #region Editor
    void OnEnable()
    {
        // Link serialized properties to their names in the target class
        axisToAnimateProp = serializedObject.FindProperty("axisToAnimate");
        spaceProp = serializedObject.FindProperty("space");
        customTransformProp = serializedObject.FindProperty("customTransform");

        SetUpOnEnable(serializedObject);

        // Load banner
        banner = (Texture)Resources.Load(bannerPath, typeof(Texture));
    }

    public override void OnInspectorGUI()
    {
        AMAAnimation_Move script = (AMAAnimation_Move)target;

        SetUpOnInspector(serializedObject, banner);

        #region Components drawing
        #region Main Settings
        // Axis
        EditorGUILayout.PropertyField(axisToAnimateProp, new GUIContent("Axis to animate"), true);
        
        // End value
        DisplayVector3("End value:", ref script.endValue, script.axisToAnimate, script);
        
        // Anim duration
        script.animationDuration = EditorGUILayout.FloatField("Anim. Duration", script.animationDuration);
       
        // Space
        EditorGUILayout.PropertyField(spaceProp, new GUIContent("Space"), true);
        
        // Play anim on start
        script.playAnimationOnStart = EditorGUILayout.Toggle("Play anim. on Start()", script.playAnimationOnStart);
        
        // Custom transform
        script.addCustomTransform = EditorGUILayout.Toggle("Use another Transform", script.addCustomTransform);
        if (script.addCustomTransform) EditorGUILayout.PropertyField(customTransformProp, new GUIContent("Custom Transform"), true);
        #endregion

        #region Misc Settings
        DrawMisc(serializedObject, script);

        script.addFromValue = EditorGUILayout.Toggle("Change Anim. starting value", script.addFromValue);
        if (script.addFromValue) DisplayVector3("Starting value:", ref script.fromValue, script.axisToAnimate, script);
        #endregion

        // Apply serialized changes
        serializedObject.ApplyModifiedProperties();
        #endregion
    }
    #endregion
}