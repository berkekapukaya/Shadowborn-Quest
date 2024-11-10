using Enemies;
using Inventory;
using Player;
using UnityEngine;

namespace Weapons
{
    public class DamageSource : MonoBehaviour
    {
        private int _damage = 1;
    
        private void Start()
        {
            var currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
            _damage = (currentActiveWeapon as IWeapon)?.GetWeaponInfo().weaponDamage ?? _damage;
        } 
        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(_damage);
        }
    }
}