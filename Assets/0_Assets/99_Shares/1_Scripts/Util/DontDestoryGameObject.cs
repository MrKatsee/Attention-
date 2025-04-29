using UnityEngine;

namespace Util
{
    public class DontDestroyGameObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}