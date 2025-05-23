using Player;
using UnityEngine;
using Weapons;

namespace Misc
{
    public class Destructable : MonoBehaviour
    {
        [SerializeField] private GameObject destroyVFX;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.GetComponent<DamageSource>()) return;
            
            var pickupSpawner = GetComponent<PickupSpawner>();
            pickupSpawner?.DropItems();
            
            Instantiate(destroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
