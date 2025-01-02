//
// • Ama Motion Automatizer
// • [ Main file ]
// • By Amaryne Bréand
// • Last updated: 03/01/2025
//

using UnityEngine;
using System.Collections;

namespace AMA
{
    public delegate void MAfunction();
    public delegate float CurveDelegate(float currentTimeInSeconds, float startValue, float endValue, float duration);

    public enum Axis
    {
        x,
        y,
        z,
        All
    }

    public static class AMAMain
    {
        private static bool debug = true;

        #region Class
        public class MA
        {
            // Objects
            public IEnumerator coroutine;
            public Transform transform;

            // Positions
            public Vector3 startPos;
            public Vector3 endPos;

            // Miscellaneous
            public bool isActive;
            public bool snapToEndValue;
            public Axis selectedAxis;
            public float duration;
            public float delay;
            // Curves Miscellaneous
            public CurveDelegate curveDelegate;
            public AnimationCurve animationCurve;

            // Functions
            public MAfunction onStartFunc;
            public MAfunction onCompleteFunc;

            // Destructor
            ~MA() { this.INTERNAL_Destroy(); }
        }
        #endregion

        #region Creator
        /// <summary>
        /// Creates a MA with basic parameters. Not intended to be used by user.
        /// </summary>
        public static MA INTERNAL_CreateMA(Transform _transform = null)
        {
            MA ma = new MA()
            {
                isActive = true,
                transform = _transform,

                startPos = new Vector3(0.0f, 0.0f, 0.0f),
                endPos = new Vector3(0.0f, 0.0f, 0.0f),

                duration = 1.0f,
                delay = 0.0f,
                snapToEndValue = true,
                curveDelegate = AMACurves.GetCurveFunction(Curves.Linear),
                animationCurve = null,
            };

            return ma;
        }
        #endregion

        #region Destructor
        /// <summary>
        /// Destroys MA. Not intended to be used by user.
        /// </summary>
        public static void INTERNAL_Destroy(this MA _ma)
        {
            // Ensures coroutine is stopped (if still running)
            if (_ma.coroutine != null) { AMACoroutineRunner.Instance.StopCoroutine(_ma.coroutine); }

            // Nullify references to free resources
            _ma.coroutine = null;
            _ma.transform = null;
            _ma.startPos = Vector3.zero;
            _ma.endPos = Vector3.zero;
            _ma.isActive = false;
            _ma.snapToEndValue = false;
            _ma.duration = 0;
            _ma.delay = 0;
            _ma.selectedAxis = default(Axis);
            _ma.curveDelegate = null;
            _ma.animationCurve = null;
            _ma.onStartFunc = null;
            _ma.onCompleteFunc = null;

            _ma = null;

            #if UNITY_EDITOR
            if (debug) Debug.Log($"<color=#00C7AC>MA object has been destroyed.</color>");
            #endif
        }
        #endregion
    }
}