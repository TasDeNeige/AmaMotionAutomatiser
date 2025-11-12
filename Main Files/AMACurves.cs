//
// • Ama Motion Automatiser
// • [ Curves Methods ]
// • By Amaryne Bréand
//
// Easing code taken from https://gist.github.com/xanathar/735e17ac129a72a277ee
//

using UnityEngine;

namespace AMA
{
    #region Enum
    /// <summary>
    /// Robert Penner's curves. Visual representation available at https://easings.net.
    /// </summary>
    public enum Curves
    {
        Linear,
        Constant,
        Sine_In,
        Sine_Out,
        Sine_InOut,
        Sine_OutIn,
        Quad_In,
        Quad_Out,
        Quad_InOut,
        Quad_OutIn,
        Cubic_In,
        Cubic_Out,
        Cubic_InOut,
        Cubic_OutIn,
        Quart_In,
        Quart_Out,
        Quart_InOut,
        Quart_OutIn,
        Quint_In,
        Quint_Out,
        Quint_InOut,
        Quint_OutIn,
        Expo_In,
        Expo_Out,
        Expo_InOut,
        Expo_OutIn,
        Circ_In,
        Circ_Out,
        Circ_InOut,
        Circ_OutIn,
        Back_In,
        Back_Out,
        Back_InOut,
        Back_OutIn,
        Elastic_In,
        Elastic_Out,
        Elastic_InOut,
        Elastic_OutIn,
        Bounce_In,
        Bounce_Out,
        Bounce_InOut,
        Bounce_OutIn,

        CUSTOM
    };
    #endregion

    public static class AMACurves
    {
        #region Curves equations
        #region Linear
        /// <summary>
        /// Easing equation function for a simple linear tweening, with no easing.
        /// </summary>
        public static float Linear(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue * currentTime / duration + startValue;
        }
        #endregion

        #region Constant
        /// <summary>
        /// "Easing" function returning only the end value.
        /// </summary>
        public static float Constant(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue;
        }
        #endregion

        #region Expo
        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float ExpoEaseOut(float currentTime, float startValue, float endValue, float duration)
        {
            return (currentTime == duration) ? startValue + endValue : endValue * (-Mathf.Pow(2, -10 * currentTime / duration) + 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float ExpoEaseIn(float currentTime, float startValue, float endValue, float duration)
        {
            return (currentTime == 0) ? startValue : endValue * Mathf.Pow(2, 10 * (currentTime / duration - 1)) + startValue;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float ExpoEaseInOut(float currentTime, float startValue, float endValue, float duration)
        {
            if (currentTime == 0)
                return startValue;

            if (currentTime == duration)
                return startValue + endValue;

            if ((currentTime /= duration / 2) < 1)
                return endValue / 2 * Mathf.Pow(2, 10 * (currentTime - 1)) + startValue;

            return endValue / 2 * (-Mathf.Pow(2, -10 * --currentTime) + 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for an exponential (2^t) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float ExpoEaseOutIn(float currentTime, float startValue, float endValue, float duration)
        {
            if (currentTime < duration / 2)
                return ExpoEaseOut(currentTime * 2, startValue, endValue / 2, duration);

            return ExpoEaseIn((currentTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Circular
        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float CircEaseOut(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue * Mathf.Sqrt(1 - (currentTime = currentTime / duration - 1) * currentTime) + startValue;
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float CircEaseIn(float currentTime, float startValue, float endValue, float duration)
        {
            return -endValue * (Mathf.Sqrt(1 - (currentTime /= duration) * currentTime) - 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float CircEaseInOut(float currentTime, float startValue, float endValue, float duration)
        {
            if ((currentTime /= duration / 2) < 1)
                return -endValue / 2 * (Mathf.Sqrt(1 - currentTime * currentTime) - 1) + startValue;

            return endValue / 2 * (Mathf.Sqrt(1 - (currentTime -= 2) * currentTime) + 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float CircEaseOutIn(float currentTime, float startValue, float endValue, float duration)
        {
            if (currentTime < duration / 2)
                return CircEaseOut(currentTime * 2, startValue, endValue / 2, duration);

            return CircEaseIn((currentTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Quad
        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float QuadEaseOut(float currentTime, float startValue, float endValue, float duration)
        {
            return -endValue * (currentTime /= duration) * (currentTime - 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float QuadEaseIn(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue * (currentTime /= duration) * currentTime + startValue;
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float QuadEaseInOut(float currentTime, float startValue, float endValue, float duration)
        {
            if ((currentTime /= duration / 2) < 1)
                return endValue / 2 * currentTime * currentTime + startValue;

            return -endValue / 2 * ((--currentTime) * (currentTime - 2) - 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for a quadratic (t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float QuadEaseOutIn(float currentTime, float startValue, float endValue, float duration)
        {
            if (currentTime < duration / 2)
                return QuadEaseOut(currentTime * 2, startValue, endValue / 2, duration);

            return QuadEaseIn((currentTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Sine
        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float SineEaseOut(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue * Mathf.Sin(currentTime / duration * (Mathf.PI / 2)) + startValue;
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float SineEaseIn(float currentTime, float startValue, float endValue, float duration)
        {
            return -endValue * Mathf.Cos(currentTime / duration * (Mathf.PI / 2)) + endValue + startValue;
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float SineEaseInOut(float currentTime, float startValue, float endValue, float duration)
        {
            if ((currentTime /= duration / 2) < 1)
                return endValue / 2 * (Mathf.Sin(Mathf.PI * currentTime / 2)) + startValue;

            return -endValue / 2 * (Mathf.Cos(Mathf.PI * --currentTime / 2) - 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for a sinusoidal (sin(t)) easing in/out: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float SineEaseOutIn(float currentTime, float startValue, float endValue, float duration)
        {
            if (currentTime < duration / 2)
                return SineEaseOut(currentTime * 2, startValue, endValue / 2, duration);

            return SineEaseIn((currentTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Cubic
        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float CubicEaseOut(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue * ((currentTime = currentTime / duration - 1) * currentTime * currentTime + 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float CubicEaseIn(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue * (currentTime /= duration) * currentTime * currentTime + startValue;
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float CubicEaseInOut(float currentTime, float startValue, float endValue, float duration)
        {
            if ((currentTime /= duration / 2) < 1)
                return endValue / 2 * currentTime * currentTime * currentTime + startValue;

            return endValue / 2 * ((currentTime -= 2) * currentTime * currentTime + 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for a cubic (t^3) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float CubicEaseOutIn(float currentTime, float startValue, float endValue, float duration)
        {
            if (currentTime < duration / 2)
                return CubicEaseOut(currentTime * 2, startValue, endValue / 2, duration);

            return CubicEaseIn((currentTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Quartic
        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float QuartEaseOut(float currentTime, float startValue, float endValue, float duration)
        {
            return -endValue * ((currentTime = currentTime / duration - 1) * currentTime * currentTime * currentTime - 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float QuartEaseIn(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue * (currentTime /= duration) * currentTime * currentTime * currentTime + startValue;
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float QuartEaseInOut(float currentTime, float startValue, float endValue, float duration)
        {
            if ((currentTime /= duration / 2) < 1)
                return endValue / 2 * currentTime * currentTime * currentTime * currentTime + startValue;

            return -endValue / 2 * ((currentTime -= 2) * currentTime * currentTime * currentTime - 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for a quartic (t^4) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float QuartEaseOutIn(float currentTime, float startValue, float endValue, float duration)
        {
            if (currentTime < duration / 2)
                return QuartEaseOut(currentTime * 2, startValue, endValue / 2, duration);

            return QuartEaseIn((currentTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Quintic
        /// <summary>
        /// Easing equation function for a quintic (t^5) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float QuintEaseOut(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue * ((currentTime = currentTime / duration - 1) * currentTime * currentTime * currentTime * currentTime + 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float QuintEaseIn(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue * (currentTime /= duration) * currentTime * currentTime * currentTime * currentTime + startValue;
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float QuintEaseInOut(float currentTime, float startValue, float endValue, float duration)
        {
            if ((currentTime /= duration / 2) < 1)
                return endValue / 2 * currentTime * currentTime * currentTime * currentTime * currentTime + startValue;
            return endValue / 2 * ((currentTime -= 2) * currentTime * currentTime * currentTime * currentTime + 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for a quintic (t^5) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float QuintEaseOutIn(float currentTime, float startValue, float endValue, float duration)
        {
            if (currentTime < duration / 2)
                return QuintEaseOut(currentTime * 2, startValue, endValue / 2, duration);
            return QuintEaseIn((currentTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Elastic
        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float ElasticEaseOut(float currentTime, float startValue, float endValue, float duration)
        {
            if ((currentTime /= duration) == 1)
                return startValue + endValue;

            float p = duration * .3f;
            float s = p / 4;

            return (endValue * Mathf.Pow(2, -10 * currentTime) * Mathf.Sin((currentTime * duration - s) * (2 * Mathf.PI) / p) + endValue + startValue);
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float ElasticEaseIn(float currentTime, float startValue, float endValue, float duration)
        {
            if ((currentTime /= duration) == 1)
                return startValue + endValue;

            float p = duration * .3f;
            float s = p / 4;

            return -(endValue * Mathf.Pow(2, 10 * (currentTime -= 1)) * Mathf.Sin((currentTime * duration - s) * (2 * Mathf.PI) / p)) + startValue;
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float ElasticEaseInOut(float currentTime, float startValue, float endValue, float duration)
        {
            if ((currentTime /= duration / 2) == 2)
                return startValue + endValue;

            float p = duration * (.3f * 1.5f);
            float s = p / 4;

            if (currentTime < 1)
                return -.5f * (endValue * Mathf.Pow(2, 10 * (currentTime -= 1)) * Mathf.Sin((currentTime * duration - s) * (2 * Mathf.PI) / p)) + startValue;
            return endValue * Mathf.Pow(2, -10 * (currentTime -= 1)) * Mathf.Sin((currentTime * duration - s) * (2 * Mathf.PI) / p) * .5f + endValue + startValue;
        }

        /// <summary>
        /// Easing equation function for an elastic (exponentially decaying sine wave) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float ElasticEaseOutIn(float currentTime, float startValue, float endValue, float duration)
        {
            if (currentTime < duration / 2)
                return ElasticEaseOut(currentTime * 2, startValue, endValue / 2, duration);
            return ElasticEaseIn((currentTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion

        #region Bounce
        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float BounceEaseOut(float currentTime, float startValue, float endValue, float duration)
        {
            if ((currentTime /= duration) < (1f / 2.75f))
                return endValue * (7.5625f * currentTime * currentTime) + startValue;
            else if (currentTime < (2f / 2.75f))
                return endValue * (7.5625f * (currentTime -= (1.5f / 2.75f)) * currentTime + .75f) + startValue;
            else if (currentTime < (2.5f / 2.75f))
                return endValue * (7.5625f * (currentTime -= (2.25f / 2.75f)) * currentTime + .9375f) + startValue;
            else
                return endValue * (7.5625f * (currentTime -= (2.625f / 2.75f)) * currentTime + .984375f) + startValue;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float BounceEaseIn(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue - BounceEaseOut(duration - currentTime, 0, endValue, duration) + startValue;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float BounceEaseInOut(float currentTime, float startValue, float endValue, float duration)
        {
            if (currentTime < duration / 2)
                return BounceEaseIn(currentTime * 2, 0, endValue, duration) * .5f + startValue;
            else
                return BounceEaseOut(currentTime * 2 - duration, 0, endValue, duration) * .5f + endValue * .5f + startValue;
        }

        /// <summary>
        /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float BounceEaseOutIn(float currentTime, float startValue, float endValue, float duration)
        {
            if (currentTime < duration / 2)
                return BounceEaseOut(currentTime * 2, startValue, endValue / 2, duration);
            return BounceEaseIn((currentTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }

        #endregion

        #region Back
        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out: 
        /// decelerating from zero velocity.
        /// </summary>
        public static float BackEaseOut(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue * ((currentTime = currentTime / duration - 1) * currentTime * ((1.70158f + 1) * currentTime + 1.70158f) + 1) + startValue;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in: 
        /// accelerating from zero velocity.
        /// </summary>
        public static float BackEaseIn(float currentTime, float startValue, float endValue, float duration)
        {
            return endValue * (currentTime /= duration) * currentTime * ((1.70158f + 1) * currentTime - 1.70158f) + startValue;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out: 
        /// acceleration until halfway, then deceleration.
        /// </summary>
        public static float BackEaseInOut(float currentTime, float startValue, float endValue, float duration)
        {
            float s = 1.70158f;
            if ((currentTime /= duration / 2) < 1)
                return endValue / 2 * (currentTime * currentTime * (((s *= (1.525f)) + 1) * currentTime - s)) + startValue;
            return endValue / 2 * ((currentTime -= 2) * currentTime * (((s *= (1.525f)) + 1) * currentTime + s) + 2) + startValue;
        }

        /// <summary>
        /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in: 
        /// deceleration until halfway, then acceleration.
        /// </summary>
        public static float BackEaseOutIn(float currentTime, float startValue, float endValue, float duration)
        {
            if (currentTime < duration / 2)
                return BackEaseOut(currentTime * 2, startValue, endValue / 2, duration);
            return BackEaseIn((currentTime * 2) - duration, startValue + endValue / 2, endValue / 2, duration);
        }
        #endregion
        #endregion

        public static CurveDelegate GetCurveFunction(Curves _curve)
        {
            switch (_curve)
            {
                case Curves.Linear: return Linear;
                case Curves.Constant: return Constant;
                case Curves.Sine_In: return SineEaseIn;
                case Curves.Sine_Out: return SineEaseOut;
                case Curves.Sine_InOut: return SineEaseInOut;
                case Curves.Sine_OutIn: return SineEaseOutIn;
                case Curves.Quad_In: return QuadEaseIn;
                case Curves.Quad_Out: return QuadEaseOut;
                case Curves.Quad_InOut: return QuadEaseInOut;
                case Curves.Quad_OutIn: return QuadEaseOutIn;
                case Curves.Cubic_In: return CubicEaseIn;
                case Curves.Cubic_Out: return CubicEaseOut;
                case Curves.Cubic_InOut: return CubicEaseInOut;
                case Curves.Cubic_OutIn: return CubicEaseOutIn;
                case Curves.Quart_In: return QuartEaseIn;
                case Curves.Quart_Out: return QuartEaseOut;
                case Curves.Quart_InOut: return QuartEaseInOut;
                case Curves.Quart_OutIn: return QuartEaseOutIn;
                case Curves.Quint_In: return QuintEaseIn;
                case Curves.Quint_Out: return QuintEaseOut;
                case Curves.Quint_InOut: return QuintEaseInOut;
                case Curves.Quint_OutIn: return QuintEaseOutIn;
                case Curves.Expo_In: return ExpoEaseIn;
                case Curves.Expo_Out: return ExpoEaseOut;
                case Curves.Expo_InOut: return ExpoEaseInOut;
                case Curves.Expo_OutIn: return ExpoEaseOutIn;
                case Curves.Circ_In: return CircEaseIn;
                case Curves.Circ_Out: return CircEaseOut;
                case Curves.Circ_InOut: return CircEaseInOut;
                case Curves.Circ_OutIn: return CircEaseOutIn;
                case Curves.Back_In: return BackEaseIn;
                case Curves.Back_Out: return BackEaseOut;
                case Curves.Back_InOut: return BackEaseInOut;
                case Curves.Back_OutIn: return BackEaseOutIn;
                case Curves.Elastic_In: return ElasticEaseIn;
                case Curves.Elastic_Out: return ElasticEaseOut;
                case Curves.Elastic_InOut: return ElasticEaseInOut;
                case Curves.Elastic_OutIn: return ElasticEaseOutIn;
                case Curves.Bounce_In: return BounceEaseIn;
                case Curves.Bounce_Out: return BounceEaseOut;
                case Curves.Bounce_InOut: return BounceEaseInOut;
                case Curves.Bounce_OutIn: return BounceEaseOutIn;

                default:
                    if (_curve == Curves.CUSTOM) Debug.Assert(false, "To use a CUSTOM curve, please pass the Animation Curve directly to your .SetCurve() function.");
                    else Debug.Assert(false, $"{(int)_curve} is not one of the available equations.");
                    return Linear;
            }
        }
    }
}