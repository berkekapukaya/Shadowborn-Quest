using System;
using System.Collections;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Misc
{
    public class Pickups : MonoBehaviour
    {
        
        private enum PickupType
        {
            GoldCoin,
            HealthGlobe,
            StaminaGlobe
        }
        
        [SerializeField] private PickupType pickupType;
        
        [SerializeField] private float pickUpDistance = 5f;
        [SerializeField] private float accelerationRate = .2f;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private AnimationCurve animCurve;
        [SerializeField] private float heightY = 1.5f;
        [SerializeField] private float popDuration = 1f;
        
        private float _initialMoveSpeed;
        private Vector3 _moveDir;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _initialMoveSpeed = moveSpeed;
        }

        private void Start()
        {
            StartCoroutine(AnimCurveSpawnRoutine());
        }

        private void Update()
        {
            var playerPos = PlayerController.Instance.transform.position;
            
            if(Vector3.Distance(transform.position, playerPos) < pickUpDistance)
            {
                _moveDir = (playerPos - transform.position).normalized;
                moveSpeed += accelerationRate;
            }
            else
            {
                _moveDir = Vector3.zero;
                moveSpeed = _initialMoveSpeed;
            }
        }
        
        private void FixedUpdate()
        {
            _rb.velocity = _moveDir * (moveSpeed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                DetectPickupType();
                Destroy(gameObject);
            }
        }

        private IEnumerator AnimCurveSpawnRoutine()
        {
            var startPoint = transform.position;
            var spreadRadius = Random.Range(1f, 1.5f);
            var randomOffset = Random.insideUnitCircle * spreadRadius;
            var endPoint = startPoint + new Vector3(randomOffset.x, randomOffset.y, 0);
            
            var timePassed = 0f;

            while (timePassed < popDuration)
            {
                timePassed += Time.deltaTime;
                var t = timePassed / popDuration;
                var height = animCurve.Evaluate(t);
                transform.position = Vector2.Lerp(startPoint, endPoint, t) + new Vector2(0, height);
                yield return null;
            }
            {
                yield return null;
            }
        }

        private void DetectPickupType()
        {
            switch (pickupType)
            {
                case PickupType.GoldCoin:
                    Debug.Log("Picked up a coin");
                    EconomyManager.Instance.UpdateCurrentGold();
                    break;
                case PickupType.HealthGlobe:
                    Debug.Log("Picked up a health globe");
                    PlayerHealth.Instance.HealPlayer(1);
                    break;
                case PickupType.StaminaGlobe:
                    Debug.Log("Picked up a stamina globe");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
