using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Misc
{
    public class CursorManager : Singleton<CursorManager>
    {
        private Camera _mainCamera;

        private Image _image;

        protected override void Awake()
        {
            base.Awake();
            
            SetMainCamera();
            _image = GetComponent<Image>();
        }

        void Start()
        {
            Cursor.visible = false;
            if (Application.isPlaying)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        void Update()
        {
            Vector2 cursorPos = Input.mousePosition;
            _image.rectTransform.position = cursorPos;
            
            if (!Application.isPlaying) { return;}
            
            Cursor.visible = false;

        }
        
        public void SetMainCamera()
        {
            _mainCamera = Camera.main;
        }
    }
}
