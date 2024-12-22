namespace InventoryDTO.Weapons
{
    public class BigBulletWeaponData : WeaponData, ICreateDefaultValues, IUpgradeable
    {
        public BigBulletWeaponData()
        {
        }

        public ItemData CreateDefaultValues(string itemType, string element)
        {
            BigBulletWeaponData bigBulletWeaponData = new BigBulletWeaponData
            {
                Id = itemType + element,
                ItemType = itemType,
                Element = element,
                Name = "Big Bullet",
                
                TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 10, 120, 4, 1),
                ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 16),
                CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 4, 3f, 0.2f, 2),
                DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 1f),

            };

            return bigBulletWeaponData;
        }

        public void SetLevel(int level)
        {
            throw new System.NotImplementedException();
        }
    }
}