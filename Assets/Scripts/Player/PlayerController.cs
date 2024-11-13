using System.Collections;
using Helpers;
using Misc;
using UnityEngine;
using SceneManagement;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        public bool FacingLeft { get; private set; } = false;

        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float dashSpeed = 4f;
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private Transform weaponCollider;
        [SerializeField] private Transform slashAnimSpawnPoint;

        private Vector2 _movement;
        private Rigidbody2D _rb;

        private const float DashTime = .2f;
        private const float DashCd = 0.25f;

        private PlayerControls _playerControls;
        private Animator _myAnimator;
        private SpriteRenderer _mySpriteRenderer;
        private Knockback _knockback;
        
        private bool _isDashing = false;
        private float _startingMoveSpeed;
        
        [HideInInspector]
        public Camera mainCamera;

        protected override void Awake()
        {
            base.Awake();

            _playerControls = new PlayerControls();
            _rb = GetComponent<Rigidbody2D>();
            _myAnimator = GetComponent<Animator>();
            _mySpriteRenderer = GetComponent<SpriteRenderer>();
            _knockback = GetComponent<Knockback>();
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void Start()
        {
            _playerControls.Combat.Dash.performed += _ => Dash();
            _startingMoveSpeed = moveSpeed;
        }

        private void Update()
        {
            PlayerInput();
        }

        private void FixedUpdate()
        {
            Move();
            AdjustPlayerFacingDirection();
        }

        public Transform GetWeaponCollider()
        {
            return weaponCollider;
        }
        
        public Transform GetSlashAnimSpawnPoint()
        {
            return slashAnimSpawnPoint;
        }

        private void PlayerInput()
        {
            _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
            _myAnimator.SetFloat(StringConstants.moveX, _movement.x);
            _myAnimator.SetFloat(StringConstants.moveY, _movement.y);
        }

        private void Move()
        {
            if (_knockback.GettingKnockedBack) return;
            _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
        }

        private void AdjustPlayerFacingDirection()
        {
            var mousePos = Input.mousePosition;
            var playerScreenPoint = mainCamera.WorldToScreenPoint(transform.position);
            _mySpriteRenderer.flipX = mousePos.x < playerScreenPoint.x;
            FacingLeft = _mySpriteRenderer.flipX;
        }

        private void Dash()
        {
            if (_isDashing || Stamina.Instance.CurrentStamina <= 0) return;
            Stamina.Instance.UseStamina();
            _isDashing = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }

        private IEnumerator EndDashRoutine()
        {
            yield return new WaitForSeconds(DashTime);
            moveSpeed = _startingMoveSpeed;
            trailRenderer.emitting = false;
            yield return new WaitForSeconds(DashCd);
            _isDashing = false;
        }
    }
}