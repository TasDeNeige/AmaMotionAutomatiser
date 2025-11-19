//
// • Ama Motion Automatiser
// • [ Animation UI Move Component ]
// • By Amaryne Bréand
//

using AMA;
using UnityEditor;
using UnityEngine;

public class AMAAnimation_Move_UI : AMABasicComponent
{
    public enum Space { World, Local, AnchoredPos, AnchoredPos3D };
    RectTransform rectTransform;

    #region In inspector
    [Header("Main settings")]
    [HideInInspector] public AMA.Axis axisToAnimate = AMA.Axis.All;
    [HideInInspector] public Vector3 endValue = Vector3.one;
    [HideInInspector, Tooltip("In seconds")] public float animationDuration = 1f;
    [HideInInspector] public Space space = Space.World;
    [HideInInspector] public bool addCustomRectTransform = false;
    [HideInInspector] public RectTransform customRectTransform;
    [HideInInspector] public bool playAnimationOnStart = false;

    [Header("Miscellaneous")]
    [HideInInspector] public bool addFromValue;
    [HideInInspector] public Vector3 fromValue = Vector3.zero;
    #endregion

    private void OnEnable() { rectTransform = GetComponent<RectTransform>(); }

    void Start()
    {
        if (playAnimationOnStart) PlayAnimation();
    }

    public void PlayAnimation()
    {
        // Prevent bugs with no custom transform given
        if (addCustomRectTransform && customRectTransform == null)
        {
            Debug.LogWarning(AMAMain.debugAlertString + "Custom RectTransform was activated, but no recttransform was given in " + transform.name + ". Please add one in the component or thanks to a call to the 'AssignCustomRectTransform' function before the 'PlayAnimation'.");
            return;
        }

        // Create animation
        AMAMain.MA<Vector3> newMA;

        // Depending on space
        switch(space)
        {
            case Space.World: newMA = (addCustomRectTransform ? customRectTransform : rectTransform).AMAmove(axisToAnimate, endValue, animationDuration, snapToEndValue); break;
            case Space.Local: newMA = (addCustomRectTransform ? customRectTransform : rectTransform).AMAlocalMove(axisToAnimate, endValue, animationDuration, snapToEndValue); break;
            case Space.AnchoredPos: newMA = (addCustomRectTransform ? customRectTransform : rectTransform).AMAanchoredPosMove(axisToAnimate, endValue, animationDuration, snapToEndValue); break;
            case Space.AnchoredPos3D: newMA = (addCustomRectTransform ? customRectTransform : rectTransform).AMAanchoredPos3dMove(axisToAnimate, endValue, animationDuration, snapToEndValue); break;
            default: newMA = (addCustomRectTransform ? customRectTransform : rectTransform).AMAmove(axisToAnimate, endValue, animationDuration, snapToEndValue); break;
        }

        AddMisc(ref newMA);
        // From Value
        if (addFromValue) newMA.From(fromValue);
    }

    public Vector3 GetCurrentPosition()
    {
        // Get RectTransform
        if (rectTransform == null) rectTransform = GetComponent<RectTransform>();

        // Depending on space
        switch (space)
        {
            case Space.World: return rectTransform.position;
            case Space.Local: return rectTransform.localPosition;
            case Space.AnchoredPos: return rectTransform.anchoredPosition;
            case Space.AnchoredPos3D: return rectTransform.anchoredPosition3D;
            default: return rectTransform.position;
        }
    }

    public void AssignCustomRectTransform(RectTransform _customRectTransform) { customRectTransform = _customRectTransform; }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AMAAnimation_Move_UI))]
class AMAAnimationMoveUiEditor : AMAComponentEditor<Vector3>
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
        customTransformProp = serializedObject.FindProperty("customRectTransform");

        SetUpOnEnable(serializedObject);

        // Load banner
        banner = (Texture)Resources.Load(bannerPath, typeof(Texture));
    }

    public override void OnInspectorGUI()
    {
        AMAAnimation_Move_UI script = (AMAAnimation_Move_UI)target;

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
       
        // Space
        EditorGUILayout.PropertyField(spaceProp, new GUIContent("Space"), true);
        
        // Play anim on start
        script.playAnimationOnStart = EditorGUILayout.Toggle("Play anim. on Start()", script.playAnimationOnStart);
        
        // Custom transform
        script.addCustomRectTransform = EditorGUILayout.Toggle("Use another RectTransform", script.addCustomRectTransform);
        if (script.addCustomRectTransform) EditorGUILayout.PropertyField(customTransformProp, new GUIContent("Custom RectTransform"), true);
        #endregion

        #region Misc Settings
        DrawMisc(serializedObject, script);
        #endregion

        // Apply serialized changes
        serializedObject.ApplyModifiedProperties();
        #endregion
    }

    #region Tools
    void DisplayVector3(string _nameToDisplay, ref Vector3 _vector, AMA.Axis _axis, AMAAnimation_Move_UI _script, bool _enable = true)
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