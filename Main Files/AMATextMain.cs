//
// • Ama Motion Automatiser
// • [ Text Main file ]
// • By Amaryne Bréand
//

using System.Collections.Generic;
using UnityEngine;

namespace AMA
{
    public static class AMATextMain
    {
        public enum TagType { WAVY, FALLING_DOWN, SHAKE };
        public const string wavyTag = "wavy";
        public const string fallDownTag = "fall_down";
        public const string shakeTag = "shake";

        #region Main class
        abstract public class TextMA
        {
            TagType tagType;

            // Values
            int startId;
            int endId;

            // Method
            public abstract Vector3 Animate(Vector3 _vertPos, int _currentChar);

            // Creator
            public TextMA() { }

            // Destructor
            ~TextMA() { /*this.INTERNAL_Destroy();*/ }

            #region Getters/Setters
            public TagType TagType { get => tagType; }
            public int StartId { get => startId; set => startId = value; }
            public int EndId { get => endId; set => endId = value; }
            #endregion
        }
        #endregion

        #region Destructor
        public static void INTERNAL_Destroy(this TextMA _ma)
        {
        }
        #endregion

        #region Overriders
        // Wavy
        public class TextMA_Wavy : TextMA
        {
            // Values
            float waveSpeed = 2f;
            float waveIntensity = 10.0f;
            float displacementIntensity = 0.01f;

            // Constructors
            public TextMA_Wavy() { }
            public TextMA_Wavy(int _startId, int _endId, float _waveSpeed, float _waveIntensity, float _displacementIntensity)
            {
                StartId = _startId;
                EndId = _endId;

                waveSpeed = _waveSpeed == float.MinValue ? waveSpeed : _waveSpeed;
                waveIntensity = _waveIntensity == float.MinValue ? waveIntensity : _waveIntensity;
                displacementIntensity = _displacementIntensity == float.MinValue ? displacementIntensity : _displacementIntensity;
            }

            // Method
            public override Vector3 Animate(Vector3 _vertPos, int _currentChar)
            {
                return _vertPos + new Vector3(0, Mathf.Sin(-(Time.time * waveSpeed + -_vertPos.x * displacementIntensity)) * waveIntensity, 0);
            }
        }

        // Shake
        public class TextMA_Shake : TextMA
        {
            // Values
            float displacementIntensity = 0.1f;
            int frameDelay = 4;
            int nbDisplacements = 20;
            List<float> shakeDisplacements = new List<float>();

            // Constructors
            public TextMA_Shake() { }
            public TextMA_Shake(int _startId, int _endId, float _displacementIntensity, int _frameDelay, int _nbDisplacements)
            {
                StartId = _startId;
                EndId = _endId;

                displacementIntensity = _displacementIntensity == float.MinValue ? displacementIntensity : _displacementIntensity;
                frameDelay = _frameDelay == int.MinValue ? frameDelay : _frameDelay;
                nbDisplacements = _nbDisplacements == int.MinValue ? nbDisplacements : _nbDisplacements;

                // Initialize displacements
                for (int i = 0; i < nbDisplacements; i++)
                {
                    shakeDisplacements.Add(Random.Range(-100f, 100f));
                }
            }

            // Method
            public override Vector3 Animate(Vector3 _vertPos, int _currentChar)
            {
                // Delay between shakes
                int frameId = Time.frameCount + (frameDelay - (Time.frameCount % frameDelay));

                // Set up displacement
                float displacementX = shakeDisplacements[(_currentChar + frameId) % nbDisplacements] * displacementIntensity;
                float displacementY = shakeDisplacements[(_currentChar + (frameId * 2)) % nbDisplacements] * displacementIntensity;

                // Displace each verts by according list's displacement
                return _vertPos + new Vector3(displacementX, displacementY, 0);
            }
        }

        // Fall down
        public class TextMA_FallDown : TextMA
        {
            // Values
            float fallSpeed = 2f;

            // Constructors
            public TextMA_FallDown() { }
            public TextMA_FallDown(int _startId, int _endId, float _fallSpeed)
            {
                StartId = _startId;
                EndId = _endId;

                fallSpeed = _fallSpeed == float.MinValue ? fallSpeed : _fallSpeed;
            }

            // Method
            public override Vector3 Animate(Vector3 _vertPos, int _currentChar)
            {
                return _vertPos + new Vector3(0, Mathf.Atan(-(Time.time * fallSpeed + -_vertPos.x * 0.01f)) * 10.0f, 0);
            }
        }
        #endregion
    }
}
