using Inventory;
using Player;
using UnityEngine;

namespace Weapons
{
    public class Staff : MonoBehaviour, IWeapon
    {
        
        [SerializeField] private WeaponInfo weaponInfo;
        [SerializeField] private GameObject magicLaser;
        [SerializeField] private Transform laserSpawnPoint;

        private Animator _animator;
        
        readonly int STAFF_ATTACK_HASH = Animator.StringToHash("StaffAttack");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Attack()
        {
            _animator.SetTrigger(STAFF_ATTACK_HASH);
        }

        private void Update()
        {
            MouseFollowWithOffset();
        }
        
        public WeaponInfo GetWeaponInfo()
        {
            return weaponInfo;
        }
        
        public void SpawnStaffProjectileAnimEvent()
        {
            var newLaser = Instantiate(magicLaser, laserSpawnPoint.position, Quaternion.identity);
            newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
        }

        private void MouseFollowWithOffset()
        {
           
            var mousePos = Input.mousePosition;
            var playerScreenPoint = PlayerController.Instance.mainCamera.WorldToScreenPoint(PlayerController.Instance.transform.position);

            var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            if (mousePos.x < playerScreenPoint.x)
            {
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            }
            else
            {
                ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}
