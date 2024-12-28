//
// Ama Motion Automatizer
// [ Motions Methods ]
// • By Amaryne Bréand
// • Last updated: 21/12/2024
//

using UnityEngine;
using System.Collections;
using static AMA.AMAMain;

namespace AMA
{
    public static class AMAMotions
    {
        #region Movements

        /// <summary>
        /// Moves Game Object to a specific X position.
        /// </summary>
        /// <param name="_xEndPos">Final X position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        public static MA AMAmoveX(this Transform _transform, float _xEndPos, float _duration)
        {
            // Set up MA
            MA ma = CreateMA(_transform);
            ma.startPos = _transform.position;
            ma.endPos = new Vector3(_xEndPos, _transform.position.y, _transform.position.z);
            ma.duration = _duration;

            // Apply MA
            ma.coroutine = ma.MoveToPos();
            AMACoroutineRunner.Instance.StartCoroutine(ma.coroutine);

            // Return MA for other functions
            return ma;
        }

        /// <summary>
        /// Moves Game Object to a specific Y position.
        /// </summary>
        /// <param name="_yEndPos">Final Y position</param>
        /// <param name="_duration">Time taken to move to given position</param>
        public static MA AMAmoveY(this Transform _transform, float _yEndPos, float _duration)
        {
            // Set up MA
            MA ma = CreateMA(_transform);
            ma.startPos = _transform.position;
            ma.endPos = new Vector3(_transform.position.x, _yEndPos, _transform.position.z);
            ma.duration = _duration;

            // Apply MA
            ma.coroutine = ma.MoveToPos();
            AMACoroutineRunner.Instance.StartCoroutine(ma.coroutine);

            // Return MA for other functions
            return ma;
        }

        public static IEnumerator MoveToPos(this MA _ma)
        {
            // Do a first yield return in order to execute in last
            yield return null;

            // Execute function when MA starts its journey (if there is one)
            if (_ma.onStartFunc != null) { _ma.onStartFunc(); }

            // Wait for delay (if there is one)
            if (_ma.delay > 0) { yield return new WaitForSeconds(_ma.delay); }

            // Set up calculations (big brain timeee)
            Vector3 initialPosition = _ma.startPos;
            Vector3 targetPosition = _ma.endPos;
            float elapsedTime = 0f;

            // While time is cooling down
            while (elapsedTime < _ma.duration)
            {
                elapsedTime += Time.deltaTime;

                float easedTime = 0;
                // If selected curve is a custom one (aka uses Unity's Animation Curves)
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

                // Set position
                _ma.transform.position = Vector3.LerpUnclamped(initialPosition, targetPosition, easedTime);

                yield return null;
            }

            // Ensures that object has reached its final position
            _ma.transform.position = targetPosition;

            // Execute function when MA has finished its journey (if there is one)
            if (_ma.onCompleteFunc != null) { _ma.onCompleteFunc(); } // ok goodnight
        }
        #endregion
    }

}