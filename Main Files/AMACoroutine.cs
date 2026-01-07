//
// • Ama Motion Automatiser
// • [ Main Coroutine ]
// • By Amaryne Bréand
//

using System.Collections;
using UnityEngine;
using static AMA.AMAMain;

namespace AMA
{
    public static class AMACoroutine
    {
        // Main Coroutine (used for multiple tweens)
        public static IEnumerator MotionToEndValue<T>(this MA<T> _ma)
        {
            // Ensure object is still accessible (otherwise get out of coroutine)
            if (!_ma.GetAvailability()) yield break;

            // Wait for delay (if there is one)
            if (_ma.delay > 0) { yield return new WaitForSeconds(_ma.delay); }

            // Execute function when MA starts its journey (if there is one)
            if (_ma.onStartFunc != null) { _ma.onStartFunc(); }

            // Set MA to start value (if there is one)
            if (_ma.hasFromValue)
            {
                _ma.SetModifiedValue(_ma.ApplyAxisMask(_ma.selectedAxis, _ma.fromValue));
                _ma.startValue = _ma.fromValue;
            }

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







        // Shake Coroutine
        public static IEnumerator Shake(this MAShake _ma)
        {
            // Ensure object is still accessible (otherwise get out of coroutine)
            if (!_ma.GetAvailability()) yield break;

            // Wait for delay (if there is one)
            if (_ma.delay > 0) { yield return new WaitForSeconds(_ma.delay); }

            // Execute function when MA starts its journey (if there is one)
            if (_ma.onStartFunc != null) { _ma.onStartFunc(); }

            // Set MA to start value (if there is one)
            if (_ma.hasFromValue)
            {
                _ma.SetModifiedValue(_ma.ApplyAxisMask(_ma.selectedAxis, _ma.fromValue));
                _ma.startValue = _ma.fromValue;
            }

            // Set up calculations
            UnityEngine.Vector3 initialPosition = _ma.startValue;
            UnityEngine.Vector3 externalOffset = _ma.ZeroValue(); // Tracks external movement
            Vector3 externalFinalPosition = _ma.ZeroValue();
            Vector3 lastShake = _ma.ZeroValue();
            bool hasWentThroughFirstFrame = false;
            float elapsedTime = 0f;
            float lastShakeTimestamp = Time.time;
            float startTimeStamp = Time.time;

            // While time is cooling down
            while (elapsedTime <= _ma.duration)
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

                elapsedTime = lastShakeTimestamp - startTimeStamp;

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

                // Calculate interpolated radius
                float interpolatedRadius = Mathf.Lerp(0.0f, _ma.ShakeRadius, easedTime);


                // Replace transform to correctly track external offset (without considering added shake)
                _ma.SetModifiedValue(_ma.ValueAccordingToAxis(_ma.selectedAxis,  (_ma.GetModifiedValue() - lastShake), externalOffset));
                externalOffset = _ma.GetExternalOffset(_ma.transform.position, externalOffset);
                
                // Change shake position
                UnityEngine.Vector3 shakePosition = (Random.insideUnitSphere * interpolatedRadius);
                _ma.SetModifiedValue(_ma.ValueAccordingToAxis(_ma.selectedAxis,  (_ma.GetModifiedValue())+ shakePosition, externalOffset));
                lastShake = shakePosition;

                // Retrieve time
                lastShakeTimestamp = Time.time;

                // Add delay between each shakes
                if (_ma.DelayBetweenShakes > 0.0f) // If there is a delay
                {
                    // Ensure that no useless wait time is added if MA finishes before next shake
                    if (elapsedTime + _ma.DelayBetweenShakes > _ma.duration)
                    {
                        yield return new WaitForSeconds(_ma.duration - elapsedTime);
                    }
                    else
                    {
                        yield return new WaitForSeconds(_ma.DelayBetweenShakes);
                    }
                }

                yield return null;
            }

            // Ensure object is still accessible (otherwise get out of coroutine)
            if (!_ma.GetAvailability()) yield break;

            // Ensures that object comes back to place
            if (_ma.snapToEndValue) _ma.SetModifiedValue(_ma.ApplyAxisMask(_ma.selectedAxis, _ma.transform.position - lastShake));

            // Execute function when MA has finished its journey (if there is one)
            if (_ma.onCompleteFunc != null) { _ma.onCompleteFunc(); }

            // Destroy MA
            _ma.INTERNAL_Destroy();
        }
    }
}
