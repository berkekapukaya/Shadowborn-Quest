using System;
using Player;
using SceneManagement;
using UnityEngine;

namespace Inventory
{
    public class ActiveInventory : Singleton<ActiveInventory>
    {
        private int _activeSlotIndexNum = 0;
        
        private PlayerControls _playerControls;

        protected override void Awake()
        {
            base.Awake();
            
            _playerControls = new PlayerControls();
        }
        
        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void Start()
        {
            _playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
            
        }

        private void ToggleActiveSlot(int numValue)
        {
            ToggleActiveHighlight(numValue - 1);
        }

        private void ToggleActiveHighlight(int indexNum)
        {
            _activeSlotIndexNum = indexNum;

            foreach (Transform inventorySlot in this.transform)
            {
                inventorySlot.GetChild(0).gameObject.SetActive(false);
            }
            
            this.transform.GetChild(_activeSlotIndexNum).GetChild(0).gameObject.SetActive(true);
            
            ChangeActiveWeapon();
        }

        private void ChangeActiveWeapon()
        {
            if (PlayerHealth.Instance.IsDead) return;
            
            if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
            {
                Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
            }
            
            // var weaponToSpawn = transform.GetChild(_activeSlotIndexNum).GetComponentInChildren<InventorySlot>()
            //     .GetWeaponInfo().weaponPrefab; // Cleaned this one as down below
            
            var childTransform = transform.GetChild(_activeSlotIndexNum);
            var inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
            var weaponInfo = inventorySlot.GetWeaponInfo();

            if (!weaponInfo)
            {
                ActiveWeapon.Instance.WeaponNull();
                return;
            }
            
            var weaponToSpawn = weaponInfo.weaponPrefab;
            var newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            
            newWeapon.transform.parent = ActiveWeapon.Instance.transform;
            
            ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
        }

        public void EquipStartingWeapon()
        {
            ToggleActiveHighlight(0);
        }
    }
}
