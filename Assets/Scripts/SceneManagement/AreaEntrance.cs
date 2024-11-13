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
            Debug.Log("You are entering from the correct entrance");
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerAsFollowTarget();
            CursorManager.Instance.SetMainCamera();
            UIFade.Instance.FadeFromBlack();
        }
    }
}
