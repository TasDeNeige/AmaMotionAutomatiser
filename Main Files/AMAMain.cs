//
// • Ama Motion Automatiser
// • [ Main file ]
// • By Amaryne Bréand
//

using UnityEngine;
using System.Collections;
using System;
using TMPro;

namespace AMA
{
    public delegate void MAfunction();
    public delegate float CurveDelegate(float currentTimeInSeconds, float startValue, float endValue, float duration);

    public enum Axis { x, y, z, All }

    public static class AMAMain
    {
        private static bool debug = false;
        public static string debugAlertString = $"<b><color=#00C7AC>AMA • </color><color=#F5715D>Debug • </color></b> ";
        public static void ToggleDebug(bool _bool) => debug = _bool;
        public static void StopAllCoroutines() => AMACoroutineRunner.Instance.INTERNAL_StopAll();

        #region Main class
        abstract public class MA<T>
        {
            // Main
            public IEnumerator coroutine;
            public object unityObject;

            // Values
            public T fromValue;
            public T startValue;
            public T endValue;

            // Miscellaneous
            public Axis selectedAxis;
            public float duration;
            public bool hasFromValue = false;
            public bool snapToEndValue;
            public float delay;
            public CurveDelegate curveDelegate = AMACurves.GetCurveFunction(Curves.Linear);
            public AnimationCurve animationCurve = null;

            // Functions
            public MAfunction onStartFunc;
            public MAfunction onLateStartFunc;
            public MAfunction onCompleteFunc;

            // Creators
            public MA() { }
            public MA(T _start, T _end, float _duration = 1.0f, float _delay = 0.0f, bool _snapToEnd = true)
            {
                startValue = _start;
                endValue = _end;
                duration = _duration;
                delay = _delay;
                snapToEndValue = _snapToEnd;

                curveDelegate = AMACurves.GetCurveFunction(Curves.Linear);
                animationCurve = null;
            }

            // Destructor
            ~MA() { this.INTERNAL_Destroy(); }

            #region Methods
            public abstract bool GetAvailability();
            public abstract T GetModifiedValue();
            public abstract void SetModifiedValue(T _newValue);

            // Generic interpolation
            public T Lerp(T startValue, T endValue, float t)
            {
                switch (typeof(T))
                {
                    case Type T when T == typeof(float):
                        return (T)(object)Mathf.Lerp((float)(object)startValue, (float)(object)endValue, t);

                    case Type T when T == typeof(Vector3):
                        return (T)(object)Vector3.LerpUnclamped((Vector3)(object)startValue, (Vector3)(object)endValue, t);

                    case Type T when T == typeof(Quaternion):
                        return (T)(object)Quaternion.LerpUnclamped((Quaternion)(object)startValue, (Quaternion)(object)endValue, t);

                    case Type T when T == typeof(Color):
                        return (T)(object)Color.LerpUnclamped((Color)(object)startValue, (Color)(object)endValue, t);

                    default:
                        Debug.LogError(debugAlertString + $"Lerp not implemented for type {typeof(T)}");
                        return startValue;
                }
            }

            // Neutral value for each type
            public T ZeroValue()
            {
                switch (typeof(T))
                {
                    case Type T when T == typeof(float):
                        return (T)(object)0f;

                    case Type T when T == typeof(Vector3):
                        return (T)(object)Vector3.zero;

                    case Type T when T == typeof(Quaternion):
                        return (T)(object)Quaternion.identity;

                    case Type T when T == typeof(Color):
                        return (T)(object)Color.clear;

                    default:
                        Debug.LogError(debugAlertString + $"ZeroValue not implemented for type {typeof(T)}");
                        return default;
                }
            }

            public T ApplyAxisMask(Axis selectedAxis, T changedValue)
            {
                // Vector 3
                if (typeof(T) == typeof(Vector3))
                {
                    Vector3 _changedValue = (Vector3)(object)changedValue;
                    Vector3 _unchangedValue = (Vector3)(object)GetModifiedValue();

                    switch (selectedAxis)
                    {
                        case Axis.x: return (T)(object)new Vector3(_changedValue.x, _unchangedValue.y, _unchangedValue.z);
                        case Axis.y: return (T)(object)new Vector3(_unchangedValue.x, _changedValue.y, _unchangedValue.z);
                        case Axis.z: return (T)(object)new Vector3(_unchangedValue.x, _unchangedValue.y, _changedValue.z);
                        case Axis.All: return changedValue;
                    }
                }

                // Color
                else if (typeof(T) == typeof(Color))
                {
                    return changedValue;
                }

                // Quaternion
                else if (typeof(T) == typeof(Quaternion))
                {
                    Quaternion _changedValue = (Quaternion)(object)changedValue;
                    Quaternion _unchangedValue = (Quaternion)(object)GetModifiedValue();

                    switch (selectedAxis)
                    {
                        case Axis.x: return (T)(object)new Quaternion(_changedValue.x, _unchangedValue.y, _unchangedValue.z, _unchangedValue.w);
                        case Axis.y: return (T)(object)new Quaternion(_unchangedValue.x, _changedValue.y, _unchangedValue.z, _unchangedValue.w);
                        case Axis.z: return (T)(object)new Quaternion(_unchangedValue.x, _unchangedValue.y, _changedValue.z, _unchangedValue.w);
                        case Axis.All: return changedValue;
                    }
                }

                Debug.LogError(debugAlertString + $"ApplyAxisMask not implemented for type {typeof(T)}");
                return changedValue;
            }

            public T ValueAccordingToAxis(Axis selectedAxis, T value, T offset)
            {
                // Vector 3
                if (typeof(T) == typeof(Vector3))
                {
                    Vector3 _value = (Vector3)(object)value;
                    Vector3 _offset = (Vector3)(object)offset;
                    Vector3 _unchangedValue = (Vector3)(object)GetModifiedValue();

                    switch (selectedAxis)
                    {
                        case Axis.x: return (T)(object)new Vector3(_value.x + _offset.x, _unchangedValue.y, _unchangedValue.z);
                        case Axis.y: return (T)(object)new Vector3(_unchangedValue.x, _value.y + _offset.y, _unchangedValue.z);
                        case Axis.z: return (T)(object)new Vector3(_unchangedValue.x, _unchangedValue.y, _value.z + _offset.z);
                        case Axis.All: return (T)(object)(_value + _offset);
                    }
                }

                // Color
                else if (typeof(T) == typeof(Color))
                {
                    Color _value = (Color)(object)value;
                    Color _offset = (Color)(object)offset;
                    return (T)(object)(_value + _offset);
                }

                // Quaternion
                else if (typeof(T) == typeof(Quaternion))
                {
                    Quaternion _value = (Quaternion)(object)value;
                    Quaternion _offset = (Quaternion)(object)offset;
                    Quaternion _unchangedValue = (Quaternion)(object)GetModifiedValue();

                    switch (selectedAxis)
                    {
                        case Axis.x: return (T)(object)new Quaternion(_value.x + _offset.x, _unchangedValue.y, _unchangedValue.z, _unchangedValue.w);
                        case Axis.y: return (T)(object)new Quaternion(_unchangedValue.x, _value.y + _offset.y, _unchangedValue.z, _unchangedValue.w);
                        case Axis.z: return (T)(object)new Quaternion(_unchangedValue.x, _unchangedValue.y, _value.z + _offset.z, _unchangedValue.w);
                        case Axis.All: return (T)(object)(_value * _offset);
                    }
                }

                Debug.LogError(debugAlertString + $"ValueAccordingToAxis not implemented for type {typeof(T)}");
                return offset;
            }

            // Calculate external offset
            public T GetExternalOffset(T interpolatedValue, T previousOffset)
            {
                // Vector 3
                if (typeof(T) == typeof(Vector3))
                {
                    Vector3 interp = (Vector3)(object)interpolatedValue;
                    Vector3 prevOff = (Vector3)(object)previousOffset;
                    Vector3 modified = (Vector3)(object)GetModifiedValue();

                    return (T)(object)(modified - (interp + prevOff));
                }

                // Color
                else if (typeof(T) == typeof(Color))
                {
                    Color interp = (Color)(object)interpolatedValue;
                    Color prevOff = (Color)(object)previousOffset;
                    Color modified = (Color)(object)GetModifiedValue();

                    return (T)(object)(modified - (interp + prevOff));
                }

                // Quaternion
                if (typeof(T) == typeof(Quaternion))
                {
                    Quaternion interp = (Quaternion)(object)interpolatedValue;
                    Quaternion prevOff = (Quaternion)(object)previousOffset;
                    Quaternion modified = (Quaternion)(object)GetModifiedValue();

                    return (T)(object)(Quaternion.Inverse(interp * prevOff) * modified);
                }

                Debug.LogError(debugAlertString + $"GetExternalOffset not implemented for type {typeof(T)}");
                return ZeroValue();
            }

            #endregion
        }
        #endregion

        #region Destructor
        public static void INTERNAL_Destroy<T>(this MA<T> _ma)
        {
            // Prevent accessing MA if it's not accessible anymore
            if (!_ma.GetAvailability()) return;

            AMACoroutineRunner.Instance.INTERNAL_DeleteCoroutine(_ma.unityObject, _ma.coroutine);

            _ma.coroutine = null;
            _ma.fromValue = default;
            _ma.startValue = default;
            _ma.endValue = default;
            _ma.hasFromValue = default;
            _ma.snapToEndValue = false;
            _ma.duration = 0;
            _ma.delay = 0;
            _ma.selectedAxis = default(Axis);
            _ma.curveDelegate = null;
            _ma.animationCurve = null;
            _ma.onStartFunc = null;
            _ma.onCompleteFunc = null;

            #if UNITY_EDITOR
            if (debug) Debug.Log(debugAlertString + "<color=#0CB167>MA object has been destroyed.</color>");
            #endif
        }
        #endregion

    }
}
