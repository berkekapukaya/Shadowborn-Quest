using UnityEngine;
using UnityEngine.UI;

namespace Misc
{
    public class CursorManager : MonoBehaviour
    {
        private Camera _mainCamera;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Start()
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

        private void Update()
        {
            Vector2 cursorPos = Input.mousePosition;
            _image.rectTransform.position = cursorPos;
            
            if (!Application.isPlaying) { return;}
            
            Cursor.visible = false;

        }
    }
}
