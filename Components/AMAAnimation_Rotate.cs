//
// • Ama Motion Automatiser
// • [ Animation Rotation Component ]
// • By Amaryne Bréand
//

using AMA;
using UnityEditor;
using UnityEngine;

public class AMAAnimation_Rotate : AMABasicComponent
{
    public enum Type { Quaternion, Euler };

    #region In inspector
    [Header("Main settings")]
    [HideInInspector] public AMA.Axis axisToAnimate = AMA.Axis.All;
    [HideInInspector] public Quaternion endValueQuat = Quaternion.identity;
    [HideInInspector] public Vector3 endValueVec = Vector3.one;
    [HideInInspector, Tooltip("In seconds")] public float animationDuration = 1f;
    [HideInInspector] public Type type = Type.Quaternion;
    [HideInInspector] public bool addCustomTransform = false;
    [HideInInspector] public Transform customTransform;
    [HideInInspector] public bool playAnimationOnStart = false;

    [Header("Miscellaneous")]
    [HideInInspector] public bool addFromValue;
    [HideInInspector] public Quaternion fromValueQuat = Quaternion.identity;
    [HideInInspector] public Vector3 fromValueVec = Vector3.zero;
    #endregion

    void Start()
    {
        if (playAnimationOnStart) PlayAnimation();
    }

    public void PlayAnimation()
    {
        // Depending on space
        switch(type)
        {
            case Type.Quaternion:
                // Create animation
                AMAMain.MA<Quaternion> newMA1;

                // Set up animation
                newMA1 = (addCustomTransform ? customTransform : transform).AMArotate(axisToAnimate, endValueQuat, animationDuration);
                AddMisc(ref newMA1);

                // From Value
                if (addFromValue) newMA1.From(fromValueQuat);
                break;
                
            case Type.Euler:
                // Create Utilitary class

                // Create animation
                AMAMain.MA<Vector3> newMA2;

                // Set up animation
                newMA2 = (addCustomTransform ? customTransform : transform).AMArotateEuler(axisToAnimate, endValueVec, animationDuration);
                AddMisc(ref newMA2);

                // From Value
                if (addFromValue) newMA2.From(fromValueVec);
                break;

            default:
                // Create animation
                AMAMain.MA<Quaternion> newMA3;

                // Set up animation
                newMA3 = (addCustomTransform ? customTransform : transform).AMArotate(axisToAnimate, endValueQuat, animationDuration);
                AddMisc(ref newMA3);

                // From Value
                if (addFromValue) newMA3.From(fromValueQuat);
                break;
        }
    }

    public Quaternion GetCurrentQuaternionRotation() { return transform.rotation; }
    public Vector3 GetCurrentEulerRotation() { return transform.eulerAngles; }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AMAAnimation_Rotate))]
class AMAAnimationRotateEditor : AMAComponentEditor<Quaternion>
{
    SerializedProperty axisToAnimateProp;
    SerializedProperty typeProp;
    SerializedProperty customTransformProp;

    Texture banner;
    string bannerPath = "AMA_AnimationComponentBanner";

    #region Editor
    void OnEnable()
    {
        // Link serialized properties to their names in the target class
        axisToAnimateProp = serializedObject.FindProperty("axisToAnimate");
        typeProp = serializedObject.FindProperty("type");
        customTransformProp = serializedObject.FindProperty("customTransform");
        
        SetUpOnEnable(serializedObject);

        // Load banner
        banner = (Texture)Resources.Load(bannerPath, typeof(Texture));
    }

    public override void OnInspectorGUI()
    {
        AMAAnimation_Rotate script = (AMAAnimation_Rotate)target;

        SetUpOnInspector(serializedObject, banner);

        #region Components drawing
        #region Main Settings
        // Axis
        EditorGUILayout.PropertyField(axisToAnimateProp, new GUIContent("Axis to animate"), true);

        // Start value
        script.addFromValue = EditorGUILayout.Toggle("Change Start value", script.addFromValue);
        
        // Display according to type
        switch(script.type)
        {
            case AMAAnimation_Rotate.Type.Quaternion:
                // Start value
                DisplayQuaternion("Starting value:", ref script.fromValueQuat, script.axisToAnimate, script, script.addFromValue);

                // End value
                DisplayQuaternion("End value:", ref script.endValueQuat, script.axisToAnimate, script);
                break;

            case AMAAnimation_Rotate.Type.Euler:
                // Start value
                DisplayVector3("Starting value:", ref script.fromValueVec, script.axisToAnimate, script, script.addFromValue);

                // End value
                DisplayVector3("End value:", ref script.endValueVec, script.axisToAnimate, script);
                break;

            default:
                // Start value
                DisplayQuaternion("Starting value:", ref script.fromValueQuat, script.axisToAnimate, script, script.addFromValue);

                // End value
                DisplayQuaternion("End value:", ref script.endValueQuat, script.axisToAnimate, script);
                break;
        }
        
        // Anim duration
        script.animationDuration = EditorGUILayout.FloatField("Anim. Duration", script.animationDuration);
       
        // Space
        EditorGUILayout.PropertyField(typeProp, new GUIContent("Space"), true);
        
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
    void DisplayQuaternion(string _nameToDisplay, ref Quaternion _vector, AMA.Axis _axis, AMAAnimation_Rotate _script, bool _enable = true)
    {
        GUI.enabled = _enable;
        EditorGUILayout.LabelField(_nameToDisplay);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Get Current Position"))
        {
            Quaternion currentQuatRotation= _script.GetCurrentQuaternionRotation();

            switch (_axis)
            {
                case Axis.All: _vector = currentQuatRotation; break;
                case Axis.x: _vector.x = currentQuatRotation.x; break;
                case Axis.y: _vector.y = currentQuatRotation.y; break;
                case Axis.z: _vector.z = currentQuatRotation.z; break;
                /// ADD case Axis.w HERE !!!!!!!!!!!!!!!!
            }
        }

        float fieldWidth = (EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth) / 4f - 12;

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

        // Toggle W
        GUI.enabled = !_enable ? false : _axis == AMA.Axis.All ? true : false;
        _vector.w = EditorGUILayout.FloatField(_vector.w, GUILayout.Width(fieldWidth));
        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();
        GUI.enabled = true;
    }

    // Display Vector3 with fields greyed out according to selected axis
    void DisplayVector3(string _nameToDisplay, ref Vector3 _vector, AMA.Axis _axis, AMAAnimation_Rotate _script, bool _enable = true)
    {
        GUI.enabled = _enable;
        EditorGUILayout.LabelField(_nameToDisplay);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Get Current Position"))
        {
            Vector3 currentPos = _script.GetCurrentEulerRotation();

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