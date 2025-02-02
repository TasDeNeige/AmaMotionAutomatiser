//
// • Ama Motion Automatizer
// • [ Main file ]
// • By Amaryne Bréand
// • Last updated: 31/01/2025
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

        #region Main class
        abstract public class MA
        {
            // Main
            public IEnumerator coroutine;

            // Values
            public Vector3 startValue;
            public Vector3 endValue;

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

            // Methods
            public abstract Vector3 GetModifiedValue();
            public abstract void SetModifiedValue(Vector3 _newValue);
        }
        #endregion

        #region Overriders
        #region Move
        #region Transform
        public class MAMoveTransform : MA
        {
            public Transform transform;

            public override Vector3 GetModifiedValue() { return transform.position; }
            public override void SetModifiedValue(Vector3 _newValue) { transform.position = _newValue; }
        }

        public class MAMovelocalTransform : MA
        {
            public Transform transform;

            public override Vector3 GetModifiedValue() { return transform.localPosition; }
            public override void SetModifiedValue(Vector3 _newValue) { transform.localPosition = _newValue; }
        }
        #endregion

        #region Rect Transform
        public class MAMoveRectTransform : MA
        {
            public RectTransform rectTransform;

            public override Vector3 GetModifiedValue() { return rectTransform.position; }
            public override void SetModifiedValue(Vector3 _newValue) { rectTransform.position = _newValue; }
        }

        public class MAMoveLocalRectTransform : MA
        {
            public RectTransform rectTransform;

            public override Vector3 GetModifiedValue() { return rectTransform.localPosition; }
            public override void SetModifiedValue(Vector3 _newValue) { rectTransform.localPosition = _newValue; }
        }

        public class MAMoveAnchoredPositionRectTransform : MA
        {
            public RectTransform rectTransform;

            public override Vector3 GetModifiedValue() { return rectTransform.anchoredPosition; }
            public override void SetModifiedValue(Vector3 _newValue) { rectTransform.anchoredPosition = _newValue; }
        }

        public class MAMoveAnchoredPosition3dRectTransform : MA
        {
            public RectTransform rectTransform;

            public override Vector3 GetModifiedValue() { return rectTransform.anchoredPosition3D; }
            public override void SetModifiedValue(Vector3 _newValue) { rectTransform.anchoredPosition3D = _newValue; }
        }

        #endregion
        #endregion

        #region Scale
        #region Transform
        public class MAScaleTransform : MA
        {
            public Transform transform;

            public override Vector3 GetModifiedValue() { return transform.localScale; }
            public override void SetModifiedValue(Vector3 _newValue) { transform.localScale = _newValue; }
        }
        #endregion
        #region RectTransform
        public class MAScaleRectTransform : MA
        {
            public RectTransform rectTransform;

            public override Vector3 GetModifiedValue() { return rectTransform.localScale; }
            public override void SetModifiedValue(Vector3 _newValue) { rectTransform.localScale = _newValue; }
        }
        #endregion
        #endregion
        #endregion

        #region Creators
        /// <summary>
        /// Principal MA setup function. Not intended to be used by user.
        /// </summary>
        private static MA INTERNAL_SetUpMA(MA _ma)
        {
            _ma.isActive = true;

            _ma.startValue = new Vector3(0.0f, 0.0f, 0.0f);
            _ma.endValue = new Vector3(0.0f, 0.0f, 0.0f);

            _ma.duration = 1.0f;
            _ma.delay = 0.0f;
            _ma.snapToEndValue = true;
            _ma.curveDelegate = AMACurves.GetCurveFunction(Curves.Linear);
            _ma.animationCurve = null;

            return _ma;
        }

        #region Move
        #region Transform
        /// <summary>
        /// Creates a MA with basic parameters for transform position. Not intended to be used by user.
        /// </summary>
        public static MA INTERNAL_CreateMoveTransformMA(Transform _transform)
        {
            MA ma = new MAMoveTransform()
            {
                transform = _transform
            };

            INTERNAL_SetUpMA(ma);
            return ma;
        }

        /// <summary>
        /// Creates a MA with basic parameters for transform local position. Not intended to be used by user.
        /// </summary>
        public static MA INTERNAL_CreateMoveLocalTransformMA(Transform _transform)
        {
            MA ma = new MAMovelocalTransform()
            {
                transform = _transform
            };

            INTERNAL_SetUpMA(ma);
            return ma;
        }
        #endregion

        #region RectTransform
        /// <summary>
        /// Creates a MA with basic parameters for rect transform position. Not intended to be used by user.
        /// </summary>
        public static MA INTERNAL_CreateMoveRectTransformMA(RectTransform _rectTransform)
        {
            MA ma = new MAMoveRectTransform()
            {
                rectTransform = _rectTransform
            };

            INTERNAL_SetUpMA(ma);
            return ma;
        }

        /// <summary>
        /// Creates a MA with basic parameters for local rect transform position. Not intended to be used by user.
        /// </summary>
        public static MA INTERNAL_CreateMoveLocalRectTransformMA(RectTransform _rectTransform)
        {
            MA ma = new MAMoveLocalRectTransform()
            {
                rectTransform = _rectTransform
            };

            INTERNAL_SetUpMA(ma);
            return ma;
        }
        
        /// <summary>
        /// Creates a MA with basic parameters for local rect transform position. Not intended to be used by user.
        /// </summary>
        public static MA INTERNAL_CreateMoveAnchoredPositionRectTransformMA(RectTransform _rectTransform)
        {
            MA ma = new MAMoveAnchoredPositionRectTransform()
            {
                rectTransform = _rectTransform
            };

            INTERNAL_SetUpMA(ma);
            return ma;
        }

        /// <summary>
        /// Creates a MA with basic parameters for local rect transform position. Not intended to be used by user.
        /// </summary>
        public static MA INTERNAL_CreateMoveAnchoredPosition3dRectTransformMA(RectTransform _rectTransform)
        {
            MA ma = new MAMoveAnchoredPosition3dRectTransform()
            {
                rectTransform = _rectTransform
            };

            INTERNAL_SetUpMA(ma);
            return ma;
        }
        #endregion
        #endregion

        #region Scale
        #region Transform
        /// <summary>
        /// Creates a MA with basic parameters for transform position. Not intended to be used by user.
        /// </summary>
        public static MA INTERNAL_CreateScaleTransformMA(Transform _transform)
        {
            MA ma = new MAScaleTransform()
            {
                transform = _transform
            };

            INTERNAL_SetUpMA(ma);
            return ma;
        }
        #endregion

        #region RectTransform
        /// <summary>
        /// Creates a MA with basic parameters for transform position. Not intended to be used by user.
        /// </summary>
        public static MA INTERNAL_CreateScaleRectTransformMA(RectTransform _rectTransform)
        {
            MA ma = new MAScaleTransform()
            {
                transform = _rectTransform
            };

            INTERNAL_SetUpMA(ma);
            return ma;
        }
        #endregion
        #endregion
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
            _ma.startValue = Vector3.zero;
            _ma.endValue = Vector3.zero;
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
            /// Put 'debug' to false to suppress this log.
            if (debug) Debug.Log($"<color=#00C7AC>MA object has been destroyed.</color>");
#endif
        }
        #endregion
    }
}