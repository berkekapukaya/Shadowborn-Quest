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

            if (!enemyAI) return;
            TakeDamage(1, other.transform);
           
        }

        public void TakeDamage(int damage, Transform hitTransform)
        {
            if (!canTakeDamage) return;
            _knockback.GetKnockedBack(hitTransform, knockbackThrustAmount);
            StartCoroutine(_flash.FlashRoutine());
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
