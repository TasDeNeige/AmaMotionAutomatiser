using AMA;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Runtime.CompilerServices;

public class AMAAnimation : MonoBehaviour
{
    #region In inspector
    [Header("Main settings")]
    [SerializeField] public AMA.Axis axisToAnimate = Axis.All;
    [SerializeField] public Vector3 endValue = Vector3.one;
    [SerializeField, Tooltip("In seconds")] public float animationDuration = 1f;
    [SerializeField] public bool playAnimationOnStart = false;

    [Header("Miscellaneous")]
    [SerializeField] AMA.Curves curve = Curves.Linear;
    [SerializeField, HideInInspector] public bool addFunctionOnStart;
    [SerializeField, HideInInspector] public UnityEvent startFunction;
    [SerializeField, HideInInspector] public bool addFunctionOnEnd;
    [SerializeField, HideInInspector] public UnityEvent endFunction;
    [SerializeField, HideInInspector] public bool addFromValue;
    [SerializeField, HideInInspector] public Vector3 fromValue = Vector3.zero;
    [SerializeField] public bool addDelay;
    [SerializeField, Tooltip("In seconds"), HideInInspector] public float delay = 0f;
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

        /* Add curves */    if (curve != Curves.Linear) newMA.SetCurve(curve);
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

[CustomEditor(typeof(AMAAnimation))]
class AMAAnimationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AMAAnimation script = (AMAAnimation)target;

        script.addFunctionOnStart = EditorGUILayout.Toggle("Add Function on Animation Start", script.addFunctionOnStart);
        if (script.addFunctionOnStart) script.startFunction = EditorGUILayout.ObjectField("Functions on start: ", script.startFunction, typeof(UnityEvent), true) as UnityEvent;

        script.addDelay = EditorGUILayout.Toggle("Add Delay to Animation", script.addDelay);
        if (script.addDelay) script.delay = EditorGUILayout.FloatField("Delay: ", script.delay);

    }
}
