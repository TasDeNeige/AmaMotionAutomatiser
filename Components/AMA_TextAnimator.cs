//
// • Ama Motion Automatiser
// • [ Text Component ]
// • By Amaryne Bréand
//

using AMA;
using static AMA.AMATextMain;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;

public class AMA_TextAnimator : MonoBehaviour
{
    List<TextMA> tags = new List<TextMA>();

    TMP_Text textComponent;

    [Header("Animation")]
    [SerializeField] bool skipInvisibleCharacters = true;

    #region Monobehaviour
    private void Awake()
    {
        // Retrieve text component
        textComponent = GetComponent<TMP_Text>();

        // If no text was found
        if (textComponent == null)
        {
            Debug.LogWarning(AMAMain.debugAlertString + "No TMP_Text found in component " + transform.name + ". Component will be destroyed.");
            Destroy(this);
            return;
        }

        ProcessTags();
    }

    private void Update()
    {
        // Don't process text if no tag has been registered
        if (tags.Count <= 0) return;

        #region Animation
        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo;

        // Go through each tag
        for (int currentTag = 0; currentTag < tags.Count; currentTag++)
        {
            // Go through each character
            for (int currentChar = tags[currentTag].StartId;
                currentChar < (tags[currentTag].EndId < textInfo.characterCount ? tags[currentTag].EndId : textInfo.characterCount);
                ++currentChar)
            {
                TMP_CharacterInfo characterInfo = textInfo.characterInfo[currentChar];

                // Skip invisible characters
                if (skipInvisibleCharacters && !characterInfo.isVisible) { continue; }

                Vector3[] verts = textInfo.meshInfo[characterInfo.materialReferenceIndex].vertices;

                // Go through each verts
                for (int currentVert = 0; currentVert < 4; ++currentVert)
                {
                    // Current position of the vertex
                    Vector3 vertPos = verts[characterInfo.vertexIndex + currentVert];

                    // Animate
                    verts[characterInfo.vertexIndex + currentVert] = tags[currentTag].Animate(vertPos, currentChar);
                }
            }
        }

        // Update working copy
        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            // Retrieve draft meshes
            TMP_MeshInfo meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;

            // Apply modifications
            textComponent.UpdateGeometry(meshInfo.mesh, i);
            textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        }
        #endregion
    }
    #endregion

    #region Methods
    private void ProcessTags()
    {
        // Mesh update forced to get true Link count
        textComponent.ForceMeshUpdate();

        // Process each tag
        for (int i = 0; i < textComponent.textInfo.linkCount; i++)
        {
            TMP_LinkInfo link = textComponent.textInfo.linkInfo[i];

            // Split tag (to retrieve settings)
            string[] decomposedTag = link.GetLinkID().Split(',');

            // If link is one of our tag
            switch (decomposedTag[0]) // '0' corresponds to the tag type (e.g. 'wavy' or 'shake)
            {
                // Wavy
                case wavyTag:
                    float wavyWaveSpeed = float.MinValue;
                    float wavyWaveIntensity = float.MinValue;
                    float wavyDisplacementIntensity = float.MinValue;

                    // Retrieve info
                    for (int settingId = 1; settingId < decomposedTag.Length; settingId++)
                    {
                        string[] setting = decomposedTag[settingId].Split('=');

                        switch(setting[0])
                        {
                            case "s": wavyWaveSpeed = float.Parse(setting[1], CultureInfo.InvariantCulture); break;
                            case "w": wavyWaveIntensity = float.Parse(setting[1], CultureInfo.InvariantCulture); break;
                            case "d": wavyDisplacementIntensity = float.Parse(setting[1], CultureInfo.InvariantCulture); break;

                            default: Debug.LogWarning(AMAMain.debugAlertString + "Unrecognised \'" + setting[0] + "\' in tag 'wavy', " + transform.name
                                                                         + ".\nVerify there is no space before the setting type."); break;
                        }
                    }

                    TextMA_Wavy newWavyTag = new TextMA_Wavy(link.linkTextfirstCharacterIndex,
                                                                                            link.linkTextfirstCharacterIndex + link.linkTextLength,
                                                                                            wavyWaveSpeed, wavyWaveIntensity, wavyDisplacementIntensity);
                    tags.Add(newWavyTag);
                    break;

                // Fall Down
                case fallDownTag:
                    float fallDownSpeed = float.MinValue;

                    // Retrieve info
                    for (int settingId = 1; settingId < decomposedTag.Length; settingId++)
                    {
                        string[] setting = decomposedTag[settingId].Split('=');

                        switch (setting[0])
                        {
                            case "s": fallDownSpeed = float.Parse(setting[1], CultureInfo.InvariantCulture); break;

                            default:
                                Debug.LogWarning(AMAMain.debugAlertString + "Unrecognised \'" + setting[0] + "\' in tag 'fall_down', " + transform.name
                                                                         + ".\nVerify there is no space before the setting type."); break;
                        }
                    }

                    TextMA_FallDown newFallDownTag = new TextMA_FallDown(link.linkTextfirstCharacterIndex,
                                                                                            link.linkTextfirstCharacterIndex + link.linkTextLength,
                                                                                            fallDownSpeed);
                    tags.Add(newFallDownTag);
                    break;

                // Shake
                case shakeTag:
                    float shakeDisplacementIntensity = float.MinValue;
                    int shakeFrameDelay = int.MinValue;
                    int shakeNbDisplacements = int.MinValue;

                    // Retrieve info
                    for (int settingId = 1; settingId < decomposedTag.Length; settingId++)
                    {
                        string[] setting = decomposedTag[settingId].Split('=');

                        switch (setting[0])
                        {
                            case "i": shakeDisplacementIntensity = float.Parse(setting[1], CultureInfo.InvariantCulture); break;
                            case "d": shakeFrameDelay = int.Parse(setting[1]); break;
                            case "n": shakeNbDisplacements = int.Parse(setting[1]); break;

                            default:
                                Debug.LogWarning(AMAMain.debugAlertString + "Unrecognised \'" + setting[0] + "\' in tag 'shake', " + transform.name
                                                                         + ".\nVerify there is no space before the setting type."); break;
                        }
                    }

                    TextMA_Shake newShakeTag = new TextMA_Shake(link.linkTextfirstCharacterIndex,
                                                                                                        link.linkTextfirstCharacterIndex + link.linkTextLength,
                                                                                                        shakeDisplacementIntensity, shakeFrameDelay, shakeNbDisplacements);
                    tags.Add(newShakeTag);
                    break;

                default: /* Not one of our tags */ break;
            }
        }
    }
    #endregion
}