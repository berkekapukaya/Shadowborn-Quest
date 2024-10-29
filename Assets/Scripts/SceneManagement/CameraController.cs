using Cinemachine;
using Player;
using UnityEngine;

namespace SceneManagement
{
    public class CameraController : Singleton<CameraController>
    {
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        public void SetPlayerAsFollowTarget()
        {
            _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            _cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
            SetMainCamera();
        }

        private static void SetMainCamera()
        {
            PlayerController.Instance.mainCamera = Camera.main;
        }
    }
}
