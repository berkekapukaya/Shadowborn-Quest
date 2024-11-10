using Enemies;
using Inventory;
using UnityEngine;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 22f;
        [SerializeField] private GameObject particleOnHitPrefabVFX;
        
        private WeaponInfo _weaponInfo;
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
        
        public void UpdateWeaponInfo(WeaponInfo weaponInfo)
        {
            _weaponInfo = weaponInfo;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemyHealth = other.GetComponent<EnemyHealth>();
            var indestructable = other.gameObject.GetComponent<Indestructable>();

            if (other.isTrigger || (!enemyHealth && !indestructable)) return;
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
        private void DetectFireDistance()
        {
            if (!(Vector3.Distance(transform.position, _startPosition) > _weaponInfo.weaponRange)) return;
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        private void MoveProjectile()
        {
            transform.Translate(Vector2.right * (moveSpeed * Time.deltaTime));
        }
    }
}
