//
// • Ama Motion Automatizer
// • [ Scaling Methods ]
// • By Amaryne Bréand
// • Last updated: 31/01/2025
//

using UnityEngine;
using System.Collections;
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
        public static MA AMAscale(this Transform _transform, Axis _selectedAxis, float _endValue, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateScaleTransformMA(_transform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endValue, _transform.localScale.y, _transform.localScale.z); break;
                case Axis.y: ma.endValue = new Vector3(_transform.localScale.x, _endValue, _transform.localScale.z); break;
                case Axis.z: ma.endValue = new Vector3(_transform.localScale.x, _transform.localScale.y, _endValue); break;
                case Axis.All: ma.endValue = new Vector3(_endValue, _endValue, _endValue); break;
            }

            // Return MA for other functions
            return INTERNAL_AMAscaleTransform(ma, _transform, _selectedAxis, _duration, _snapToEndValue);
        }

        // Using float as End value
        /// <summary>
        /// Scale Game Object to a specific value.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply scaling</param>
        /// <param name="_endValue">Final value</param>
        /// <param name="_duration">Time taken to scale to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA AMAscale(this Transform _transform, Axis _selectedAxis, Vector3 _endValue, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateScaleTransformMA(_transform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endValue.x, _transform.localScale.y, _transform.localScale.z); break;
                case Axis.y: ma.endValue = new Vector3(_transform.localScale.x, _endValue.y, _transform.localScale.z); break;
                case Axis.z: ma.endValue = new Vector3(_transform.localScale.x, _transform.localScale.y, _endValue.z); break;
                case Axis.All: ma.endValue = _endValue; break;
            }

            // Return MA for other functions
            return INTERNAL_AMAscaleTransform(ma, _transform, _selectedAxis, _duration, _snapToEndValue);
        }

        // ------------------------------------------ [ INTERNAL FUNCTIONS ] ------------------------------------------ //

        private static MA INTERNAL_AMAscaleTransform(MA _ma, Transform _transform, Axis _selectedAxis, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            _ma.startValue = _transform.localScale;
            _ma.duration = _duration;
            _ma.selectedAxis = _selectedAxis;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_ma.coroutine);

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
        public static MA AMAscale(this RectTransform _rectTransform, Axis _selectedAxis, float _endValue, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateScaleTransformMA(_rectTransform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endValue, _rectTransform.localScale.y, _rectTransform.localScale.z); break;
                case Axis.y: ma.endValue = new Vector3(_rectTransform.localScale.x, _endValue, _rectTransform.localScale.z); break;
                case Axis.z: ma.endValue = new Vector3(_rectTransform.localScale.x, _rectTransform.localScale.y, _endValue); break;
                case Axis.All: ma.endValue = new Vector3(_endValue, _endValue, _endValue); break;
            }

            // Return MA for other functions
            return INTERNAL_AMAscaleTransform(ma, _rectTransform, _selectedAxis, _duration, _snapToEndValue);
        }

        // Using float as End value
        /// <summary>
        /// Scale Game Object to a specific value.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply scaling</param>
        /// <param name="_endValue">Final value</param>
        /// <param name="_duration">Time taken to scale to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA AMAscale(this RectTransform _rectTransform, Axis _selectedAxis, Vector3 _endValue, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateScaleTransformMA(_rectTransform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endValue.x, _rectTransform.localScale.y, _rectTransform.localScale.z); break;
                case Axis.y: ma.endValue = new Vector3(_rectTransform.localScale.x, _endValue.y, _rectTransform.localScale.z); break;
                case Axis.z: ma.endValue = new Vector3(_rectTransform.localScale.x, _rectTransform.localScale.y, _endValue.z); break;
                case Axis.All: ma.endValue = _endValue; break;
            }

            // Return MA for other functions
            return INTERNAL_AMAscaleTransform(ma, _rectTransform, _selectedAxis, _duration, _snapToEndValue);
        }

        // ------------------------------------------ [ INTERNAL FUNCTIONS ] ------------------------------------------ //

        private static MA INTERNAL_AMAscaleRectTransform(MA _ma, RectTransform _rectTransform, Axis _selectedAxis, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            _ma.startValue = _rectTransform.localScale;
            _ma.duration = _duration;
            _ma.selectedAxis = _selectedAxis;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_ma.coroutine);

            return _ma;
        }
        #endregion
    }
}