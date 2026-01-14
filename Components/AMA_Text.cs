//
// • Ama Motion Automatiser
// • [ Text Component ]
// • By Amaryne Bréand
//

using AMA;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

//[RequireComponent(typeof(TextMeshPro))]
public class AMA_Text : MonoBehaviour
{
    // Tags
    static string wavyOpenTag = "<wavy>";
    static string wavyCloseTag = "</wavy>";
    static string generalCloseTag = "</>";


    TMP_Text textComponent;
    int startAtId = 0;
    int endAtId = int.MaxValue;

    [Header("Animation")]
    [SerializeField] bool skipInvisibleCharacters = true;
    [SerializeField] float waveSpeed = 2f;
    [SerializeField] float displacementIntensity = 0.01f;
    [SerializeField] float waveIntensity  = 10.0f;

    private void Awake()
    {
        //TMPro_EventManager.TEXT_CHANGED_EVENT.Add(TextChangedTier);

        // Retrieve text component
        textComponent = GetComponent<TMP_Text>();

        // If no text was found
        if (textComponent == null)
        {
            Debug.LogWarning(AMAMain.debugAlertString + "No TMP_Text found in component " + transform.name + ". Game object will be destroyed.");
            Destroy(gameObject);
            return;
        }

        HandleTags();
    }

    // Used to tie methods to TMP's TEXT_CHANGED event
    private void TextChangedTier(Object obj)
    {
        HandleTags();
    }

    private void HandleTags()
    {
        // Wavy Open
        if (textComponent.text.Contains(wavyOpenTag))
        {
            // Get start index
            startAtId = textComponent.text.IndexOf(wavyOpenTag);
            Debug.Log("Effect starts at " + startAtId);

            // Erase tag from string
            HideTag(wavyOpenTag, startAtId);
        }

        // Wavy Close
        if (textComponent.text.Contains(wavyCloseTag))
        {
            // Get start index
            endAtId = textComponent.text.IndexOf(wavyCloseTag);
            Debug.Log("Effect ends at " + endAtId);

            // Erase tag from string
            HideTag(wavyCloseTag, endAtId);
        }

        // General Close
        if (textComponent.text.Contains(generalCloseTag))
        {
            // Get start index
            endAtId = textComponent.text.IndexOf(generalCloseTag);
            Debug.Log("Effect ends at " + endAtId);

            // Erase tag from string
            HideTag(generalCloseTag, endAtId);
        }
    }

    private void HideTag(string _tag, int _startId)
    {
        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo;

        //textComponent.text = textComponent.text.Substring(0, _startId) + textComponent.text.Substring(_startId + _tag.Length);


        //for (int i = 0; i < _tag.Length; i++)
        //{
        //    int charIndex = _startId + i;
        //    if (charIndex < 0 || charIndex >= textInfo.characterCount)
        //        continue;

        //    TMP_CharacterInfo charInfo = textInfo.characterInfo[charIndex];

        //    if (!charInfo.isVisible)
        //        continue;

        //    int matIndex = charInfo.materialReferenceIndex;
        //    int vertIndex = charInfo.vertexIndex;

        //    Vector3[] verts = textInfo.meshInfo[matIndex].vertices;
        //    Vector3 collapsed = verts[vertIndex];

        //    verts[vertIndex + 0] = collapsed;
        //    verts[vertIndex + 1] = collapsed;
        //    verts[vertIndex + 2] = collapsed;
        //    verts[vertIndex + 3] = collapsed;

        //    textInfo.meshInfo[matIndex].mesh.vertices = verts;
        //    textComponent.UpdateGeometry(textInfo.meshInfo[matIndex].mesh, matIndex);
        //}
    }


    private void Update()
    {
        #region Animation
        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo;

        // Go through each character
        for (int currentChar = startAtId;
            currentChar < (endAtId < textInfo.characterCount ? endAtId : textInfo.characterCount);
            ++currentChar)
        {
            TMP_CharacterInfo characterInfo = textInfo.characterInfo[currentChar];


            // Skip invisible characters
            if (skipInvisibleCharacters && !characterInfo.isVisible) { continue; }

            Vector3[] verts = textInfo.meshInfo[characterInfo.materialReferenceIndex].vertices;
            // Go through each verts
            for (int currentVert = 0; currentVert < /*verts.Length*/ 4; ++currentVert)
            {
                // Current position of the vertex
                Vector3 vertPos = verts[characterInfo.vertexIndex + currentVert];

                // Animation code:
                // Falling down text
                //verts[characterInfo.vertexIndex + currentVert] = vertPos + new Vector3(0, Mathf.Atan(-(Time.time * 2f + -vertPos.x * 0.01f)) * 10f, 0);

                // Wavy text
                //verts[characterInfo.vertexIndex + currentVert] = vertPos + new Vector3(0, Mathf.Sin(-(Time.time * waveSpeed + -vertPos.x * displacementIntensity)) * waveIntensity, 0);

                verts[characterInfo.vertexIndex + currentVert] = Vector3.zero;

                // Push changes to the mesh
                textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
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
}