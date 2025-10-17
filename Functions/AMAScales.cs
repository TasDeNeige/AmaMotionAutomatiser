//
// • Ama Motion Automatiser
// • [ Scaling Methods ]
// • By Amaryne Bréand
//

using UnityEngine;
using static AMA.AMAMain;

namespace AMA
{
    public static class AMAScales
    {
        #region Transform
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        // Using float as End value
        /// <summary>
        /// Scale Game Object to a specific value.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply scaling</param>
        /// <param name="_endValue">Final value</param>
        /// <param name="_duration">Time taken to scale to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAscale(this Transform _transform, Axis _selectedAxis, float _endValue, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAScaleTransform _ma = new MAScaleTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.localScale;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, new Vector3(_endValue, _endValue, _endValue));
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_transform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }

        // Using Vector3 as End value
        /// <summary>
        /// Scale Game Object to a specific value.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply scaling</param>
        /// <param name="_endValue">Final value</param>
        /// <param name="_duration">Time taken to scale to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAscale(this Transform _transform, Axis _selectedAxis, Vector3 _endValue, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAScaleTransform _ma = new MAScaleTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.localScale;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, _endValue);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_transform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region RectTransform
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        // Using float as End value
        /// <summary>
        /// Scale Game Object to a specific value.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply scaling</param>
        /// <param name="_endValue">Final value</param>
        /// <param name="_duration">Time taken to scale to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAscale(this RectTransform _rectTransform, Axis _selectedAxis, float _endValue, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAScaleRectTransform _ma = new MAScaleRectTransform();
            _ma.unityObject = _rectTransform;
            _ma.rectTransform = _rectTransform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.rectTransform.localScale;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, new Vector3(_endValue, _endValue, _endValue));
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_rectTransform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }

        // Using Vector3 as End value
        /// <summary>
        /// Scale Game Object to a specific value.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply scaling</param>
        /// <param name="_endValue">Final value</param>
        /// <param name="_duration">Time taken to scale to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAscale(this RectTransform _rectTransform, Axis _selectedAxis, Vector3 _endValue, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAScaleRectTransform _ma = new MAScaleRectTransform();
            _ma.unityObject = _rectTransform;
            _ma.rectTransform = _rectTransform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.rectTransform.localScale;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, _endValue);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_rectTransform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion
    }
}