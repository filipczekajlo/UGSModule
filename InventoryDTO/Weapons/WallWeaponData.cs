namespace InventoryDTO.Weapons
{
    public class WallWeaponData : WeaponData, ICreateDefaultValues, IUpgradeable
    {
        public ItemFloatProperty Duration { get; set; } = new ItemFloatProperty(StringConsts.Duration, 4);

        public ItemFloatProperty Distance { get; set; } = new ItemFloatProperty(StringConsts.Distance, 8, 16, 0.5f, 2);

        public ItemData CreateDefaultValues(string itemType, string element)
        {
            WallWeaponData fieldWeaponData = new WallWeaponData
            {
                Id = itemType + element,
                ItemType = itemType,
                Element = element,
                Name = "Wall Weapon",
                
                TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 16, 160, 8, 1),
                ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 25),
                CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 4, 3f, 0.2f, 4),
                DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 1f),

            };

            return fieldWeaponData;
        }

        public void Upgrade(int level)
        {
            throw new System.NotImplementedException();
        }
    }
}