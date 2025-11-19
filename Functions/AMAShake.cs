//
// • Ama Motion Automatiser
// • [ Shake Methods ]
// • By Amaryne Bréand
//

using UnityEngine;
using static AMA.AMAMain;

namespace AMA
{
    public static class AMAShake
    {
        #region Transform
        #region Shake
        // ------------------------------------------ [ PUBLIC FUNCTION ] ------------------------------------------ //

        // Used with float
        /// <summary>
        /// Move Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_shakeRadius">Radius in which the transform can be shaken.</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_delayBetweenShakes">Add a delay between each shake. Defaulted to "0.0f"</param>
        /// <param name="_snapBackToStartPos">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAshake(this Transform _transform, Axis _selectedAxis, float _shakeRadius, float _duration, float _delayBetweenShakes = 0.0f, bool _snapBackToStartPos = true)
        {
            // Set up MA
            MAShake _ma = new MAShake();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.position;
            _ma.ShakeRadius = _shakeRadius;
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapBackToStartPos;
            _ma.SetCurve(Curves.Constant);
            _ma.DelayBetweenShakes = _delayBetweenShakes;

            // Apply MA
            _ma.coroutine = _ma.Shake();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_transform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion
        #endregion
    }
}