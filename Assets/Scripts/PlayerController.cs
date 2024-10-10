using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Vector2 _movement;
    private Rigidbody2D _rb;

    private PlayerControls _playerControls;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private Camera _mainCamera;
    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
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
        myAnimator.SetFloat(StringConstants.moveX, _movement.x);
        myAnimator.SetFloat(StringConstants.moveY, _movement.y);
    }

    private void Move()
    {
        _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        var mousePos = Input.mousePosition;
        var playerScreenPoint = _mainCamera.WorldToScreenPoint(transform.position);
        mySpriteRenderer.flipX = mousePos.x < playerScreenPoint.x;
    }
    
}
