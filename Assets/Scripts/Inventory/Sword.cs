using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Inventory
{
    public class Sword : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject slashAnimPrefab;
        [SerializeField] private float attackCooldown = 0.5f;
        [SerializeField] private WeaponInfo weaponInfo;
         
        private Transform _slashAnimSpawnPoint;
        private Transform _weaponCollider;
        private Animator _myAnimator;
        private GameObject _slashAnim;
        
        private readonly int ATTACK_HASH = Animator.StringToHash("Attack");
        private void Awake()
        {
            _myAnimator = GetComponent<Animator>();
            _weaponCollider = PlayerController.Instance.GetWeaponCollider();
            _slashAnimSpawnPoint = PlayerController.Instance.GetSlashAnimSpawnPoint();
            //Don't use the one below it's bad practice. It's just there to know that it is a another way of doing it.
            // slashAnimSpawnPoint = GameObject.Find("SlashAnimationSpawnPoint").transform;
        }
        private void Update()
        {
            MouseFollowWithOffset();
        }
        
        public WeaponInfo GetWeaponInfo()
        {
            return weaponInfo;
        }
        
        public void Attack()
        {
            _myAnimator.SetTrigger(ATTACK_HASH);
            _weaponCollider.gameObject.SetActive(true);
            _slashAnim = Instantiate(slashAnimPrefab, _slashAnimSpawnPoint.position, Quaternion.identity);
            _slashAnim.transform.parent = transform.parent;
        }

        public void DoneAttackAnimEvent()
        {
            _weaponCollider.gameObject.SetActive(false);
        }

        public void SwingUpFlipAnim()
        {
            if(_slashAnim == null) return;
            _slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
            if (PlayerController.Instance.FacingLeft)
            {
                _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    
        public void SwingDownFlipAnim()
        {
            if(_slashAnim == null) return;
            _slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (PlayerController.Instance.FacingLeft)
            {
                _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        private void MouseFollowWithOffset()
        {
           
            var mousePos = Input.mousePosition;
            var playerScreenPoint = PlayerController.Instance.mainCamera.WorldToScreenPoint(PlayerController.Instance.transform.position);

            var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            if (mousePos.x < playerScreenPoint.x)
            {
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
                _weaponCollider.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
                _weaponCollider.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        private void OnDisable()
        {
            // ActiveWeapon.Instance.ToggleIsAttacking(false);
            // _weaponCollider.gameObject.SetActive(false);
        }
    }
}
