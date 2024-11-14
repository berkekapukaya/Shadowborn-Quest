using System;
using System.Collections;
using Enemies;
using Misc;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : Singleton<PlayerHealth>
    {
        public bool IsDead { get; private set; }
        
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private float knockbackThrustAmount = 10f;
        [SerializeField] private float damageRecoveryTime = 1f;
        
        private int _currentHealth;
        private bool _canTakeDamage = true;

        private Slider _healthSlider;
        private Knockback _knockback;
        private Flash _flash;
        
        const string HEALTH_SLIDER_TEXT = "HealthSlider";
        const string TOWN_TEXT = "Town";
        readonly int DEATH_HASH = Animator.StringToHash("Death");
        protected override void Awake()
        {
            base.Awake();
            _knockback = GetComponent<Knockback>();
            _flash = GetComponent<Flash>();
        }
        private void Start()
        {
            IsDead = false;
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
            if (!_canTakeDamage) return;
            _knockback.GetKnockedBack(hitTransform, knockbackThrustAmount);
            StartCoroutine(_flash.FlashRoutine());
            _canTakeDamage = false;
            _currentHealth -= damage;
            CheckIfPlayerDead();
            UpdateHealthSlider();
            StartCoroutine(DamageRecoveryRoutine());
        }

        private void CheckIfPlayerDead()
        {
            if (_currentHealth > 0 || IsDead) return;
            _currentHealth = 0;
            IsDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
        
        private IEnumerator DeathLoadSceneRoutine()
        {
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
            Stamina.Instance.ResetStaminaOnDeath();
            SceneManager.LoadScene(TOWN_TEXT);
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
            _canTakeDamage = true;
        }

        private void UpdateHealthSlider()
        {
            if (_healthSlider == null)
            {
                _healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
            }
            
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = _currentHealth;
        }
    }
}
