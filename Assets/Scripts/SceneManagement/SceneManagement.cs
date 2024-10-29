using UnityEngine;

namespace SceneManagement
{
    public class SceneManagementS : Singleton<SceneManagementS>
    {
        public string SceneTransitionName { get; private set; }
        
        public void SetTransitionName(string transitionName)
        {
            this.SceneTransitionName = transitionName;
        }
    }
}
