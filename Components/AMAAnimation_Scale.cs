//
// • Ama Motion Automatiser
// • [ Animation Scale Component ]
// • By Amaryne Bréand
//

using AMA;
using UnityEditor;
using UnityEngine;

public class AMAAnimation_Scale : AMABasicComponent
{
    #region In inspector
    [Header("Main settings")]
    [HideInInspector] public AMA.Axis axisToAnimate = AMA.Axis.All;
    [HideInInspector] public Vector3 endValue = Vector3.one;
    [HideInInspector, Tooltip("In seconds")] public float animationDuration = 1f;
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
        newMA = (addCustomTransform ? customTransform : transform).AMAscale(axisToAnimate, endValue, animationDuration, snapToEndValue);

        AddMisc(ref newMA);
        // From Value
        if (addFromValue) newMA.From(fromValue);
    }

    public Vector3 GetCurrentScale()
    {
        return transform.localScale;
    }

    public void AssignCustomTransform(Transform _customTransform) { customTransform = _customTransform; }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AMAAnimation_Scale))]
class AMAAnimationScaleEditor : AMAComponentEditor<Vector3>
{
    SerializedProperty axisToAnimateProp;
    SerializedProperty customTransformProp;

    Texture banner;
    string bannerPath = "AMA_AnimationComponentBanner";

    #region Editor
    void OnEnable()
    {
        // Link serialized properties to their names in the target class
        axisToAnimateProp = serializedObject.FindProperty("axisToAnimate");
        customTransformProp = serializedObject.FindProperty("customTransform");

        SetUpOnEnable(serializedObject);

        // Load banner
        banner = (Texture)Resources.Load(bannerPath, typeof(Texture));
    }

    public override void OnInspectorGUI()
    {
        AMAAnimation_Scale script = (AMAAnimation_Scale)target;

        SetUpOnInspector(serializedObject, banner);

        #region Components drawing
        #region Main Settings
        // Axis
        EditorGUILayout.PropertyField(axisToAnimateProp, new GUIContent("Axis to animate"), true);

        // Start value
        script.addFromValue = EditorGUILayout.Toggle("Change Anim. starting value", script.addFromValue);
        DisplayVector3("Starting value:", ref script.fromValue, script.axisToAnimate, script, script.addFromValue);

        // End value
        DisplayVector3("End value:", ref script.endValue, script.axisToAnimate, script);
        
        // Anim duration
        script.animationDuration = EditorGUILayout.FloatField("Anim. Duration", script.animationDuration);
       
        // Play anim on start
        script.playAnimationOnStart = EditorGUILayout.Toggle("Play anim. on Start()", script.playAnimationOnStart);
        
        // Custom transform
        script.addCustomTransform = EditorGUILayout.Toggle("Use another Transform", script.addCustomTransform);
        if (script.addCustomTransform) EditorGUILayout.PropertyField(customTransformProp, new GUIContent("Custom Transform"), true);
        #endregion

        #region Misc Settings
        DrawMisc(serializedObject, script);
        #endregion

        // Apply serialized changes
        serializedObject.ApplyModifiedProperties();
        #endregion
    }

    #region Tools
    // Display Vector3 with fields greyed out according to selected axis
    void DisplayVector3(string _nameToDisplay, ref Vector3 _vector, AMA.Axis _axis, AMAAnimation_Scale _script, bool _enable = true)
    {
        GUI.enabled = _enable;
        EditorGUILayout.LabelField(_nameToDisplay);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Get Current Position"))
        {
            Vector3 currentPos = _script.GetCurrentScale();

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