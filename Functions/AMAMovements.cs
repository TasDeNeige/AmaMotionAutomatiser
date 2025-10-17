//
// • Ama Motion Automatiser
// • [ Movements Methods ]
// • By Amaryne Bréand
//

using UnityEngine;
using static AMA.AMAMain;

namespace AMA
{
    public static class AMAMovements
    {
        #region Transform
        #region Move
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        // Using float as End pos
        /// <summary>
        /// Move Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAmove(this Transform _transform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAMoveTransform _ma = new MAMoveTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.position;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, new Vector3(_endPos, _endPos, _endPos));
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
        public static MA<Vector3> AMAmove(this Transform _transform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAMoveTransform _ma = new MAMoveTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.position;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, _endPos);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_transform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region Local move
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        // Using float as End pos
        /// <summary>
        /// Move Game Object to a specific local position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAlocalMove(this Transform _transform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAMoveLocalTransform _ma = new MAMoveLocalTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.localPosition;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, new Vector3(_endPos, _endPos, _endPos));
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
        /// Move Game Object to a specific local position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAlocalMove(this Transform _transform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAMoveLocalTransform _ma = new MAMoveLocalTransform();
            _ma.unityObject = _transform;
            _ma.transform = _transform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.transform.localPosition;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, _endPos);
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

        #region RectTransform
        #region Move
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        // Using float as End pos
        /// <summary>
        /// Move Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAmove(this RectTransform _rectTransform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAMoveRectTransform _ma = new MAMoveRectTransform();
            _ma.unityObject = _rectTransform;
            _ma.rectTransform = _rectTransform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.rectTransform.position;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, new Vector3(_endPos, _endPos, _endPos));
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_rectTransform, _ma.coroutine);

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
        public static MA<Vector3> AMAmove(this RectTransform _rectTransform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAMoveRectTransform _ma = new MAMoveRectTransform();
            _ma.unityObject = _rectTransform;
            _ma.rectTransform = _rectTransform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.rectTransform.position;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, _endPos);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_rectTransform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region Local Move
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        // Using float as End pos
        /// <summary>
        /// Move Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAlocalMove(this RectTransform _rectTransform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAMoveLocalRectTransform _ma = new MAMoveLocalRectTransform();
            _ma.unityObject = _rectTransform;
            _ma.rectTransform = _rectTransform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.rectTransform.localPosition;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, new Vector3(_endPos, _endPos, _endPos));
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_rectTransform, _ma.coroutine);

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
        public static MA<Vector3> AMAlocalMove(this RectTransform _rectTransform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAMoveLocalRectTransform _ma = new MAMoveLocalRectTransform();
            _ma.unityObject = _rectTransform;
            _ma.rectTransform = _rectTransform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.rectTransform.localPosition;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, _endPos);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_rectTransform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region Anchored Position Move
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        // Using float as End pos
        /// <summary>
        /// Move Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAanchoredPosMove(this RectTransform _rectTransform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAMoveAnchoredPositionRectTransform _ma = new MAMoveAnchoredPositionRectTransform();
            _ma.unityObject = _rectTransform;
            _ma.rectTransform = _rectTransform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.rectTransform.anchoredPosition;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, new Vector3(_endPos, _endPos, _endPos));
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_rectTransform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }

        // Using Vector2 as End pos
        /// <summary>
        /// Moves Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAanchoredPosMove(this RectTransform _rectTransform, Axis _selectedAxis, Vector2 _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAMoveAnchoredPositionRectTransform _ma = new MAMoveAnchoredPositionRectTransform();
            _ma.unityObject = _rectTransform;
            _ma.rectTransform = _rectTransform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.rectTransform.anchoredPosition;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, _endPos);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_rectTransform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region Anchored Position 3D Move
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        // Using float as End pos
        /// <summary>
        /// Move Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA<Vector3> AMAanchoredPos3dMove(this RectTransform _rectTransform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAMoveAnchoredPosition3DRectTransform _ma = new MAMoveAnchoredPosition3DRectTransform();
            _ma.unityObject = _rectTransform;
            _ma.rectTransform = _rectTransform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.rectTransform.anchoredPosition3D;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, new Vector3(_endPos, _endPos, _endPos));
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_rectTransform, _ma.coroutine);

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
        public static MA<Vector3> AMAanchoredPos3dMove(this RectTransform _rectTransform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAMoveAnchoredPosition3DRectTransform _ma = new MAMoveAnchoredPosition3DRectTransform();
            _ma.unityObject = _rectTransform;
            _ma.rectTransform = _rectTransform;
            _ma.selectedAxis = _selectedAxis;
            _ma.startValue = _ma.rectTransform.anchoredPosition3D;
            _ma.endValue = _ma.ApplyAxisMask(_selectedAxis, _endPos);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_rectTransform, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion
        #endregion
    }
}