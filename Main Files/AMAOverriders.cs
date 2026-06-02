//
// • Ama Motion Automatiser
// • [ Overriders ]
// • By Amaryne Bréand
//

using TMPro;
using UnityEngine;
using static AMA.AMAMain;

namespace AMA
{
    #region Move
    #region Transform
    public class MAMoveTransform : MA<Vector3>
    {
        public Transform transform;

        public override bool GetAvailability() => TestObjAvailability(transform);
        public override Vector3 GetModifiedValue() => transform.position;
        public override void SetModifiedValue(Vector3 _newValue) => transform.position = _newValue;

        // Vector3
        public void SetUp(Transform _transform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.position;
            endValue = ApplyAxisMask(_selectedAxis, _endPos, transform.position);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }

        // Float
        public void SetUp(Transform _transform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.position;
            endValue = ApplyAxisMask(_selectedAxis, new Vector3(_endPos, _endPos, _endPos), transform.position);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }
    }

    public class MAMoveLocalTransform : MA<Vector3>
    {
        public Transform transform;

        public override bool GetAvailability() => TestObjAvailability(transform);
        public override Vector3 GetModifiedValue() => transform.localPosition;
        public override void SetModifiedValue(Vector3 _newValue) => transform.localPosition = _newValue;

        // Vector3
        public void SetUp(Transform _transform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.localPosition;
            endValue = ApplyAxisMask(_selectedAxis, _endPos, transform.localPosition);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }

        // Float
        public void SetUp(Transform _transform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.localPosition;
            endValue = ApplyAxisMask(_selectedAxis, new Vector3(_endPos, _endPos, _endPos), transform.localPosition);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }
    }
    #endregion

    #region Rect Transform
    public class MAMoveRectTransform : MA<Vector3>
    {
        public RectTransform rectTransform;

        public override bool GetAvailability() => TestObjAvailability(rectTransform);
        public override Vector3 GetModifiedValue() => rectTransform.position;
        public override void SetModifiedValue(Vector3 _newValue) => rectTransform.position = _newValue;

        // Vector3
        public void SetUp(RectTransform _rectTransform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _rectTransform;
            rectTransform = _rectTransform;
            selectedAxis = _selectedAxis;
            startValue = _rectTransform.position;
            endValue = ApplyAxisMask(_selectedAxis, _endPos, _rectTransform.position);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }

        // Float
        public void SetUp(RectTransform _rectTransform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _rectTransform;
            rectTransform = _rectTransform;
            selectedAxis = _selectedAxis;
            startValue = _rectTransform.position;
            endValue = ApplyAxisMask(_selectedAxis, new Vector3(_endPos, _endPos, _endPos), _rectTransform.position);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }
    }

    public class MAMoveLocalRectTransform : MA<Vector3>
    {
        public RectTransform rectTransform;

        public override bool GetAvailability() => TestObjAvailability(rectTransform);
        public override Vector3 GetModifiedValue() => rectTransform.localPosition;
        public override void SetModifiedValue(Vector3 _newValue) => rectTransform.localPosition = _newValue;

        // Vector3
        public void SetUp(RectTransform _rectTransform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _rectTransform;
            rectTransform = _rectTransform;
            selectedAxis = _selectedAxis;
            startValue = _rectTransform.localPosition;
            endValue = ApplyAxisMask(_selectedAxis, _endPos, _rectTransform.localPosition);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }

        // Float
        public void SetUp(RectTransform _rectTransform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _rectTransform;
            rectTransform = _rectTransform;
            selectedAxis = _selectedAxis;
            startValue = _rectTransform.localPosition;
            endValue = ApplyAxisMask(_selectedAxis, new Vector3(_endPos, _endPos, _endPos), _rectTransform.localPosition);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }
    }

    public class MAMoveAnchoredPositionRectTransform : MA<Vector3>
    {
        public RectTransform rectTransform;

        public override bool GetAvailability() => TestObjAvailability(rectTransform);
        public override Vector3 GetModifiedValue() => rectTransform.anchoredPosition;
        public override void SetModifiedValue(Vector3 _newValue) => rectTransform.anchoredPosition = _newValue;

        // Vector3
        public void SetUp(RectTransform _rectTransform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _rectTransform;
            rectTransform = _rectTransform;
            selectedAxis = _selectedAxis;
            startValue = _rectTransform.anchoredPosition;
            endValue = ApplyAxisMask(_selectedAxis, _endPos, _rectTransform.anchoredPosition);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }

        // Float
        public void SetUp(RectTransform _rectTransform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _rectTransform;
            rectTransform = _rectTransform;
            selectedAxis = _selectedAxis;
            startValue = _rectTransform.anchoredPosition;
            endValue = ApplyAxisMask(_selectedAxis, new Vector3(_endPos, _endPos, _endPos), _rectTransform.anchoredPosition);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }
    }

    public class MAMoveAnchoredPosition3DRectTransform : MA<Vector3>
    {
        public RectTransform rectTransform;

        public override bool GetAvailability() => TestObjAvailability(rectTransform);
        public override Vector3 GetModifiedValue() => rectTransform.anchoredPosition3D;
        public override void SetModifiedValue(Vector3 _newValue) => rectTransform.anchoredPosition3D = _newValue;

        // Vector3
        public void SetUp(RectTransform _rectTransform, Axis _selectedAxis, Vector3 _endPos, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _rectTransform;
            rectTransform = _rectTransform;
            selectedAxis = _selectedAxis;
            startValue = _rectTransform.anchoredPosition3D;
            endValue = ApplyAxisMask(_selectedAxis, _endPos, _rectTransform.anchoredPosition3D);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }

        // Float
        public void SetUp(RectTransform _rectTransform, Axis _selectedAxis, float _endPos, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _rectTransform;
            rectTransform = _rectTransform;
            selectedAxis = _selectedAxis;
            startValue = _rectTransform.anchoredPosition3D;
            endValue = ApplyAxisMask(_selectedAxis, new Vector3(_endPos, _endPos, _endPos), _rectTransform.anchoredPosition3D);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }
    }
    #endregion
    #endregion

    #region Scale
    #region Transform
    public class MAScaleTransform : MA<Vector3>
    {
        public Transform transform;

        public override bool GetAvailability() => TestObjAvailability(transform);
        public override Vector3 GetModifiedValue() => transform.localScale;
        public override void SetModifiedValue(Vector3 _newValue) => transform.localScale = _newValue;

        // Vector3
        public void SetUp(Transform _transform, Axis _selectedAxis, Vector3 _endScale, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.localScale;
            endValue = ApplyAxisMask(_selectedAxis, _endScale, transform.localScale);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }

        // Float
        public void SetUp(Transform _transform, Axis _selectedAxis, float _endScale, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.localScale;
            endValue = ApplyAxisMask(_selectedAxis, new Vector3(_endScale, _endScale, _endScale), transform.localScale);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }
    }
    #endregion

    #region RectTransform
    public class MAScaleRectTransform : MA<Vector3>
    {
        public RectTransform rectTransform;

        public override bool GetAvailability() => TestObjAvailability(rectTransform);
        public override Vector3 GetModifiedValue() => rectTransform.localScale;
        public override void SetModifiedValue(Vector3 _newValue) => rectTransform.localScale = _newValue;

        // Vector3
        public void SetUp(RectTransform _transform, Axis _selectedAxis, Vector3 _endScale, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            rectTransform = _transform;
            selectedAxis = _selectedAxis;
            startValue = rectTransform.localScale;
            endValue = ApplyAxisMask(_selectedAxis, _endScale, rectTransform.localScale);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }

        // Float
        public void SetUp(RectTransform _transform, Axis _selectedAxis, float _endScale, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            rectTransform = _transform;
            selectedAxis = _selectedAxis;
            startValue = rectTransform.localScale;
            endValue = ApplyAxisMask(_selectedAxis, new Vector3(_endScale, _endScale, _endScale), rectTransform.localScale);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }
    }
    #endregion
    #endregion

    #region Fade
    #region Material
    public class MAFadeMaterial : MA<Color>
    {
        public Material material;

        public override bool GetAvailability() => TestObjAvailability(material);
        public override Color GetModifiedValue() => material.color;
        public override void SetModifiedValue(Color _newValue) => material.color = _newValue;
    }
    #endregion

    #region Image
    public class MAFadeImage : MA<Color>
    {
        public UnityEngine.UI.Image image;

        public override bool GetAvailability() => TestObjAvailability(image);
        public override Color GetModifiedValue() => image.color;
        public override void SetModifiedValue(Color _newValue) => image.color = _newValue;
    }
    #endregion

    #region TmpText
    public class MAFadeTmpText : MA<Color>
    {
        public TMP_Text text;

        public override bool GetAvailability() => TestObjAvailability(text);
        public override Color GetModifiedValue() => text.color;
        public override void SetModifiedValue(Color _newValue) => text.color = _newValue;
    }
    #endregion

    #region Color
    public class MAFadeColor : MA<Color>
    {
        public Color color;

        public override bool GetAvailability() => TestObjAvailability(color);
        public override Color GetModifiedValue() => color;
        public override void SetModifiedValue(Color _newValue) => color = _newValue;
    }
    #endregion
    #endregion

    #region Rotate
    #region World
    #region Quaternion
    public class MARotateQuaternionTransform : MA<Quaternion>
    {
        public Transform transform;

        public override bool GetAvailability() => TestObjAvailability(transform);
        public override Quaternion GetModifiedValue() => transform.rotation;
        public override void SetModifiedValue(Quaternion _newValue) => transform.rotation = _newValue;

        // Quaternion
        public void SetUp(Transform _transform, Axis _selectedAxis, Quaternion _endRotation, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.rotation;
            endValue = ApplyAxisMask(_selectedAxis, _endRotation, transform.rotation);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }

        // Float
        public void SetUp(Transform _transform, Axis _selectedAxis, float _endRotation, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.rotation;
            endValue = ApplyAxisMask(_selectedAxis, new Quaternion(_endRotation, _endRotation, _endRotation, _endRotation), transform.rotation);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }
    }
    #endregion

    #region Euler Angles
    public class MARotateEulerTransform : MA<Vector3>
    {
        public Transform transform;

        public override bool GetAvailability() => TestObjAvailability(transform);
        public override Vector3 GetModifiedValue() => transform.eulerAngles;
        public override void SetModifiedValue(Vector3 _newValue) => transform.eulerAngles = _newValue;

        // Vector3
        public void SetUp(Transform _transform, Axis _selectedAxis, Vector3 _endRotation, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.rotation.eulerAngles;
            endValue = ApplyAxisMask(_selectedAxis, _endRotation, transform.rotation.eulerAngles);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }

        // Float
        public void SetUp(Transform _transform, Axis _selectedAxis, float _endRotation, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.rotation.eulerAngles;
            endValue = ApplyAxisMask(_selectedAxis, new Vector3(_endRotation, _endRotation, _endRotation), transform.rotation.eulerAngles);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }
    }
    #endregion

    #region Local
    #region Quaternion
    public class MALocalRotateQuaternionTransform : MA<Quaternion>
    {
        public Transform transform;

        public override bool GetAvailability() => TestObjAvailability(transform);
        public override Quaternion GetModifiedValue() => transform.localRotation;
        public override void SetModifiedValue(Quaternion _newValue) => transform.localRotation = _newValue;

        // Quaternion
        public void SetUp(Transform _transform, Axis _selectedAxis, Quaternion _endRotation, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.localRotation;
            endValue = ApplyAxisMask(_selectedAxis, _endRotation, transform.localRotation);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }

        // Float
        public void SetUp(Transform _transform, Axis _selectedAxis, float _endRotation, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.localRotation;
            endValue = ApplyAxisMask(_selectedAxis, new Quaternion(_endRotation, _endRotation, _endRotation, _endRotation), transform.localRotation);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }
    }
    #endregion

    #region Euler Angles
    public class MALocalRotateEulerTransform : MA<Vector3>
    {
        public Transform transform;

        public override bool GetAvailability() => TestObjAvailability(transform);
        public override Vector3 GetModifiedValue() => transform.localEulerAngles;
        public override void SetModifiedValue(Vector3 _newValue) => transform.localEulerAngles = _newValue;

        // Vector3
        public void SetUp(Transform _transform, Axis _selectedAxis, Vector3 _endRotation, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.localRotation.eulerAngles;
            endValue = ApplyAxisMask(_selectedAxis, _endRotation, transform.localRotation.eulerAngles);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }

        // Float
        public void SetUp(Transform _transform, Axis _selectedAxis, float _endRotation, float _duration, bool _snapToEndValue = true)
        {
            unityObject = _transform;
            transform = _transform;
            selectedAxis = _selectedAxis;
            startValue = transform.localRotation.eulerAngles;
            endValue = ApplyAxisMask(_selectedAxis, new Vector3(_endRotation, _endRotation, _endRotation), transform.localRotation.eulerAngles);
            duration = _duration;
            snapToEndValue = _snapToEndValue;
        }
    }
    #endregion
    #endregion
    #endregion
    #endregion

    #region Shake
    public class MAShake : MA<Vector3>
    {
        public Transform transform;
        float shakeRadius;
        float delayBetweenShakes = 0f;

        public float ShakeRadius { get => shakeRadius; set => shakeRadius = value; }
        public float DelayBetweenShakes { get => delayBetweenShakes; set => delayBetweenShakes = value; }

        public override bool GetAvailability() => TestObjAvailability(transform);
        public override Vector3 GetModifiedValue() => transform.position;
        public override void SetModifiedValue(Vector3 _newValue) => transform.position = _newValue;
    }
    #endregion
}