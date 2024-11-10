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
        [SerializeField] private int projectilePerBurst;
        [SerializeField] [Range(0, 359)] private float angleSpread;
        [SerializeField] private float startingDistance = .1f;
        [SerializeField] private float burstDelay;
        [SerializeField] private float restTime = 1f;
        [SerializeField] private bool stagger;
        [SerializeField] private bool oscillate;

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
            
            float startAngle, currentAngle, angleStep, endAngle;
            
            float timeBetweenProjectiles = 0;

            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            
            if(stagger){ timeBetweenProjectiles = burstDelay / projectilePerBurst;}

            for (var i = 0; i < burstCount; i++)
            {
                if (!oscillate)
                {
                    TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
                }

                if (oscillate && i % 2 != 1)
                {
                    TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
                }
                else if(oscillate)
                {
                    currentAngle = endAngle;
                    endAngle = startAngle;
                    startAngle = currentAngle;
                    angleStep = -angleStep;
                }
                
                for(var j = 0; j < projectilePerBurst; j++)
                {
                    var pos = FindBulletSpawnPos(currentAngle);
                
                    var newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                    newBullet.transform.right = newBullet.transform.position - transform.position;
            
                    if(newBullet.TryGetComponent(out Projectile projectile))
                    {
                        projectile.UpdateProjectileMoveSpeed(bulletMoveSpeed);
                    }
                
                    currentAngle += angleStep;

                    if (stagger)
                    {
                        yield return new WaitForSeconds(timeBetweenProjectiles);
                    }
                }
                
                currentAngle = startAngle;
                if(!stagger){yield return new WaitForSeconds(burstDelay);
                }
                
            }
            
            yield return new WaitForSeconds(restTime);
            _isShooting = false;
        }
        
        private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
        {
            Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
            var targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            startAngle = targetAngle;
            endAngle = targetAngle;
            currentAngle = targetAngle;
            var halfAngleSpread = 0f;
            angleStep = 0;
            if (angleSpread == 0) return;
            angleStep = angleSpread / (projectilePerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }


        private Vector2 FindBulletSpawnPos(float currentAngle)
        {
            var x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            var y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
            
            var pos = new Vector2(x, y);
            return pos;
        }
    }
}
