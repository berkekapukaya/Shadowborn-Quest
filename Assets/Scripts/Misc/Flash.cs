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

        private Material _defaultMat;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultMat = _spriteRenderer.material;
        }

        public IEnumerator FlashRoutine()
        {
            _spriteRenderer.material = whiteFlashMat;
            yield return new WaitForSeconds(restoreDefaultMatTime);
            _spriteRenderer.material = _defaultMat;
        }
        
        public float GetRestoreDefaultMatTime()
        {
            return restoreDefaultMatTime;
        }
    }
}
