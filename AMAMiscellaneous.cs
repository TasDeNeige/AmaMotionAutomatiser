//
// • Ama Motion Automatizer
// • [ Miscellaneous Methods ]
// • By Amaryne Bréand
//

using UnityEngine;
using static AMA.AMACurves;
using static AMA.AMAMain;

namespace AMA
{
    public static class AMAMiscellaneous
    {

        #region Methods
        #region On Start/End
        /// <summary>
        /// Execute given function when MA starts.
        /// </summary>
        /// <param name="_action">Function to execute</param>
        public static MA<T> OnStart<T>(this MA<T> _ma, MAfunction _action)
        {
            if (_ma == null)
            {
                return _ma;
            }

            _ma.onStartFunc = _action;
            return _ma;
        }

        /// <summary>
        /// Execute given function after first MA's first frame.
        /// </summary>
        /// <param name="_action">Function to execute</param>
        public static MA<T> OnLateStart<T>(this MA<T> _ma, MAfunction _action)
        {
            if (_ma == null)
            {
                return _ma;
            }

            _ma.onLateStartFunc = _action;
            return _ma;
        }

        /// <summary>
        /// Execute given function when MA ends.
        /// </summary>
        /// <param name="_action">Function to execute</param>
        public static MA<T> OnEnd<T>(this MA<T> _ma, MAfunction _action)
        {
            if (_ma == null)
            {
                return _ma;
            }

            _ma.onCompleteFunc = _action;
            return _ma;
        }
        #endregion

        /// <summary>
        /// Set delay before executing MA.
        /// </summary>
        /// <param name="_delay">Delay before execution</param>
        public static MA<T> SetDelay<T>(this MA<T> _ma, float _delay)
        {
            _ma.delay = _delay;
            return _ma;
        }

        /// <summary>
        /// Stop all Automations on an Object
        /// </summary>
        /// <param name="_delay">Delay before execution</param>
        public static void StopMA(this object _object)
        {
            AMACoroutineRunner.Instance.INTERNAL_StopCoroutine(_object);
        }

        #region Set Curves
        /// <summary>
        /// Set curve to moderate MA's movement.
        /// </summary>
        /// <param name="_curve">Curve to use.</param>
        public static MA<T> SetCurve<T>(this MA<T> _ma, Curves _curve)
        {
            _ma.curveDelegate = GetCurveFunction(_curve);
            return _ma;
        }

        /// <summary>
        /// Set curve to moderate MA's movement.
        /// </summary>
        /// <param name="_curve">Curve to use.</param>
        public static MA<T> SetCurve<T>(this MA<T> _ma, AnimationCurve _curve)
        {
            //_ma.curveDelegate = GetCurveFunction(Curves.CUSTOM); // may be used someday?
            _ma.animationCurve = _curve;
            return _ma;
        }
        #endregion
        #endregion
    }
}