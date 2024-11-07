using System;
using Player;
using UnityEngine;

namespace Inventory
{
    public class Staff : MonoBehaviour, IWeapon
    {
        
        [SerializeField] private WeaponInfo weaponInfo;

        public void Attack()
        {
            Debug.Log("Staff Attack");
        }

        private void Update()
        {
            MouseFollowWithOffset();
        }
        
        public WeaponInfo GetWeaponInfo()
        {
            return weaponInfo;
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
