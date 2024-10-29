using UnityEngine;

namespace Misc
{
   public class Parallax : MonoBehaviour
   {
      [SerializeField] private float parallaxOffset = -0.15f;
      
      private Camera _cam;
      private Vector2 _startPos;
      private Vector2 TravelDir => (Vector2)_cam.transform.position - _startPos;

      private void Awake()
      {
         _cam = Camera.main;
      }

      private void Start()
      {
         _startPos = transform.position;
      }

      private void FixedUpdate()
      {
         transform.position = _startPos + TravelDir * parallaxOffset;
      }
   }
}
