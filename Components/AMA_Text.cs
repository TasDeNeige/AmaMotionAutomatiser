//
// • Ama Motion Automatiser
// • [ Text Component ]
// • By Amaryne Bréand
//

using AMA;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

//[RequireComponent(typeof(TextMeshPro))]
public class AMA_Text : MonoBehaviour
{
    #region Tag system
    enum TagType { WAVY, FALLING_DOWN, SHAKE };
    const string wavyTag = "wavy";
    const string fallingDownTag = "falling_down";
    const string shakeTag = "shake";

    struct TagInfo { public TagType tagType; public int startId; public int endId; }
    List<TagInfo> tags = new List<TagInfo>();
    #endregion

    TMP_Text textComponent;

    [Header("Animation")]
    [SerializeField] bool skipInvisibleCharacters = true;
    [SerializeField] float waveSpeed = 2f;
    [SerializeField] float displacementIntensity = 0.01f;
    [SerializeField] float waveIntensity = 10.0f;

    [Header("Shake related")]
    [SerializeField] int nbDisplacements = 20;
    [SerializeField, Min(1)] int frameDelay = 4;
    bool hasShakeEffect = false;
    List<float> shakeDisplacements = new List<float>();

    #region Monobehaviour
    private void Awake()
    {
        // Retrieve text component
        textComponent = GetComponent<TMP_Text>();

        // If no text was found
        if (textComponent == null)
        {
            Debug.LogWarning(AMAMain.debugAlertString + "No TMP_Text found in component " + transform.name + ". Game object will be destroyed.");
            Destroy(gameObject);
            return;
        }

        ProcessTags();
    }

    private void Start()
    {
        // Generate displacements
        if (hasShakeEffect)
        {
            for (int i = 0; i < nbDisplacements; i++)
            {
                shakeDisplacements.Add(Random.Range(-100f, 100f));
            }
        }
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
            for (int currentChar = tags[currentTag].startId;
                currentChar < (tags[currentTag].endId < textInfo.characterCount ? tags[currentTag].endId : textInfo.characterCount);
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

                    // Animation code:
                    switch (tags[currentTag].tagType)
                    {
                        // Wavy text
                        case TagType.WAVY: verts[characterInfo.vertexIndex + currentVert] = WaveAnimation(vertPos); break;

                        // Falling down text
                        case TagType.FALLING_DOWN: verts[characterInfo.vertexIndex + currentVert] = FallDownAnimation(vertPos); break;

                        // Displace each verts by according list's displacement
                        case TagType.SHAKE: verts[characterInfo.vertexIndex + currentVert] = ShakeAnimation(vertPos, currentChar); break;
                    }
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

            TagInfo newTag = new TagInfo();
            newTag.startId = link.linkTextfirstCharacterIndex;
            newTag.endId = newTag.startId + link.linkTextLength;

            // If link is one of our tag
            switch (link.GetLinkID())
            {
                case wavyTag: newTag.tagType = TagType.WAVY; tags.Add(newTag); break;
                case fallingDownTag: newTag.tagType = TagType.FALLING_DOWN; tags.Add(newTag); break;
                case shakeTag: newTag.tagType = TagType.SHAKE; tags.Add(newTag); hasShakeEffect = true; break;
                default: /* Not one of our tags */ break;
            }
        }
    }

    private Vector3 WaveAnimation(Vector3 _vertPos)
    {
         return _vertPos + new Vector3(0, Mathf.Sin(-(Time.time * waveSpeed + -_vertPos.x * displacementIntensity)) * waveIntensity, 0);
    }

    private Vector3 FallDownAnimation(Vector3 _vertPos)
    {
        return _vertPos + new Vector3(0, Mathf.Atan(-(Time.time * 2f + -_vertPos.x * 0.01f)) * 10f, 0);
    }

    private Vector3 ShakeAnimation(Vector3 _vertPos, int _currentChar)
    {
        // Delay between shakes
        int frameId = Time.frameCount + (frameDelay - (Time.frameCount % frameDelay));

        // Set up displacement
        float displacementX = shakeDisplacements[(_currentChar + frameId) % nbDisplacements] * displacementIntensity;
        float displacementY = shakeDisplacements[(_currentChar + (frameId * 2)) % nbDisplacements] * displacementIntensity;

        // Displace each verts by according list's displacement
        return  _vertPos + new Vector3(displacementX, displacementY, 0);
    }
    #endregion
}