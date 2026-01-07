//
// • Ama Motion Automatiser
// • [ Coroutine Runner ]
// • By Amaryne Bréand
//

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AMA
{
    public class AMACoroutineRunner : MonoBehaviour
    {
        List<IEnumerator> coroutinesToStart = new List<IEnumerator>();
        Dictionary<object, List<IEnumerator>> ongoingCoroutines = new Dictionary<object, List<IEnumerator>>();

        static AMACoroutineRunner instance;

        public static AMACoroutineRunner Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject("AMA Coroutine Runner");
                    instance = obj.AddComponent<AMACoroutineRunner>();
                    DontDestroyOnLoad(obj);
                }
                return instance;
            }
        }

        #region Monobehaviour
        void LateUpdate()
        {
            // If there are coroutines to start
            if (coroutinesToStart.Count > 0)
            {
                // Start every coroutine needed
                for (int i = 0; i < coroutinesToStart.Count; i++)
                {
                    StartCoroutine(coroutinesToStart[i]);
                }

                // Get rid of them (to avoid starting them a second time)
                coroutinesToStart.Clear();
            }
        }
        #endregion

        #region Methods
        // Start coroutine in late update
        public void INTERNAL_StartCoroutine(object _object, IEnumerator _coroutine)
        {
            // Add Coroutine to ongoingMAs
            if (!ongoingCoroutines.ContainsKey(_object))
            {
                // Create list if needed
                ongoingCoroutines.Add(_object, new List<IEnumerator>());
            }

            // Add MA to list
            ongoingCoroutines[_object].Add(_coroutine);

            coroutinesToStart.Add(_coroutine);
        }

        // Stop Coroutine (from user input)
        public void INTERNAL_StopCoroutine(object _object)
        {
            // Get out if object doesn't have coroutines
            if (!ongoingCoroutines.ContainsKey(_object)) return;

            // Stop all MAs
            for (int i = 0; i < ongoingCoroutines[_object].Count; i++)
            {
                StopCoroutine(ongoingCoroutines[_object][i]);
            }

            ongoingCoroutines.Remove(_object);
        }

        // Stop All Coroutines (from user input)
        public void INTERNAL_StopAll()
        {
            // Stop all
            foreach (object key in ongoingCoroutines.Keys)
            {
                for (int i = 0; i < ongoingCoroutines[key].Count; i++)
                {
                    StopCoroutine(ongoingCoroutines[key][i]);
                }
            }

            ongoingCoroutines.Clear();
        }


        // Delete Coroutine from dict (e.g. used when MA is destroyed)
        public void INTERNAL_DeleteCoroutine(object _object, IEnumerator _coroutine)
        {
            // Get out if object doesn't have coroutines
            if (!ongoingCoroutines.ContainsKey(_object)) return;

            // Delete Coroutine
            ongoingCoroutines[_object].Remove(_coroutine);
        }
        #endregion
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(AMACoroutineRunner))]
    class AMACoroutineRunnerEditor : AMAComponentEditor<bool>
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Hey :D");
            GUILayout.Label("Please do not manually add this script to a component.");
        }
    }
#endif
}
