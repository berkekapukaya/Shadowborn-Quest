using System;
using Misc;
using Player;
using UnityEngine;

namespace Enemies
{
    public class GrapeLandSplatter : MonoBehaviour
    {
        private SpriteFade _spriteFade;

        private void Awake()
        {
            _spriteFade = GetComponent<SpriteFade>();
        }
        
        private void Start()
        {
            StartCoroutine(_spriteFade.SlowFadeRoutine());

            Invoke("DisableCollider", 0.2f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Yo");
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth?.TakeDamage(1, transform);
            
        }

        private void DisableCollider()
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
}
