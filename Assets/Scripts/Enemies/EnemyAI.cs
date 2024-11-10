using System.Collections;
using Player;
using UnityEngine;
namespace Enemies
{
   public class EnemyAI : MonoBehaviour
   {
      [SerializeField] private float roamChangeDirFrequency = 2f;
      [SerializeField] private float attackRange = 5f;
      [SerializeField] private MonoBehaviour enemyType;
      [SerializeField] private float attackCooldown = 2f;
      [SerializeField] private bool stopMovingWhileAttacking = false;
      
      private enum State
      {
         Roaming,
         Attacking
      }

      private State _state;
      private EnemyPathfinding _enemyPathfinding;
      private Rigidbody2D _rb;
      private Vector2 _roamingPosition;
      private float _timeRoaming = 0f;

      private bool _canAttack = true;
   
      [SerializeField] private float moveSpeed = 4f;

      private void Awake()
      {
         _enemyPathfinding = GetComponent<EnemyPathfinding>();
         _rb = GetComponent<Rigidbody2D>();
         _state = State.Roaming;
      }

      private void Start()
      {
         _roamingPosition = GetRoamingPosition();
      }

      private void Update()
      {
         MovementStateControl();
      }
      
      private void MovementStateControl()
      {
         switch (_state)
         {
            default:
            case State.Roaming:
               Roaming();
               break;
            case State.Attacking:
               Attacking();
               break;
         }
      }
      private void Roaming()
      {
         _timeRoaming += Time.deltaTime;
         
         _enemyPathfinding.MoveTo(_roamingPosition);
         
         if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= attackRange)
         {
            _state = State.Attacking;
         }
         
         if(_timeRoaming >= roamChangeDirFrequency)
         {
            _roamingPosition = GetRoamingPosition();
            _timeRoaming = 0f;
         }
      }
      private void Attacking()
      {
         if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) >= attackRange)
         {
            _state = State.Roaming;
         }
         if (!_canAttack || attackRange == 0) return;
         _canAttack = false;
         (enemyType as IEnemy)?.Attack();
         
         if (stopMovingWhileAttacking)
         {
            _enemyPathfinding.StopMoving();
         }
         else
         {
            _enemyPathfinding.MoveTo(_roamingPosition);
         }
         
         StartCoroutine(AttackCooldownRoutine());
      }
      private IEnumerator AttackCooldownRoutine()
      {
         yield return new WaitForSeconds(attackCooldown);
         _canAttack = true;
      }
      private Vector2 GetRoamingPosition()
      {
         _timeRoaming = 0f;
         return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
      }
   }
}
