using System;
using System.Collections;
using Misc;
using Player;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int startingHealth = 3;
        [SerializeField] private GameObject deathVFXPrefab;
    
        private int _currentHealth;
        private Knockback _knockback;
        private Flash _flash;
        private void Awake()
        {
            _flash = GetComponent<Flash>();
            _knockback = GetComponent<Knockback>();
        }

        private void Start()
        {
            _currentHealth = startingHealth;
        }
        
        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
            StartCoroutine(_flash.FlashRoutine());
            StartCoroutine(DelayCheckHealth());
        }
        
        private IEnumerator DelayCheckHealth()
        {
            yield return new WaitForSeconds(_flash.GetRestoreDefaultMatTime());
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (_currentHealth <= 0)
            {
                Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
