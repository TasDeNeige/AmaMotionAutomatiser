//
// • Ama Motion Automatizer
// • [ Main Coroutine ]
// • By Amaryne Bréand
// • Last updated: 30/03/2025
//

using UnityEngine;
using System.Collections;
using static AMA.AMAMain;

namespace AMA
{
    public static class AMACoroutine
    {
        public static IEnumerator MotionToEndValue<T>(this MA<T> _ma)
        {
            // Ensure object is still accessible (otherwise get out of coroutine)
            if (!_ma.GetAvailability()) yield break;

            // Wait for delay (if there is one)
            if (_ma.delay > 0) { yield return new WaitForSeconds(_ma.delay); }

            // Execute function when MA starts its journey (if there is one)
            if (_ma.onStartFunc != null) { _ma.onStartFunc(); }

            // Set up calculations (big brain timeee)
            T initialPosition = _ma.startValue;
            T targetPosition = _ma.endValue;
            T externalOffset = _ma.ZeroValue(); // Tracks external movement
            bool hasWentThroughFirstFrame = false;
            float elapsedTime = 0f;

            // While time is cooling down
            while (elapsedTime < _ma.duration)
            {
                // Ensure object is still accessible (otherwise get out of coroutine)
                if (!_ma.GetAvailability()) yield break;

                // Call late start function
                if (!hasWentThroughFirstFrame)
                {
                    if (elapsedTime > 0f)
                    {
                        hasWentThroughFirstFrame = true;
                        if (_ma.onLateStartFunc != null) { _ma.onLateStartFunc(); }
                    }
                }

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
                T interpolatedValue = _ma.Lerp(initialPosition, targetPosition, easedTime);

                // Set position according to axis
                _ma.SetModifiedValue(_ma.ValueAccordingToAxis(_ma.selectedAxis, interpolatedValue, externalOffset));

                // Track any external movement since the last frame
                externalOffset = _ma.GetExternalOffset(interpolatedValue, externalOffset);

                yield return null;
            }

            // Ensure object is still accessible (otherwise get out of coroutine)
            if (!_ma.GetAvailability()) yield break;

            // Ensures that object has reached its final position
            if (_ma.snapToEndValue) _ma.SetModifiedValue(_ma.ApplyAxisMask(_ma.selectedAxis, targetPosition));

            // Execute function when MA has finished its journey (if there is one)
            if (_ma.onCompleteFunc != null) { _ma.onCompleteFunc(); }

            // Destroy MA
            _ma.INTERNAL_Destroy();
        } // ok goodnight
    }
}
