using System.Collections;
using UnityEngine;

namespace Misc
{
    public class SpriteFade : MonoBehaviour
    {
        [SerializeField] private float fadeTime = .4f;
        private SpriteRenderer _spriteRenderer;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public IEnumerator SlowFadeRoutine()
        {
            var elapsedTime = 0f;
            var startValue = _spriteRenderer.color.a;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                var newColor = _spriteRenderer.color;
                newColor.a = Mathf.Lerp(startValue, 0f, elapsedTime / fadeTime);
                _spriteRenderer.color = newColor;
                yield return null;
            }
            
            Destroy(gameObject);
        }
    }
}