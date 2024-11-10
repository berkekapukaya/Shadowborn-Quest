using System.Collections;
using Player;
using UnityEngine;
using Weapons;

namespace Enemies
{
    public class Shooter : MonoBehaviour, IEnemy
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletMoveSpeed;
        [SerializeField] private int burstCount;
        [SerializeField] private float burstDelay;
        [SerializeField] private float restTime = 1f;

        private bool _isShooting = false;
        
        
        public void Attack()
        {
            if (!_isShooting)
            {
                StartCoroutine(ShootRoutine());
            }
        }
        
        private IEnumerator ShootRoutine()
        {
            _isShooting = true;

            for (var i = 0; i < burstCount; i++)
            {
                Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
            
                var newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                newBullet.transform.right = targetDirection;
            
                if(newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateProjectileMoveSpeed(bulletMoveSpeed);
                }

                yield return new WaitForSeconds(burstDelay);
            }
            
            yield return new WaitForSeconds(restTime);
            _isShooting = false;
        }
    }
}
