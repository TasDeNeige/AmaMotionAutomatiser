//
// • Ama Motion Automatizer
// • [ Coroutine Runner ]
// • By Amaryne Bréand
// • Last updated: 28/01/2025
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AMA
{
    public class AMACoroutineRunner : MonoBehaviour
    {
        private List<IEnumerator> coroutinesToStart = new List<IEnumerator>();

        private static AMACoroutineRunner instance;

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

        // Start coroutine in late update
        public void INTERNAL_StartCoroutine(IEnumerator _coroutine)
        {
            coroutinesToStart.Add(_coroutine);
        }

        private void LateUpdate()
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
    }
}
