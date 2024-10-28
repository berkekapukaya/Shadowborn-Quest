using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int startingHealth = 3;
    
        private int _currentHealth;

        private void Start()
        {
            _currentHealth = startingHealth;
        }
        
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            Debug.Log("Enemy took damage" + _currentHealth);
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Enemy died");
            Destroy(gameObject);
        }
    }
}
