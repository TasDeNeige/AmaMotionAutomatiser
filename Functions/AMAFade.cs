//
// • Ama Motion Automatiser
// • [ Fading Methods ]
// • By Amaryne Bréand
//

using UnityEngine;
using static AMA.AMAMain;
using TMPro;

namespace AMA
{
    public static class AMAFade
    {
        #region Color fade
        #region Material
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        /// <summary>
        /// Color material to a specific color.
        /// </summary>
        /// <param name="_endColor">Final color</param>
        /// <param name="_duration">Time taken to color to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA<Color> AMAfade(this Material _material, Color _endColor, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAFadeMaterial _ma = new MAFadeMaterial();
            _ma.unityObject = _material;
            _ma.material = _material;
            _ma.startValue = _ma.material.color;
            _ma.endValue = _endColor;
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_material, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region Image
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        /// <summary>
        /// Color Unity UI Image color to a specific color.
        /// </summary>
        /// <param name="_endColor">Final color</param>
        /// <param name="_duration">Time taken to color to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA<Color> AMAfade(this UnityEngine.UI.Image _image, Color _endColor, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAFadeImage _ma = new MAFadeImage();
            _ma.unityObject = _image;
            _ma.image = _image;
            _ma.startValue = _ma.image.color;
            _ma.endValue = _endColor;
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_image, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region TextMeshPro
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        /// <summary>
        /// Color TMP Text color to a specific color.
        /// </summary>
        /// <param name="_endColor">Final color</param>
        /// <param name="_duration">Time taken to color to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA<Color> AMAfade(this TMP_Text _text, Color _endColor, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAFadeTmpText _ma = new MAFadeTmpText();
            _ma.unityObject = _text;
            _ma.text = _text;
            _ma.startValue = _ma.text.color;
            _ma.endValue = _endColor;
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_text, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region Color
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        /// <summary>
        /// Color color to a specific color.
        /// </summary>
        /// <param name="_endColor">Final color</param>
        /// <param name="_duration">Time taken to color to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA<Color> AMAfade(this Color _color, Color _endColor, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAFadeColor _ma = new MAFadeColor();
            _ma.unityObject = _color;
            _ma.color = _color;
            _ma.startValue = _ma.color;
            _ma.endValue = _endColor;
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_color, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion
        #endregion

        #region Fade Alpha
        #region Material
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        /// <summary>
        /// Fade color material to a specific alpha.
        /// </summary>
        /// <param name="_endAlpha">Final alpha</param>
        /// <param name="_duration">Time taken to fade to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA<Color> AMAfadeAlpha(this Material _material, float _endAlpha, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAFadeMaterial _ma = new MAFadeMaterial();
            _ma.unityObject = _material;
            _ma.material = _material;
            _ma.startValue = _ma.material.color;
            _ma.endValue = new Color(_ma.startValue.r, _ma.startValue.g, _ma.startValue.b, _endAlpha);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_material, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region Image
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        /// <summary>
        /// Fade Unity UI Image color to a specific alpha.
        /// </summary>
        /// <param name="_endAlpha">Final alpha</param>
        /// <param name="_duration">Time taken to fade to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA<Color> AMAfadeAlpha(this UnityEngine.UI.Image _image, float _endAlpha, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAFadeImage _ma = new MAFadeImage();
            _ma.unityObject = _image;
            _ma.image = _image;
            _ma.startValue = _ma.image.color;
            _ma.endValue = new Color(_ma.startValue.r, _ma.startValue.g, _ma.startValue.b, _endAlpha);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_image, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region TextMeshPro
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        /// <summary>
        /// Fade TMP Text color to a specific alpha.
        /// </summary>
        /// <param name="_endAlpha">Final alpha</param>
        /// <param name="_duration">Time taken to fade to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA<Color> AMAfadeAlpha(this TMP_Text _text, float _endAlpha, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAFadeTmpText _ma = new MAFadeTmpText();
            _ma.unityObject = _text;
            _ma.text = _text;
            _ma.startValue = _ma.text.color;
            _ma.endValue = new Color(_ma.startValue.r, _ma.startValue.g, _ma.startValue.b, _endAlpha);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_text, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion

        #region Color
        // ------------------------------------------ [ PUBLIC FUNCTIONS ] ------------------------------------------ //

        /// <summary>
        /// Fade color to a specific alpha.
        /// </summary>
        /// <param name="_endAlpha">Final alpha</param>
        /// <param name="_duration">Time taken to fade to given value</param>
        /// <param name="_snapToEndValue">Snaps object to end value once motion is finished. Defaulted to "True"</param>
        public static MA<Color> AMAfadeAlpha(this Color _color, float _endAlpha, float _duration, bool _snapToEndValue = true)
        {
            // Set up MA
            MAFadeColor _ma = new MAFadeColor();
            _ma.unityObject = _color;
            _ma.color = _color;
            _ma.startValue = _ma.color;
            _ma.endValue = new Color(_ma.startValue.r, _ma.startValue.g, _ma.startValue.b, _endAlpha);
            _ma.duration = _duration;
            _ma.snapToEndValue = _snapToEndValue;

            // Apply MA
            _ma.coroutine = _ma.MotionToEndValue();
            AMACoroutineRunner.Instance.INTERNAL_StartCoroutine(_color, _ma.coroutine);

            // Return MA for other functions
            return _ma;
        }
        #endregion
        #endregion
    }
}
