using Misc;
using UnityEngine;

namespace Enemies
{
    public class EnemyPathfinding : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
    
        private Rigidbody2D _rb;
        private Vector2 _moveDir;
        private Knockback _knockback;
        private SpriteRenderer _spriteRenderer;
    
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _knockback = GetComponent<Knockback>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            if(_knockback.GettingKnockedBack) return;
            _rb.MovePosition(_rb.position + _moveDir * (moveSpeed * Time.fixedDeltaTime));
            _spriteRenderer.flipX = _moveDir.x < 0;
        }
    
        public void MoveTo(Vector2 targetPosition)
        {
            _moveDir = targetPosition;
        }
        
        public void StopMoving()
        {
            _moveDir = Vector2.zero;
        }
    }
}
