//
// • Ama Motion Automatiser
// • [ Animation Fade Component ]
// • By Amaryne Bréand
//

using AMA;
using UnityEditor;
using UnityEngine;

public class AMAAnimation_Fade : AMABasicComponent
{
    public enum ColorComponent { Material, UIImage, TMP_Text };

    #region In inspector
    [Header("Main settings")]
    [HideInInspector] public Color endValue = Color.white;
    [HideInInspector, Tooltip("In seconds")] public float animationDuration = 1f;
    [HideInInspector] public ColorComponent colorComponent = ColorComponent.Material;
    [HideInInspector] public bool addCustomMaterial = false;
    [HideInInspector] public Material customMaterial;
    [HideInInspector] public bool addCustomUIImage = false;
    [HideInInspector] public UnityEngine.UI.Image customUIImage;
    [HideInInspector] public bool addCustomTMP_Text = false;
    [HideInInspector] public TMPro.TMP_Text customTMP_Text;
    [HideInInspector] public bool playAnimationOnStart = false;

    [Header("Miscellaneous")]
    [HideInInspector] public bool addFromValue;
    [HideInInspector] public Color fromValue = Color.black;
    #endregion

    void Start()
    {
        if (playAnimationOnStart) PlayAnimation();
    }

    public void PlayAnimation()
    {
        // Prevent bugs with no custom ColorComponent was given
        switch(colorComponent)
        {
            case ColorComponent.Material:
                if (addCustomMaterial && customMaterial == null)
                {
                    Debug.LogWarning(AMAMain.debugAlertString + "Custom Material was activated, but no material was given in " + transform.name + ". Please add one in the component or thanks to a call to the 'AssignCustomMaterial' function before the 'PlayAnimation'.");
                    return;
                }
                break;

            case ColorComponent.UIImage:
                if (addCustomUIImage && customUIImage == null)
                {
                    Debug.LogWarning(AMAMain.debugAlertString + "Custom UI Image was activated, but no UI image was given in " + transform.name + ". Please add one in the component or thanks to a call to the 'AssignCustomUIImage' function before the 'PlayAnimation'.");
                    return;
                }
                break;

            case ColorComponent.TMP_Text:
                if (addCustomTMP_Text && customTMP_Text == null)
                {
                    Debug.LogWarning(AMAMain.debugAlertString + "Custom TMP_Text was activated, but no TMP_Text was given in " + transform.name + ". Please add one in the component or thanks to a call to the 'AssignCustomTMP_Text' function before the 'PlayAnimation'.");
                    return;
                }
                break;
            
            default: break;
        }

        // Create animation
        AMAMain.MA<Color> newMA;

        // Depending on space
        switch(colorComponent)
        {
            // Material
            case ColorComponent.Material:
                Material materialToAnimate;

                // Custom Mat
                if (addCustomMaterial) materialToAnimate = customMaterial;
                // Basic Mat
                else
                {
                    // Get Mat
                    materialToAnimate = gameObject.GetComponent<Renderer>().material;
                    
                    // Throw error if no material found
                    if (materialToAnimate == null)
                    {
                        Debug.LogError(AMA.AMAMain.debugAlertString + "No Material found for animation in " + gameObject.name);
                        return;
                    }
                }
                
                newMA = materialToAnimate.AMAfade(endValue, animationDuration, snapToEndValue);
                break;

            // UI Image
            case ColorComponent.UIImage:
                UnityEngine.UI.Image imageToAnimate;

                // Custom Image
                if (addCustomUIImage) imageToAnimate = customUIImage;
                // Basic Image
                else
                {
                    // Get Image
                    imageToAnimate = transform.GetComponent<UnityEngine.UI.Image>();

                    // Throw error if no image found
                    if (imageToAnimate == null)
                    {
                        Debug.LogError(AMA.AMAMain.debugAlertString + "No UI.Image found for animation in " + gameObject.name);
                        return;
                    }
                }

                newMA = imageToAnimate.AMAfade(endValue, animationDuration, snapToEndValue);
                break;

            // TMP Text
            case ColorComponent.TMP_Text:
                TMPro.TMP_Text textToAnimate;

                // Custom Text
                if (addCustomTMP_Text) textToAnimate = customTMP_Text;
                // Basic Text
                else
                {
                    // Get Text
                    textToAnimate = transform.GetComponent<TMPro.TMP_Text>();

                    // Throw error if no text found
                    if (textToAnimate == null)
                    {
                        Debug.LogError(AMA.AMAMain.debugAlertString + "No TMPro.TMP_Text found for animation in " + gameObject.name);
                        return;
                    }
                }

                newMA = textToAnimate.AMAfade(endValue, animationDuration, snapToEndValue);
                break;

            default:
                Debug.LogError(AMA.AMAMain.debugAlertString + "No Component's Color setted for animation in " + gameObject.name);
                return;
        }
        
        AddMisc(ref newMA);
        // From Value
        if (addFromValue) newMA.From(fromValue);
    }

    public Color GetCurrentColor()
    {
        // Depending on Color Component
        switch (colorComponent)
        {
            // Material
            case ColorComponent.Material: return gameObject.GetComponent<Renderer>().material.color;

            // UI Image
            case ColorComponent.UIImage: return transform.GetComponent<UnityEngine.UI.Image>().color;

            // TMP Text
            case ColorComponent.TMP_Text: return transform.GetComponent<TMPro.TMP_Text>().color;

            default:
                Debug.LogWarning(AMA.AMAMain.debugAlertString + "No Component's Color setted for pick in " + gameObject.name);
                return Color.white;
        }
    }

    public void AssignCustomMaterial(Material _customMaterial) { customMaterial = _customMaterial; }
    public void AssignCustomUIImage(UnityEngine.UI.Image _uiImage) { customUIImage = _uiImage; }
    public void AssignCustomTMP_Text(TMPro.TMP_Text _tmpText) { customTMP_Text = _tmpText; }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AMAAnimation_Fade))]
class AMAAnimationFadeEditor : AMAComponentEditor<Color>
{
    SerializedProperty colorComponentProp;
    SerializedProperty customMaterialProp;
    SerializedProperty customUIImageProp;
    SerializedProperty customTMP_TextProp;

    Texture banner;
    string bannerPath = "AMA_AnimationComponentBanner";

    #region Editor
    void OnEnable()
    {
        // Link serialized properties to their names in the target class
        colorComponentProp = serializedObject.FindProperty("colorComponent");
        customMaterialProp = serializedObject.FindProperty("customMaterial");
        customUIImageProp = serializedObject.FindProperty("customUIImage");
        customTMP_TextProp = serializedObject.FindProperty("customTMP_Text");

        SetUpOnEnable(serializedObject);

        // Load banner
        banner = (Texture)Resources.Load(bannerPath, typeof(Texture));
    }

    public override void OnInspectorGUI()
    {
        AMAAnimation_Fade script = (AMAAnimation_Fade)target;

        SetUpOnInspector(serializedObject, banner);

        #region Components drawing
        #region Main Settings

        // Start value
        script.addFromValue = EditorGUILayout.Toggle("Change Start value", script.addFromValue);
        if (script.addFromValue) script.fromValue = EditorGUILayout.ColorField("Starting value:", script.fromValue);

        // End value
        script.endValue = EditorGUILayout.ColorField("End value:", script.endValue);
        
        // Anim duration
        script.animationDuration = EditorGUILayout.FloatField("Anim. Duration", script.animationDuration);
       
        // Color Component
        EditorGUILayout.PropertyField(colorComponentProp, new GUIContent("Color Component"), true);
        
        // Play anim on start
        script.playAnimationOnStart = EditorGUILayout.Toggle("Play anim. on Start()", script.playAnimationOnStart);
        
        // Custom transform
        switch (script.colorComponent)
        {
            case AMAAnimation_Fade.ColorComponent.Material:
                script.addCustomMaterial = EditorGUILayout.Toggle("Use another Material", script.addCustomMaterial);
                if (script.addCustomMaterial) EditorGUILayout.PropertyField(customMaterialProp, new GUIContent("Custom Material"), true);
                break;

            case AMAAnimation_Fade.ColorComponent.UIImage:
                script.addCustomUIImage = EditorGUILayout.Toggle("Use another UI Image", script.addCustomUIImage);
                if (script.addCustomUIImage) EditorGUILayout.PropertyField(customUIImageProp, new GUIContent("Custom UI Image"), true);
                break;

            case AMAAnimation_Fade.ColorComponent.TMP_Text:
                script.addCustomTMP_Text = EditorGUILayout.Toggle("Use another TMP_Text", script.addCustomTMP_Text);
                if (script.addCustomTMP_Text) EditorGUILayout.PropertyField(customTMP_TextProp, new GUIContent("Custom TMP_Text"), true);
                break;

            default: break;
        }
        #endregion

        DrawMisc(serializedObject, script);

        // Apply serialized changes
        serializedObject.ApplyModifiedProperties();
        #endregion
    }
    #endregion
}
#endif