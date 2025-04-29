using UnityEngine;
using System.Collections;

namespace Util
{
    public class CoroutineHelper : MonoBehaviour
    {
        private static CoroutineHelper _instance;
        public static CoroutineHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = new GameObject("CoroutineHelper");
                    _instance = obj.AddComponent<CoroutineHelper>();
                }

                return _instance;
            }
        }

        public Coroutine StartRoutine(IEnumerator routine)
        {
            return Instance.StartCoroutine(routine);
        }

        public void StopRoutine(Coroutine coroutine)
        {
            Instance.StopCoroutine(coroutine);
        }
    }
}