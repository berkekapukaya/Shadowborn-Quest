using Misc;
using UnityEngine;

namespace Enemies
{
    public class EnemyPathfinding : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;
    
        private Rigidbody2D _rb;
        private Vector2 moveDir;
        private Knockback _knockback;
    
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _knockback = GetComponent<Knockback>();
        }

        private void FixedUpdate()
        {
            if(_knockback.GettingKnockedBack) return;
            _rb.MovePosition(_rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
        }
    
        public void MoveTo(Vector2 targetPosition)
        {
            moveDir = targetPosition;
        }
    }
}
