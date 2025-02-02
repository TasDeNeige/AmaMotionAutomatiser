//
// • Ama Motion Automatizer
// • [ Movements Methods ]
// • By Amaryne Bréand
// • Last updated: 31/01/2025
//

using UnityEngine;
using System.Collections;
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
        public static MA AMAmove(this Transform _transform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateMoveTransformMA(_transform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endPos, _transform.position.y, _transform.position.z); break;
                case Axis.y: ma.endValue = new Vector3(_transform.position.x, _endPos, _transform.position.z); break;
                case Axis.z: ma.endValue = new Vector3(_transform.position.x, _transform.position.y, _endPos); break;
                case Axis.All: ma.endValue = new Vector3(_endPos, _endPos, _endPos); break;
            }

            // Return MA for other functions
            return INTERNAL_AMAmoveTransform(ma, _transform, _selectedAxis, _duration, _snapToEndValue);
        }

        // Using Vector3 as End pos
        /// <summary>
        /// Moves Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA AMAmove(this Transform _transform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateMoveTransformMA(_transform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endPos.x, _transform.position.y, _transform.position.z); break;
                case Axis.y: ma.endValue = new Vector3(_transform.position.x, _endPos.y, _transform.position.z); break;
                case Axis.z: ma.endValue = new Vector3(_transform.position.x, _transform.position.y, _endPos.z); break;
                case Axis.All: ma.endValue = _endPos; break;
            }

            // Return MA for other functions
            return INTERNAL_AMAmoveTransform(ma, _transform, _selectedAxis, _duration, _snapToEndValue);
        }

        // ------------------------------------------ [ INTERNAL FUNCTIONS ] ------------------------------------------ //

        private static MA INTERNAL_AMAmoveTransform(MA _ma, Transform _transform, Axis _selectedAxis, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            _ma.startValue = _transform.position;
            _ma.duration = _duration;
            _ma.selectedAxis = _selectedAxis;
            _ma.snapToEndValue = _snapToEndValue;
            
            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_ma.coroutine);

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
        public static MA AMAlocalMove(this Transform _transform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateMoveLocalTransformMA(_transform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endPos, _transform.localPosition.y, _transform.localPosition.z); break;
                case Axis.y: ma.endValue = new Vector3(_transform.localPosition.x, _endPos, _transform.localPosition.z); break;
                case Axis.z: ma.endValue = new Vector3(_transform.localPosition.x, _transform.localPosition.y, _endPos); break;
                case Axis.All: ma.endValue = new Vector3(_endPos, _endPos, _endPos); break;
            }

            // Return MA for other functions
            return INTERNAL_AMAlocalMoveTransform(ma, _transform, _selectedAxis, _duration, _snapToEndValue);
        }

        // Using Vector3 as End pos
        /// <summary>
        /// Move Game Object to a specific local position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA AMAlocalMove(this Transform _transform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateMoveLocalTransformMA(_transform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endPos.x, _transform.localPosition.y, _transform.localPosition.z); break;
                case Axis.y: ma.endValue = new Vector3(_transform.localPosition.x, _endPos.y, _transform.localPosition.z); break;
                case Axis.z: ma.endValue = new Vector3(_transform.localPosition.x, _transform.localPosition.y, _endPos.z); break;
                case Axis.All: ma.endValue = _endPos; break;
            }

            // Return MA for other functions
            return INTERNAL_AMAlocalMoveTransform(ma, _transform, _selectedAxis, _duration, _snapToEndValue);
        }

        // ------------------------------------------ [ INTERNAL FUNCTIONS ] ------------------------------------------ //

        private static MA INTERNAL_AMAlocalMoveTransform(MA _ma, Transform _transform, Axis _selectedAxis, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            _ma.startValue = _transform.localPosition;
            _ma.duration = _duration;
            _ma.selectedAxis = _selectedAxis;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_ma.coroutine);

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
        public static MA AMAmove(this RectTransform _rectTransform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateMoveRectTransformMA(_rectTransform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endPos, _rectTransform.position.y, _rectTransform.position.z); break;
                case Axis.y: ma.endValue = new Vector3(_rectTransform.position.x, _endPos, _rectTransform.position.z); break;
                case Axis.z: ma.endValue = new Vector3(_rectTransform.position.x, _rectTransform.position.y, _endPos); break;
                case Axis.All: ma.endValue = new Vector3(_endPos, _endPos, _endPos); break;
            }

            // Return MA for other functions
            return INTERNAL_AMAmoveRectTransform(ma, _rectTransform, _selectedAxis, _duration, _snapToEndValue);
        }

        // Using Vector3 as End pos
        /// <summary>
        /// Moves Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA AMAmove(this RectTransform _rectTransform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateMoveTransformMA(_rectTransform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endPos.x, _rectTransform.position.y, _rectTransform.position.z); break;
                case Axis.y: ma.endValue = new Vector3(_rectTransform.position.x, _endPos.y, _rectTransform.position.z); break;
                case Axis.z: ma.endValue = new Vector3(_rectTransform.position.x, _rectTransform.position.y, _endPos.z); break;
                case Axis.All: ma.endValue = _endPos; break;
            }

            // Return MA for other functions
            return INTERNAL_AMAmoveRectTransform(ma, _rectTransform, _selectedAxis, _duration, _snapToEndValue);
        }

        // ------------------------------------------ [ INTERNAL FUNCTIONS ] ------------------------------------------ //

        private static MA INTERNAL_AMAmoveRectTransform(MA _ma, RectTransform _rectTransform, Axis _selectedAxis, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            _ma.startValue = _rectTransform.position;
            _ma.duration = _duration;
            _ma.selectedAxis = _selectedAxis;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_ma.coroutine);

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
        public static MA AMAlocalMove(this RectTransform _rectTransform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateMoveLocalRectTransformMA(_rectTransform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endPos, _rectTransform.localPosition.y, _rectTransform.localPosition.z); break;
                case Axis.y: ma.endValue = new Vector3(_rectTransform.localPosition.x, _endPos, _rectTransform.localPosition.z); break;
                case Axis.z: ma.endValue = new Vector3(_rectTransform.localPosition.x, _rectTransform.localPosition.y, _endPos); break;
                case Axis.All: ma.endValue = new Vector3(_endPos, _endPos, _endPos); break;
            }

            // Return MA for other functions
            return INTERNAL_AMAlocalMoveRectTransform(ma, _rectTransform, _selectedAxis, _duration, _snapToEndValue);
        }

        // Using Vector3 as End pos
        /// <summary>
        /// Moves Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA AMAlocalMove(this RectTransform _rectTransform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateMoveLocalRectTransformMA(_rectTransform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endPos.x, _rectTransform.localPosition.y, _rectTransform.localPosition.z); break;
                case Axis.y: ma.endValue = new Vector3(_rectTransform.localPosition.x, _endPos.y, _rectTransform.localPosition.z); break;
                case Axis.z: ma.endValue = new Vector3(_rectTransform.localPosition.x, _rectTransform.localPosition.y, _endPos.z); break;
                case Axis.All: ma.endValue = _endPos; break;
            }

            // Return MA for other functions
            return INTERNAL_AMAlocalMoveRectTransform(ma, _rectTransform, _selectedAxis, _duration, _snapToEndValue);
        }

        // ------------------------------------------ [ INTERNAL FUNCTIONS ] ------------------------------------------ //

        private static MA INTERNAL_AMAlocalMoveRectTransform(MA _ma, RectTransform _rectTransform, Axis _selectedAxis, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            _ma.startValue = _rectTransform.localPosition;
            _ma.duration = _duration;
            _ma.selectedAxis = _selectedAxis;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_ma.coroutine);

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
        public static MA AMAanchoredPosMove(this RectTransform _rectTransform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateMoveLocalRectTransformMA(_rectTransform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endPos, _rectTransform.anchoredPosition.y); break;
                case Axis.y: ma.endValue = new Vector3(_rectTransform.anchoredPosition.x, _endPos); break;
                case Axis.z: Debug.LogError("Z axis is not available for Anchored Positions. Please refer to a 3D Anchored Position instead."); break;
                case Axis.All: ma.endValue = new Vector3(_endPos, _endPos, _endPos); break;
            }

            // Return MA for other functions
            return INTERNAL_AMAanchoredPositionMoveRectTransform(ma, _rectTransform, _selectedAxis, _duration, _snapToEndValue);
        }

        // Using Vector2 as End pos
        /// <summary>
        /// Moves Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA AMAanchoredPosMove(this RectTransform _rectTransform, Axis _selectedAxis, Vector2 _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateMoveLocalRectTransformMA(_rectTransform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector2(_endPos.x, _rectTransform.anchoredPosition.y); break;
                case Axis.y: ma.endValue = new Vector2(_rectTransform.anchoredPosition.x, _endPos.y); break;
                case Axis.z: Debug.LogError("Z axis is not available for Anchored Positions. Please refer to a 3D Anchored Position instead."); break;
                case Axis.All: ma.endValue = _endPos; break;
            }

            // Return MA for other functions
            return INTERNAL_AMAanchoredPositionMoveRectTransform(ma, _rectTransform, _selectedAxis, _duration, _snapToEndValue);
        }

        // ------------------------------------------ [ INTERNAL FUNCTIONS ] ------------------------------------------ //

        private static MA INTERNAL_AMAanchoredPositionMoveRectTransform(MA _ma, RectTransform _rectTransform, Axis _selectedAxis, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            _ma.startValue = _rectTransform.anchoredPosition;
            _ma.duration = _duration;
            _ma.selectedAxis = _selectedAxis;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_ma.coroutine);

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
        public static MA AMAanchoredPos3dMove(this RectTransform _rectTransform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateMoveAnchoredPosition3dRectTransformMA(_rectTransform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endPos, _rectTransform.anchoredPosition3D.y, _rectTransform.anchoredPosition3D.z); break;
                case Axis.y: ma.endValue = new Vector3(_rectTransform.anchoredPosition3D.x, _endPos, _rectTransform.anchoredPosition3D.z); break;
                case Axis.z: ma.endValue = new Vector3(_rectTransform.anchoredPosition3D.x, _rectTransform.anchoredPosition3D.y, _endPos); break;
                case Axis.All: ma.endValue = new Vector3(_endPos, _endPos, _endPos); break;
            }

            // Return MA for other functions
            return INTERNAL_AMAanchoredPosition3dMoveRectTransform(ma, _rectTransform, _selectedAxis, _duration, _snapToEndValue);
        }

        // Using Vector3 as End pos
        /// <summary>
        /// Moves Game Object to a specific position.
        /// </summary>
        /// <param name="_selectedAxis">Axis on which to apply motion</param>
        /// <param name="_endPos">Final position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        /// <param name="_snapToEndValue">Snaps object to end position once motion is finished. Defaulted to "True"</param>
        public static MA AMAanchoredPos3dMove(this RectTransform _rectTransform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MA ma = INTERNAL_CreateMoveAnchoredPosition3dRectTransformMA(_rectTransform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endValue = new Vector3(_endPos.x, _rectTransform.anchoredPosition3D.y, _rectTransform.anchoredPosition3D.z); break;
                case Axis.y: ma.endValue = new Vector3(_rectTransform.anchoredPosition3D.x, _endPos.y, _rectTransform.anchoredPosition3D.z); break;
                case Axis.z: ma.endValue = new Vector3(_rectTransform.anchoredPosition3D.x, _rectTransform.anchoredPosition3D.y, _endPos.z); break;
                case Axis.All: ma.endValue = _endPos; break;
            }

            // Return MA for other functions
            return INTERNAL_AMAanchoredPosition3dMoveRectTransform(ma, _rectTransform, _selectedAxis, _duration, _snapToEndValue);
        }

        // ------------------------------------------ [ INTERNAL FUNCTIONS ] ------------------------------------------ //

        private static MA INTERNAL_AMAanchoredPosition3dMoveRectTransform(MA _ma, RectTransform _rectTransform, Axis _selectedAxis, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            _ma.startValue = _rectTransform.anchoredPosition3D;
            _ma.duration = _duration;
            _ma.selectedAxis = _selectedAxis;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_ma.coroutine);

            return _ma;
        }
        #endregion
        #endregion
    }
}