using System;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Animator _myAnimator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    private Camera _mainCamera;

    private GameObject slashAnim;

    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;

    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _mainCamera = Camera.main;
        _myAnimator = GetComponent<Animator>();
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void Attack()
    {
        _myAnimator.SetTrigger("Attack");
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void SwingUpFlipAnim()
    {
        if(slashAnim == null) return;
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if (_playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    public void SwingDownFlipAnim()
    {
        if(slashAnim == null) return;
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (_playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        var mousePos = Input.mousePosition;
        var playerScreenPoint = _mainCamera.WorldToScreenPoint(_playerController.transform.position);

        var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        
        _activeWeapon.transform.rotation = mousePos.x < playerScreenPoint.x ? Quaternion.Euler(0, -180, angle) : Quaternion.Euler(0, 0, angle);
    }

}
