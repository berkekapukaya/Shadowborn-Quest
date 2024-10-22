using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Animator _myAnimator;

    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    void Start()
    {
        _playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Attack()
    {
        //Fire our sword animation
        _myAnimator.SetTrigger("Attack");
    }

}
