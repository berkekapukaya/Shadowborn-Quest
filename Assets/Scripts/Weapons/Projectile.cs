using Enemies;
using Inventory;
using Player;
using UnityEngine;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 22f;
        [SerializeField] private GameObject particleOnHitPrefabVFX;
        [SerializeField] private bool isEnemyProjectile = false;
        
        [SerializeField] private float _projectileRange = 10f;
        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            MoveProjectile();
            DetectFireDistance();
        }
        
        public void UpdateProjectileRange(float projectileRange)
        {
            _projectileRange = projectileRange;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemyHealth = other.GetComponent<EnemyHealth>();
            var indestructable = other.gameObject.GetComponent<Indestructable>();

            var player = other.gameObject.GetComponent<PlayerHealth>();

            if (other.isTrigger || (!enemyHealth && !indestructable && !player)) return;
            if (player && isEnemyProjectile)
            {
                player.TakeDamage(1, transform);   
            }
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
        private void DetectFireDistance()
        {
            if (!(Vector3.Distance(transform.position, _startPosition) > _projectileRange)) return;
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        private void MoveProjectile()
        {
            transform.Translate(Vector2.right * (moveSpeed * Time.deltaTime));
        }
    }
}
