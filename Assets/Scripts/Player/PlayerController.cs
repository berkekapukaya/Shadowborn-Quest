using System.Collections;
using Helpers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public bool FacingLeft { get; private set; } = false;
        public static PlayerController Instance { get; private set; }
    
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float dashSpeed = 4f;
        [SerializeField] private TrailRenderer trailRenderer;
        
        private Vector2 _movement;
        private Rigidbody2D _rb;

        private const float DashTime = .2f;
        private const float DashCd = 0.25f;

        private PlayerControls _playerControls;
        private Animator _myAnimator;
        private SpriteRenderer _mySpriteRenderer;
        private Camera _mainCamera;
        private bool _isDashing = false;
        private float startingMoveSpeed;

        private void Awake()
        {
            Instance = this;
            _playerControls = new PlayerControls();
            _rb = GetComponent<Rigidbody2D>();
            _myAnimator = GetComponent<Animator>();
            _mySpriteRenderer = GetComponent<SpriteRenderer>();
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void Start()
        {
            _playerControls.Combat.Dash.performed += _ => Dash();
            startingMoveSpeed = moveSpeed;

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

        private void PlayerInput()
        {
            _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
            _myAnimator.SetFloat(StringConstants.moveX, _movement.x);
            _myAnimator.SetFloat(StringConstants.moveY, _movement.y);
        }

        private void Move()
        {
            _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
        }

        private void AdjustPlayerFacingDirection()
        {
            var mousePos = Input.mousePosition;
            var playerScreenPoint = _mainCamera.WorldToScreenPoint(transform.position);
            _mySpriteRenderer.flipX = mousePos.x < playerScreenPoint.x;
            FacingLeft = _mySpriteRenderer.flipX;
        }

        private void Dash()
        {
            if (_isDashing) return; 
            _isDashing = true;
            moveSpeed *= dashSpeed;
            trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }

        private IEnumerator EndDashRoutine()
        {
            yield return new WaitForSeconds(DashTime);
            moveSpeed = startingMoveSpeed;
            trailRenderer.emitting = false;
            yield return new WaitForSeconds(DashCd);
            _isDashing = false;
        }
    
    }
}
