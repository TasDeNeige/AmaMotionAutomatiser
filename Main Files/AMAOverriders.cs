//
// • Ama Motion Automatizer
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

        public override bool GetAvailability() => !(transform == null);
        public override Vector3 GetModifiedValue() => transform.position;
        public override void SetModifiedValue(Vector3 _newValue) => transform.position = _newValue;
    }

    public class MAMoveLocalTransform : MA<Vector3>
    {
        public Transform transform;

        public override bool GetAvailability() => !(transform == null);
        public override Vector3 GetModifiedValue() => transform.localPosition;
        public override void SetModifiedValue(Vector3 _newValue) => transform.localPosition = _newValue;
    }
    #endregion

    #region Rect Transform
    public class MAMoveRectTransform : MA<Vector3>
    {
        public RectTransform rectTransform;

        public override bool GetAvailability() => !(rectTransform == null);
        public override Vector3 GetModifiedValue() => rectTransform.position;
        public override void SetModifiedValue(Vector3 _newValue) => rectTransform.position = _newValue;
    }

    public class MAMoveLocalRectTransform : MA<Vector3>
    {
        public RectTransform rectTransform;

        public override bool GetAvailability() => !(rectTransform == null);
        public override Vector3 GetModifiedValue() => rectTransform.localPosition;
        public override void SetModifiedValue(Vector3 _newValue) => rectTransform.localPosition = _newValue;
    }

    public class MAMoveAnchoredPositionRectTransform : MA<Vector3>
    {
        public RectTransform rectTransform;

        public override bool GetAvailability() => !(rectTransform == null);
        public override Vector3 GetModifiedValue() => rectTransform.anchoredPosition;
        public override void SetModifiedValue(Vector3 _newValue) => rectTransform.anchoredPosition = _newValue;
    }

    public class MAMoveAnchoredPosition3DRectTransform : MA<Vector3>
    {
        public RectTransform rectTransform;

        public override bool GetAvailability() => !(rectTransform == null);
        public override Vector3 GetModifiedValue() => rectTransform.anchoredPosition3D;
        public override void SetModifiedValue(Vector3 _newValue) => rectTransform.anchoredPosition3D = _newValue;
    }
    #endregion
    #endregion

    #region Scale
    #region Transform
    public class MAScaleTransform : MA<Vector3>
    {
        public Transform transform;

        public override bool GetAvailability() => !(transform == null);
        public override Vector3 GetModifiedValue() => transform.localScale;
        public override void SetModifiedValue(Vector3 _newValue) => transform.localScale = _newValue;
    }
    #endregion

    #region RectTransform
    public class MAScaleRectTransform : MA<Vector3>
    {
        public RectTransform rectTransform;

        public override bool GetAvailability() => !(rectTransform == null);
        public override Vector3 GetModifiedValue() => rectTransform.localScale;
        public override void SetModifiedValue(Vector3 _newValue) => rectTransform.localScale = _newValue;
    }
    #endregion
    #endregion

    #region Fade
    #region Material
    public class MAFadeMaterial : MA<Color>
    {
        public Material material;

        public override bool GetAvailability() => !(material == null);
        public override Color GetModifiedValue() => material.color;
        public override void SetModifiedValue(Color _newValue) => material.color = _newValue;
    }
    #endregion

    #region Image
    public class MAFadeImage : MA<Color>
    {
        public UnityEngine.UI.Image image;

        public override bool GetAvailability() => !(image == null);
        public override Color GetModifiedValue() => image.color;
        public override void SetModifiedValue(Color _newValue) => image.color = _newValue;
    }
    #endregion

    #region Image
    public class MAFadeTmpText : MA<Color>
    {
        public TMP_Text text;

        public override bool GetAvailability() => !(text == null);
        public override Color GetModifiedValue() => text.color;
        public override void SetModifiedValue(Color _newValue) => text.color = _newValue;
    }
    #endregion

    #region Color
    public class MAFadeColor : MA<Color>
    {
        public Color color;

        public override bool GetAvailability() => !(color == null);
        public override Color GetModifiedValue() => color;
        public override void SetModifiedValue(Color _newValue) => color = _newValue;
    }
    #endregion
    #endregion

    #region Rotate
    #region Quaternion
    public class MARotateQuaternionTransform : MA<Quaternion>
    {
        public Transform transform;

        public override bool GetAvailability() => !(transform == null);
        public override Quaternion GetModifiedValue() => transform.rotation;
        public override void SetModifiedValue(Quaternion _newValue) => transform.rotation = _newValue;
    }
    #endregion

    #region Euler Angles
    public class MARotateEulerTransform : MA<Vector3>
    {
        public Transform transform;

        public override bool GetAvailability() => !(transform == null);
        public override Vector3 GetModifiedValue() => transform.rotation.eulerAngles;
        public override void SetModifiedValue(Vector3 _newValue) => transform.rotation = Quaternion.Euler(_newValue);
    }
    #endregion
    #endregion
}