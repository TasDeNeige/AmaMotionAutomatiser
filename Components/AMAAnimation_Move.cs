//
// • Ama Motion Automatiser
// • [ Animation Move Component ]
// • By Amaryne Bréand
//

using AMA;
using UnityEditor;
using UnityEngine;

public class AMAAnimation_Move : AMABasicComponent
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
        // Prevent bugs with no custom transform given
        if (addCustomTransform && customTransform == null)
        {
            Debug.LogWarning(AMAMain.debugAlertString + "Custom Transform was activated, but no transform was given in " + transform.name + ". Please add one in the component or thanks to a call to the 'AssignCustomTransform' function before the 'PlayAnimation'.");
            return;
        }

        // Create animation
        AMAMain.MA<Vector3> newMA;

        // Depending on space
        switch(space)
        {
            case Space.World: newMA = (addCustomTransform ? customTransform : transform).AMAmove(axisToAnimate, endValue, animationDuration, snapToEndValue); break;
            case Space.Local: newMA = (addCustomTransform ? customTransform : transform).AMAlocalMove(axisToAnimate, endValue, animationDuration, snapToEndValue); break;
            default: newMA = (addCustomTransform ? customTransform : transform).AMAmove(axisToAnimate, endValue, animationDuration, snapToEndValue); break;
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
            case Space.World: return transform.position;
            case Space.Local: return transform.localPosition;
            default: return transform.position;
        }
    }

    public void AssignCustomTransform(Transform _customTransform) { customTransform = _customTransform; }
}

#if UNITY_EDITOR
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

        // Start value
        script.addFromValue = EditorGUILayout.Toggle("Change Start value", script.addFromValue);
        DisplayVector3("Starting value:", ref script.fromValue, script.axisToAnimate, script, script.addFromValue);

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

        DrawMisc(serializedObject, script);

        // Apply serialized changes
        serializedObject.ApplyModifiedProperties();
        #endregion
    }

    #region Tools
    // Display Vector3 with fields greyed out according to selected axis
    void DisplayVector3(string _nameToDisplay, ref Vector3 _vector, AMA.Axis _axis, AMAAnimation_Move _script, bool _enable = true)
    {
        GUI.enabled = _enable;
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
        GUI.enabled = !_enable ? false : _axis == AMA.Axis.All ? true : _axis == AMA.Axis.x ? true : false;
        _vector.x = EditorGUILayout.FloatField(_vector.x, GUILayout.Width(fieldWidth));
        GUI.enabled = true;

        // Toggle Y
        GUI.enabled = !_enable ? false : _axis == AMA.Axis.All ? true : _axis == AMA.Axis.y ? true : false;
        _vector.y = EditorGUILayout.FloatField(_vector.y, GUILayout.Width(fieldWidth));
        GUI.enabled = true;

        // Toggle Z
        GUI.enabled = !_enable ? false : _axis == AMA.Axis.All ? true : _axis == AMA.Axis.z ? true : false;
        _vector.z = EditorGUILayout.FloatField(_vector.z, GUILayout.Width(fieldWidth));
        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();
        GUI.enabled = true;
    }
    #endregion
    #endregion
}
#endif