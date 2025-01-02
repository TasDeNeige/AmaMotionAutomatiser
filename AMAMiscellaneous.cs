//
// • Ama Motion Automatizer
// • [ Miscellaneous Methods ]
// • By Amaryne Bréand
// • Last updated: 03/01/2025
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
        public static MA OnStart(this MA _ma, MAfunction _action)
        {
            if (_ma == null)
            {
                return _ma;
            }

            _ma.onStartFunc = _action;
            return _ma;
        }

        /// <summary>
        /// Execute given function when MA ends.
        /// </summary>
        /// <param name="_action">Function to execute</param>
        public static MA OnEnd(this MA _ma, MAfunction _action)
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
        public static MA SetDelay(this MA _ma, float _delay)
        {
            _ma.delay = _delay;
            return _ma;
        }

        #region Set Curves
        /// <summary>
        /// Set curve to moderate MA's movement.
        /// </summary>
        /// <param name="_curve">Curve to use.</param>
        public static MA SetCurve(this MA _ma, Curves _curve)
        {
            _ma.curveDelegate = GetCurveFunction(_curve);
            return _ma;
        }

        /// <summary>
        /// Set curve to moderate MA's movement.
        /// </summary>
        /// <param name="_curve">Curve to use.</param>
        public static MA SetCurve(this MA _ma, AnimationCurve _curve)
        {
            //_ma.curveDelegate = GetCurveFunction(Curves.CUSTOM); // may be used someday?
            _ma.animationCurve = _curve;
            return _ma;
        }
        #endregion
        #endregion
    }
}