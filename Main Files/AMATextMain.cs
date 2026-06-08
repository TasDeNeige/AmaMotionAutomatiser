//
// • Ama Motion Automatiser
// • [ Text Main file ]
// • By Amaryne Bréand
//

using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AMA
{
    public static class AMATextMain
    {
        public enum TagType { WAVY, FALL_DOWN, SHAKE, RAINBOW };
        public const string wavyTag = "wavy";
        public const string fallDownTag = "fall_down";
        public const string shakeTag = "shake";
        public const string rainbowTag = "rainbow";

        #region Main class
        abstract public class TextMA
        {
            protected TagType tagType;

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
            public TextMA_Wavy() { tagType = TagType.WAVY; }
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
                return _vertPos + new Vector3(0,
                                                                Mathf.Sin(-(Time.time * waveSpeed + -_vertPos.x * displacementIntensity)) * waveIntensity,
                                                                0);
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
            public TextMA_Shake() { tagType = TagType.SHAKE; }
            public TextMA_Shake(int _startId, int _endId, float _displacementIntensity, int _frameDelay, int _nbDisplacements)
            {
                tagType = TagType.SHAKE;

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
            float fallSpeed = 10.0f;
            float fallHeight = 100.0f;
            float displacementSpeed = 0.01f;

            // Constructors
            public TextMA_FallDown() { tagType = TagType.FALL_DOWN; }
            public TextMA_FallDown(int _startId, int _endId, float _fallSpeed, float _displacementSpeed, float _fallHeight)
            {
                tagType = TagType.FALL_DOWN;

                StartId = _startId;
                EndId = _endId;

                fallSpeed = _fallSpeed == float.MinValue ? fallSpeed : _fallSpeed;
                displacementSpeed = _displacementSpeed == float.MinValue ? displacementSpeed : _displacementSpeed;
                fallHeight = _fallHeight == float.MinValue ? fallHeight : _fallHeight;
            }

            // Method
            public override Vector3 Animate(Vector3 _vertPos, int _currentChar)
            {
                return _vertPos + new Vector3(0,
                                                                -(Mathf.Atan(Time.time * fallSpeed + -_vertPos.x * displacementSpeed) / Mathf.PI * 2 * fallHeight) + fallHeight,
                                                                0);
             }
        }

        // Rainbow
        public class TextMA_Rainbow : TextMA
        {
            // Values
            float hueSpeed = 10.0f;
            float hueDisparity = 10.0f;

            // Elements
            TMP_Text textComponent;
            Color32[] newVertexColors;

            // Constructors
            public TextMA_Rainbow() { tagType = TagType.RAINBOW; }
            public TextMA_Rainbow(int _startId, int _endId, TMP_Text _textComponent, float _hueSpeed, float _hueDisparity)
            {
                tagType = TagType.RAINBOW;

                StartId = _startId;
                EndId = _endId;
                textComponent = _textComponent;

                hueSpeed = _hueSpeed == float.MinValue ? hueSpeed : _hueSpeed;
                hueDisparity = _hueDisparity == float.MinValue ? hueDisparity : _hueDisparity;
            }

            // Method
            public override Vector3 Animate(Vector3 _vertPos, int _currentChar)
            {
                Debug.LogWarning("Rainbow effect's animation function is 'RainbowAnimation()'. Please do not use 'Animate()'.");
                return Vector3.zero;
            }

            public void RainbowAnimation(int _vertIndex, int _currentChar)
            {
                newVertexColors = textComponent.textInfo.meshInfo[textComponent.textInfo.characterInfo[_currentChar].materialReferenceIndex].colors32;

                int vertColorId = (4 * (_currentChar - StartId)) + _vertIndex;
                Debug.Log("Char: " + _currentChar + " | Vert: " + _vertIndex + " = " + vertColorId);

                newVertexColors[vertColorId] = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);

                // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer
                textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }
        }
        #endregion
    }
}
