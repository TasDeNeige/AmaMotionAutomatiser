//
// • Ama Motion Automatizer
// • [ Main Coroutine ]
// • By Amaryne Bréand
// • Last updated: 31/01/2025
//

using UnityEngine;
using System.Collections;
using static AMA.AMAMain;

namespace AMA
{
    public static class AMACoroutine
    {
        public static IEnumerator MotionToEndValue(this MA _ma)
        {
            // Wait for delay (if there is one)
            if (_ma.delay > 0) { yield return new WaitForSeconds(_ma.delay); }

            // Execute function when MA starts its journey (if there is one)
            if (_ma.onStartFunc != null) { _ma.onStartFunc(); }

            // Set up calculations (big brain timeee)
            Vector3 initialPosition = _ma.startValue;
            Vector3 targetPosition = _ma.endValue;
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
    }
}
