using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Misc
{
    public class TransparencyDetection : MonoBehaviour
    {
        [Range(0, 1)] [SerializeField] private float transparencyAmount = .8f;

        [SerializeField] private float fadeTime = .4f;
        private SpriteRenderer _spriteRenderer;
        private Tilemap _tilemap;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _tilemap = GetComponent<Tilemap>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.GetComponent<PlayerController>()) return;
            if (_spriteRenderer)
            {
                StartCoroutine(FadeRoutine(_spriteRenderer, fadeTime, _spriteRenderer.color.a, transparencyAmount));
            }else if (_tilemap)
            {
                StartCoroutine(FadeRoutine(_tilemap, fadeTime, _tilemap.color.a, transparencyAmount));
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.GetComponent<PlayerController>()) return;
            if (_spriteRenderer)
            {
                StartCoroutine(FadeRoutine(_spriteRenderer, fadeTime, _spriteRenderer.color.a, 1f));
            }else if (_tilemap)
            {
                StartCoroutine(FadeRoutine(_tilemap, fadeTime, _tilemap.color.a, 1f));
            }
        }

        private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetValue)
        {
            var elapsedTime = 0f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                var newColor = spriteRenderer.color;
                newColor.a = Mathf.Lerp(startValue, targetValue, elapsedTime / fadeTime);
                spriteRenderer.color = newColor;
                yield return null;
            }
        }
        
        private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetValue)
        {
            var elapsedTime = 0f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                var newColor = tilemap.color;
                newColor.a = Mathf.Lerp(startValue, targetValue, elapsedTime / fadeTime);
                tilemap.color = newColor;
                yield return null;
            }
        }
    }
}