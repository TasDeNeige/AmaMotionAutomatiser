//
// • Ama Motion Automatizer
// • [ Main file ]
// • By Amaryne Bréand
// • Last updated: 28/12/2024
//

using UnityEngine;
using System.Collections;

namespace AMA
{
    public delegate void MAfunction();
    public delegate float CurveDelegate(float currentTimeInSeconds, float startValue, float endValue, float duration);


    public static class AMAMain
    {
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
            public float duration;
            public float delay;
            // Curves Miscellaneous
            public CurveDelegate curveDelegate;
            public AnimationCurve animationCurve;

            // Functions
            public MAfunction onStartFunc;
            public MAfunction onCompleteFunc;
        }
        #endregion

        #region Creator
        /// <summary>
        /// Creates a MA with basic parameters. Not intended to be used by user.
        /// </summary>
        public static MA CreateMA(Transform _transform = null)
        {
            MA ma = new MA()
            {
                isActive = true,
                transform = _transform,

                startPos = new Vector3(0.0f, 0.0f, 0.0f),
                endPos = new Vector3(0.0f, 0.0f, 0.0f),

                duration = 1.0f,
                delay = 0.0f,
                curveDelegate = AMACurves.GetCurveFunction(Curves.Linear),
                animationCurve = null,
            };

            return ma;
        }
        #endregion
    }
}