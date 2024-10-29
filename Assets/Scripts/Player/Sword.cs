using System.Collections;
using UnityEngine;

namespace Player
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] private GameObject slashAnimPrefab;
        [SerializeField] private Transform slashAnimSpawnPoint;
        [SerializeField] private Transform weaponCollider;
        [SerializeField] private float attackCooldown = 0.5f;
         
        private PlayerControls _playerControls;
        private Animator _myAnimator;
        private PlayerController _playerController;
        private ActiveWeapon _activeWeapon;
        private GameObject _slashAnim;
        private bool _attackButtonDown = false;
        private bool _isAttacking = true;
        
        private void Awake()
        {
            _playerController = GetComponentInParent<PlayerController>();
            _activeWeapon = GetComponentInParent<ActiveWeapon>();
            _myAnimator = GetComponent<Animator>();
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
        }

        private void Update()
        {
            MouseFollowWithOffset();
            if (!_isAttacking) return;
            Attack();
        }
        
        private void StartAttacking()
        {
            _attackButtonDown = true;
        }

        private void StopAttacking()
        {
            _attackButtonDown = false;
        }

        private IEnumerator AttackCooldownRoutine()
        {
            _isAttacking = false;
            yield return new WaitForSeconds(attackCooldown);
            _isAttacking = true;
        }

        private void Attack()
        {
            if (!_attackButtonDown) return;
            _myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            _slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            _slashAnim.transform.parent = transform.parent;
            StartCoroutine(AttackCooldownRoutine());
        }

        public void DoneAttackAnimEvent()
        {
            weaponCollider.gameObject.SetActive(false);
        }

        public void SwingUpFlipAnim()
        {
            if(_slashAnim == null) return;
            _slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
            if (_playerController.FacingLeft)
            {
                _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    
        public void SwingDownFlipAnim()
        {
            if(_slashAnim == null) return;
            _slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            if (_playerController.FacingLeft)
            {
                _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        private void MouseFollowWithOffset()
        {
           
            var mousePos = Input.mousePosition;
            var playerScreenPoint = PlayerController.Instance.mainCamera.WorldToScreenPoint(_playerController.transform.position);

            var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            if (mousePos.x < playerScreenPoint.x)
            {
                _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
                weaponCollider.rotation = Quaternion.Euler(0, -180, 0);
            }
            else
            {
                _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
                weaponCollider.rotation = Quaternion.Euler(0, 0, 0);
            }
        
        }

    }
}
