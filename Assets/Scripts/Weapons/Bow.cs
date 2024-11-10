using Inventory;
using Player;
using UnityEngine;

namespace Weapons
{
    public class Bow : MonoBehaviour, IWeapon
    {
        
        [SerializeField] private WeaponInfo weaponInfo;
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private Transform arrowSpawnPoint;
        
        private Animator _myAnimator;
        
        readonly int FIRE_HASH = Animator.StringToHash("Fire");
        
        private void Awake()
        {
            _myAnimator = GetComponent<Animator>();
        }

        public void Attack()
        {
            _myAnimator.SetTrigger(FIRE_HASH);
            var newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
            newArrow.GetComponent<Projectile>().UpdateWeaponInfo(weaponInfo);
        }
        
        public WeaponInfo GetWeaponInfo()
        {
            return weaponInfo;
        }
    }
}
