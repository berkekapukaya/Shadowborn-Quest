using UnityEngine;

namespace Misc
{
    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject coinPrefab;

        public void DropItems()
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
