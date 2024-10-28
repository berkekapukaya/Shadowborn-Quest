using Enemies;
using UnityEngine;

namespace Player
{
    public class DamageSource : MonoBehaviour
    {
        [SerializeField] private int damage = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(1);
        }
    }
}