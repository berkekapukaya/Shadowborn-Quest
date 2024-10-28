using System;
using System.Collections;
using Enemies;
using UnityEngine;

namespace Misc
{
    public class Flash : MonoBehaviour
    {
        [SerializeField] private Material whiteFlashMat;
        [SerializeField] private float restoreDefaultMatTime = 0.2f;
        private EnemyHealth _enemyHealth;

        private Material defaultMat;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            defaultMat = spriteRenderer.material;
        }

        public IEnumerator FlashRoutine()
        {
            spriteRenderer.material = whiteFlashMat;
            yield return new WaitForSeconds(restoreDefaultMatTime);
            spriteRenderer.material = defaultMat;
        }
        
        public float GetRestoreDefaultMatTime()
        {
            return restoreDefaultMatTime;
        }
    }
}
