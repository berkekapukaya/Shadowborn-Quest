using UnityEngine;

namespace SceneManagement
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = (T)this;
            }

            if (gameObject.transform.parent) return;
            
            DontDestroyOnLoad(gameObject);
        }
    }
}
