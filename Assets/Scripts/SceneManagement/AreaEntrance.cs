using Misc;
using Player;
using UnityEngine;

namespace SceneManagement
{
    public class AreaEntrance : MonoBehaviour
    {
        [SerializeField] private string transitionName;

        private void Start()
        {
            if (transitionName != SceneManagementS.Instance.SceneTransitionName) return;
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerAsFollowTarget();
            UIFade.Instance.FadeFromBlack();
        }
    }
}
