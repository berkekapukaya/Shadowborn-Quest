using System;
using System.Collections;
using Enemies;
using Misc;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private float knockbackThrustAmount = 10f;
        [SerializeField] private float damageRecoveryTime = 1f;
        
        private int _currentHealth;
        private bool canTakeDamage = true;
        
        private Knockback _knockback;
        private Flash _flash;
        private void Awake()
        {
            _knockback = GetComponent<Knockback>();
            _flash = GetComponent<Flash>();
        }
        private void Start()
        {
            _currentHealth = maxHealth;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            var enemyAI = other.gameObject.GetComponent<EnemyAI>();

            if (!enemyAI || !canTakeDamage) return;
            TakeDamage(1);
            _knockback.GetKnockedBack(other.gameObject.transform, knockbackThrustAmount);
            StartCoroutine(_flash.FlashRoutine());
        }

        private void TakeDamage(int damage)
        {
            canTakeDamage = false;
            _currentHealth -= damage;
            StartCoroutine(DamageRecoveryRoutine());

        }

        private IEnumerator DamageRecoveryRoutine()
        {
            yield return new WaitForSeconds(damageRecoveryTime);
            canTakeDamage = true;
        }
    }
}
