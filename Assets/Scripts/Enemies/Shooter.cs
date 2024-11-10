using Player;
using UnityEngine;

namespace Enemies
{
    public class Shooter : MonoBehaviour, IEnemy
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
    
        public void Attack()
        {
            Debug.Log("Shooter is attacking");
            Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
            
            var newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.transform.right = targetDirection;
        }
    }
}
