//
// • Ama Motion Automatizer
// • [ Motions Methods ]
// • By Amaryne Bréand
// • Last updated: 05/01/2024
//

using UnityEngine;
using System.Collections;
using static AMA.AMAMain;

namespace AMA
{
    public static class AMAMotions
    {
        #region Transform
        #region Move
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
            MA ma = INTERNAL_CreateTransformMA(_transform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endPos = new Vector3(_endPos, _transform.position.y, _transform.position.z); break;
                case Axis.y: ma.endPos = new Vector3(_transform.position.x, _endPos, _transform.position.z); break;
                case Axis.z: ma.endPos = new Vector3(_transform.position.x, _transform.position.y, _endPos); break;
                case Axis.All: ma.endPos = new Vector3(_endPos, _endPos, _endPos); break;
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
            MA ma = INTERNAL_CreateTransformMA(_transform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endPos = new Vector3(_endPos.x, _transform.position.y, _transform.position.z); break;
                case Axis.y: ma.endPos = new Vector3(_transform.position.x, _endPos.y, _transform.position.z); break;
                case Axis.z: ma.endPos = new Vector3(_transform.position.x, _transform.position.y, _endPos.z); break;
                case Axis.All: ma.endPos = _endPos; break;
            }

            // Return MA for other functions
            return INTERNAL_AMAmoveTransform(ma, _transform, _selectedAxis, _duration, _snapToEndValue);
        }

        private static MA INTERNAL_AMAmoveTransform(MA _ma, Transform _transform, Axis _selectedAxis, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            _ma.startPos = _transform.position;
            _ma.duration = _duration;
            _ma.selectedAxis = _selectedAxis;
            _ma.snapToEndValue = _snapToEndValue;
            
            // Apply MA
            _ma.coroutine = _ma.MoveToPos();
            AMACoroutineRunner.Instance.StartCoroutine(_ma.coroutine);

            return _ma;
        }
        #endregion

        #region Local move
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
            MA ma = INTERNAL_CreateLocalTransformMA(_transform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endPos = new Vector3(_endPos, _transform.localPosition.y, _transform.localPosition.z); break;
                case Axis.y: ma.endPos = new Vector3(_transform.localPosition.x, _endPos, _transform.localPosition.z); break;
                case Axis.z: ma.endPos = new Vector3(_transform.localPosition.x, _transform.localPosition.y, _endPos); break;
                case Axis.All: ma.endPos = new Vector3(_endPos, _endPos, _endPos); break;
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
            MA ma = INTERNAL_CreateLocalTransformMA(_transform);

            // Set position according to axis
            switch (_selectedAxis)
            {
                case Axis.x: ma.endPos = new Vector3(_endPos.x, _transform.localPosition.y, _transform.localPosition.z); break;
                case Axis.y: ma.endPos = new Vector3(_transform.localPosition.x, _endPos.y, _transform.localPosition.z); break;
                case Axis.z: ma.endPos = new Vector3(_transform.localPosition.x, _transform.localPosition.y, _endPos.z); break;
                case Axis.All: ma.endPos = _endPos; break;
            }

            // Return MA for other functions
            return INTERNAL_AMAlocalMoveTransform(ma, _transform, _selectedAxis, _duration, _snapToEndValue);
        }

        private static MA INTERNAL_AMAlocalMoveTransform(MA _ma, Transform _transform, Axis _selectedAxis, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            _ma.startPos = _transform.localPosition;
            _ma.duration = _duration;
            _ma.selectedAxis = _selectedAxis;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MoveToPos();
            AMACoroutineRunner.Instance.StartCoroutine(_ma.coroutine);

            return _ma;
        }
        #endregion
        #endregion

        #region Motion Coroutines
        public static IEnumerator MoveToPos(this MA _ma)
        {
            /// PLACEHOLDER SOLUTION
            // Sacrifices first frame in order to get every settings applied first
            yield return null; // so long little frame o7

            // Wait for delay (if there is one)
            if (_ma.delay > 0) { yield return new WaitForSeconds(_ma.delay); }

            // Execute function when MA starts its journey (if there is one)
            if (_ma.onStartFunc != null) { _ma.onStartFunc(); }

            // Set up calculations (big brain timeee)
            Vector3 initialPosition = _ma.startPos;
            Vector3 targetPosition = _ma.endPos;
            Vector3 externalOffset = Vector3.zero; // Tracks external movement
            float elapsedTime = 0f;

            // While time is cooling down
            while (elapsedTime < _ma.duration)
            {
                elapsedTime += Time.deltaTime;

                float easedTime = 0;

                // If selected curve is a custom one (a.k.a. uses Unity's Animation Curves)
                if (_ma.animationCurve != null)
                {
                    float normalizedTime = Mathf.Clamp01(elapsedTime / _ma.duration); // Get progression between 0 & 1
                    easedTime = _ma.animationCurve.Evaluate(normalizedTime); // Apply animation curve
                }
                // If selected curve is a regular one
                else
                {
                    easedTime = _ma.curveDelegate(elapsedTime, 0, 1, _ma.duration);
                }

                // Calculate interpolated position
                Vector3 interpolatedPosition = Vector3.LerpUnclamped(initialPosition, targetPosition, easedTime);

                // Set position according to axis
                switch (_ma.selectedAxis)
                {
                    case Axis.x: _ma.SetModifiedValue(new Vector3(interpolatedPosition.x + externalOffset.x, _ma.GetModifiedValue().y, _ma.GetModifiedValue().z)); break;
                    case Axis.y: _ma.SetModifiedValue(new Vector3(_ma.GetModifiedValue().x, interpolatedPosition.y + externalOffset.y, _ma.GetModifiedValue().z)); break;
                    case Axis.z: _ma.SetModifiedValue(new Vector3(_ma.GetModifiedValue().x, _ma.GetModifiedValue().y, interpolatedPosition.z + externalOffset.z)); break;
                    case Axis.All: _ma.SetModifiedValue(interpolatedPosition + externalOffset); break;
                }

                // Track any external movement since the last frame
                externalOffset += _ma.GetModifiedValue() - (interpolatedPosition + externalOffset);

                yield return null;
            }

            // Ensures that object has reached its final position
            if (_ma.snapToEndValue)
            {
                switch (_ma.selectedAxis)
                {
                    case Axis.x: _ma.SetModifiedValue(new Vector3(targetPosition.x, _ma.GetModifiedValue().y, _ma.GetModifiedValue().z)); break;
                    case Axis.y: _ma.SetModifiedValue(new Vector3(_ma.GetModifiedValue().x, targetPosition.y, _ma.GetModifiedValue().z)); break;
                    case Axis.z: _ma.SetModifiedValue(new Vector3(_ma.GetModifiedValue().x, _ma.GetModifiedValue().y, targetPosition.z)); break;
                    case Axis.All: _ma.SetModifiedValue(targetPosition); break;
                }
            }

            // Execute function when MA has finished its journey (if there is one)
            if (_ma.onCompleteFunc != null) { _ma.onCompleteFunc(); }

            // Destroy MA
            _ma.INTERNAL_Destroy();
        } // ok goodnight
        #endregion
    }

}