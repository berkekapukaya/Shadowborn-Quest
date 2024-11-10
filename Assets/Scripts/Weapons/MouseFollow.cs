using Player;
using UnityEngine;

namespace Weapons
{
    public class MouseFollow : MonoBehaviour
    {
        private void Update()
        {
            FaceMouse();
        }
        
        private void FaceMouse()
        {
            var mousePosition = Input.mousePosition;
            
            mousePosition = PlayerController.Instance.mainCamera.ScreenToWorldPoint(mousePosition);

            Vector2 direction = transform.position - mousePosition;

            transform.right = -direction;
        }
    }
}
