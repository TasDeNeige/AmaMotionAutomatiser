//
// • Ama Motion Automatiser
// • [ Rotations Methods ]
// • By Amaryne Bréand
//

using UnityEngine;
using static AMA.AMAMain;

namespace AMA
{
    public static class AMARotations
    {
        #region Transform
        #region World
        #region Quaternions
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        // Using float as End pos
        /// <summary>
        /// Moves Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Quaternion> AMArotate(this Transform _transform, Axis _selectedAxis, float _endRotation, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MARotateQuaternionTransform _ma = new MARotateQuaternionTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.rotation;
            _ma.endValue = new Quaternion(_endRotation, _endRotation, _endRotation, _endRotation);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_transform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }

        // Using Quaternion as End pos
        /// <summary>
        /// Moves Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Quaternion> AMArotate(this Transform _transform, Axis _selectedAxis, Quaternion _endRotation, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MARotateQuaternionTransform _ma = new MARotateQuaternionTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.rotation;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, _endRotation);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_transform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region Euler Angles
        // Using float as End pos
        /// <summary>
        /// Rotates Game Object to a specific rotation.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endRotation">Final rotation</param>
        /// <param name="_duration">Time taken to rotate to given rotation</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMArotateEuler(this Transform _transform, Axis _selectedAxis, float _endRotation, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MARotateEulerTransform _ma = new MARotateEulerTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.rotation.eulerAngles;
            _ma.endValue = new Vector3(_endRotation, _endRotation, _endRotation);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_transform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }

        // Using Vector3 as End pos
        /// <summary>
        /// Moves Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMArotateEuler(this Transform _transform, Axis _selectedAxis, Vector3 _endRotation, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MARotateEulerTransform _ma = new MARotateEulerTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.rotation.eulerAngles;
            _ma.endValue = _endRotation;
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_transform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion
        #endregion

        #region Local
        #region Quaternions
        // Using float as End pos
        /// <summary>
        /// Moves Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Quaternion> AMAlocalRotate(this Transform _transform, Axis _selectedAxis, float _endRotation, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MALocalRotateQuaternionTransform _ma = new MALocalRotateQuaternionTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.localRotation;
            _ma.endValue = new Quaternion(_endRotation, _endRotation, _endRotation, _endRotation);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_transform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }

        // Using Quaternion as End pos
        /// <summary>
        /// Moves Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Quaternion> AMAlocalRotate(this Transform _transform, Axis _selectedAxis, Quaternion _endRotation, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MALocalRotateQuaternionTransform _ma = new MALocalRotateQuaternionTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.localRotation;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, _endRotation);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_transform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region Euler Angles
        // Using float as End pos
        /// <summary>
        /// Rotates Game Object to a specific rotation.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endRotation">Final rotation</param>
        /// <param name="_duration">Time taken to rotate to given rotation</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAlocalRotateEuler(this Transform _transform, Axis _selectedAxis, float _endRotation, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MALocalRotateEulerTransform _ma = new MALocalRotateEulerTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.localRotation.eulerAngles;
            _ma.endValue = new Vector3(_endRotation, _endRotation, _endRotation);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_transform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }

        // Using Vector3 as End pos
        /// <summary>
        /// Moves Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAlocalRotateEuler(this Transform _transform, Axis _selectedAxis, Vector3 _endRotation, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MALocalRotateEulerTransform _ma = new MALocalRotateEulerTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.localRotation.eulerAngles;
            _ma.endValue = _endRotation;
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_transform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion
        #endregion
        #endregion
    }
}