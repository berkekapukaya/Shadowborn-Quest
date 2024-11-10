using System.Collections;
using Misc;
using Player;
using UnityEngine;

namespace Weapons
{
    public class MagicLaser : MonoBehaviour
    {
        [SerializeField] private float laserGrowTime = 2f;
        private float _laserRange;
        private SpriteRenderer _spriteRenderer;
        private CapsuleCollider2D _collider2D;
        private bool _isGrowing = true;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<CapsuleCollider2D>();
        }

        private void Start()
        {
            LaserFaceMouse();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Indestructable>() && !other.isTrigger)
            {
                _isGrowing = false;
            }
        }

        public void UpdateLaserRange(float range)
        {
            _laserRange = range;
            StartCoroutine(IncreaseLaserLenghtRoutine());
        }

        private IEnumerator IncreaseLaserLenghtRoutine()
        {
            var timePassed = 0f;

            while (_spriteRenderer.size.x < _laserRange && _isGrowing)
            {
                timePassed += Time.deltaTime;
                var linearT = timePassed / laserGrowTime;

                _spriteRenderer.size = new Vector2(Mathf.Lerp(1f, _laserRange, linearT), 1f);
                _collider2D.size = new Vector2(Mathf.Lerp(1f, _laserRange, linearT) - 0.1f, _collider2D.size.y);
                _collider2D.offset = new Vector2(Mathf.Lerp(1f, _laserRange, linearT) / 2, _collider2D.offset.y);

                yield return null;
            }

            StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
        }
        private void LaserFaceMouse()
        {
            var mousePosition = Input.mousePosition;
            
            mousePosition = PlayerController.Instance.mainCamera.ScreenToWorldPoint(mousePosition);

            Vector2 direction = transform.position - mousePosition;

            transform.right = -direction;
        }
    }
}
