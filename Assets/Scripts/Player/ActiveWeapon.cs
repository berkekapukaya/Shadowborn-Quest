using System.Collections;
using Inventory;
using SceneManagement;
using UnityEngine;

namespace Player
{
    public class ActiveWeapon : Singleton<ActiveWeapon>
    {
        public MonoBehaviour CurrentActiveWeapon { get; private set; }
        
        private float _timeBetweenAttacks;
        
        private PlayerControls _playerControls;
        private bool _attackButtonDown , _isAttacking = false;

        protected override void Awake()
        {
            base.Awake();
            _playerControls = new PlayerControls();
        }
        
        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void Start()
        {
            _playerControls.Combat.Attack.started += _ => StartAttacking();
            _playerControls.Combat.Attack.canceled += _ => StopAttacking();
            
            AttackCooldown();
        }

        private void Update()
        {
            Attack();
        }

        public void NewWeapon(MonoBehaviour newWeapon)
        {
            CurrentActiveWeapon = newWeapon;
            AttackCooldown();
            _timeBetweenAttacks = (CurrentActiveWeapon as IWeapon)?.GetWeaponInfo().weaponCooldown ?? 0.5f;
        }
        
        public void WeaponNull()
        {
            CurrentActiveWeapon = null;
        }

        private void AttackCooldown()
        {
            _isAttacking = true;
            StopAllCoroutines();
            StartCoroutine(TimeBetweenAttacksRoutine());
        }

        private IEnumerator TimeBetweenAttacksRoutine()
        {
            yield return new WaitForSeconds(_timeBetweenAttacks);
            _isAttacking = false;
        }

        private void StartAttacking()
        {
            _attackButtonDown = true;
        }

        private void StopAttacking()
        {
            _attackButtonDown = false;
        }

        private void Attack()
        {
            if (!_attackButtonDown || _isAttacking || !CurrentActiveWeapon) return;
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon)?.Attack();

        }
    }
}
