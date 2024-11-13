using UnityEngine;

namespace Misc
{
    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject coinPrefab, heartGlobe, staminaGlobe;

        public void DropItems()
        {
            var randNum = Random.Range(1, 5);
            
            if (randNum == 1)
            {
                Instantiate(heartGlobe, transform.position, Quaternion.identity);
            }
            
            if (randNum == 2)
            {
                Instantiate(staminaGlobe, transform.position, Quaternion.identity);
            }
            
            if(randNum == 3)
            {
                var randAmount = Random.Range(1, 4);

                for (var i = 0; i <= randAmount; i++)
                {
                    Instantiate(coinPrefab, transform.position, Quaternion.identity);
                }

            }
            
        }
    }
}
