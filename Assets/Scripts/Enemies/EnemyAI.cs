using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemies
{
   public class EnemyAI : MonoBehaviour
   {
      [SerializeField] private float roamChangeDirFrequency = 2f;
      
      private enum State
      {
         Roaming,
      }

      private State _state;
      private EnemyPathfinding _enemyPathfinding;
      private Rigidbody2D _rb;
   
      [SerializeField] private float moveSpeed = 4f;

      private void Awake()
      {
         _enemyPathfinding = GetComponent<EnemyPathfinding>();
         _rb = GetComponent<Rigidbody2D>();
         _state = State.Roaming;
      }

      private void Start()
      {
         StartCoroutine(RoamingRoutine());
      }
   
      private IEnumerator RoamingRoutine()
      {
         while (_state == State.Roaming)
         {
            var roamingPosition = GetRoamingPosition();
            _enemyPathfinding.MoveTo(roamingPosition);
            yield return new WaitForSeconds(roamChangeDirFrequency);
         }
      }

      private Vector2 GetRoamingPosition()
      {
         return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
      }
   }
}
