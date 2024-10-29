using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class AreaExit : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;
        [SerializeField] private string sceneTransitionName;
        
        private float _waitToLoad = 1f;
        private void OnTriggerEnter2D(Collider2D other)
        {  
            if (!other.gameObject.GetComponent<PlayerController>()) return;
            SceneManagementS.Instance.SetTransitionName(sceneTransitionName);
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
        
        private IEnumerator LoadSceneRoutine()
        {
            yield return new WaitForSeconds(_waitToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
