using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Inventory/New Weapon")]
    public class WeaponInfo : ScriptableObject
    {
        public GameObject weaponPrefab;
        public float weaponCooldown;
        public int weaponDamage;
        public float weaponRange;
    }
}
