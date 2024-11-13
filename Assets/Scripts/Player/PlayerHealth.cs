using System;
using System.Collections;
using Enemies;
using Misc;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : Singleton<PlayerHealth>
    {
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private float knockbackThrustAmount = 10f;
        [SerializeField] private float damageRecoveryTime = 1f;
        
        private int _currentHealth;
        private bool canTakeDamage = true;

        private Slider healthSlider;
        private Knockback _knockback;
        private Flash _flash;
        protected override void Awake()
        {
            base.Awake();
            _knockback = GetComponent<Knockback>();
            _flash = GetComponent<Flash>();
        }
        private void Start()
        {
            _currentHealth = maxHealth;
            UpdateHealthSlider();
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
            CheckIfPlayerDead();
            UpdateHealthSlider();
            StartCoroutine(DamageRecoveryRoutine());
        }

        private void CheckIfPlayerDead()
        {
            if (_currentHealth > 0) return;
            _currentHealth = 0;
            Debug.Log("Player is dead");
        }
        
        public void HealPlayer(int healAmount)
        {
            _currentHealth += healAmount;
            if (_currentHealth > maxHealth)
            {
                _currentHealth = maxHealth;
            }
            UpdateHealthSlider();
        }

        private IEnumerator DamageRecoveryRoutine()
        {
            yield return new WaitForSeconds(damageRecoveryTime);
            canTakeDamage = true;
        }

        private void UpdateHealthSlider()
        {
            if (healthSlider == null)
            {
                healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
            }
            
            healthSlider.maxValue = maxHealth;
            healthSlider.value = _currentHealth;
        }
    }
}
