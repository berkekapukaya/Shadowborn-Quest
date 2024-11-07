namespace Inventory
{
    internal interface IWeapon
    {
        public void Attack();

        public WeaponInfo GetWeaponInfo();
    }
}