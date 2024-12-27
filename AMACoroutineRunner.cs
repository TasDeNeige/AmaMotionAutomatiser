//
// Ama Motion Automatizer
// [ Coroutine Runner ]
// • By Amaryne Bréand
// • Last updated: 21/12/2024
//

using UnityEngine;

namespace AMA
{
    public class AMACoroutineRunner : MonoBehaviour
    {
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
    }
}
