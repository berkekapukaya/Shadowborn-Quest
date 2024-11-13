using System.Collections;
using Player;
using UnityEngine;

namespace Enemies
{
    public class GrapeProjectile : MonoBehaviour
    {

        [SerializeField] private float duration = 1;

        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float heightY = 3f;
        
        [SerializeField] private GameObject grapeShadowPrefab;

        [SerializeField] private GameObject splatterPrefab;
        // Start is called before the first frame update
        private void Start()
        {
            var grapeShadow = Instantiate(grapeShadowPrefab, transform.position + new Vector3(0, -.3f, 0), Quaternion.identity);
            
            var playerPos = PlayerController.Instance.transform.position;
            
            var grapeShadowStartPos = grapeShadow.transform.position;
            
            StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
            StartCoroutine(MoveGrapeShadowRoutine(grapeShadow, grapeShadowStartPos, playerPos));
        }

        private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
        {
            float timePassed = 0;
            
            while (timePassed < duration)
            {
                timePassed += Time.deltaTime;
                var linearT = timePassed / duration;
                var heightT = curve.Evaluate(linearT);
                var height = Mathf.Lerp(0, heightY, heightT);
                transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);
                yield return null;
            }
            Instantiate(splatterPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        private IEnumerator MoveGrapeShadowRoutine(GameObject grapeShadow, Vector3 startPosition, Vector3 endPosition)
        {
            float timePassed = 0;
            
            while (timePassed < duration)
            {
                timePassed += Time.deltaTime;
                var linearT = timePassed / duration;
                grapeShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);
                yield return null;
            }
            
            Destroy(grapeShadow);
        }
    }
}
