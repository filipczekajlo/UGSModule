namespace InventoryDTO.Weapons
{
    public class SprintWeaponData : WeaponData, ICreateDefaultValues, IUpgradeable
    {
        public ItemData CreateDefaultValues(string itemType, string element)
        {
            SprintWeaponData sprintWeaponData = new SprintWeaponData
            {
                Id = itemType + element,
                ItemType = itemType,
                Element = element,
                Name = "Sprint Weapon",
                
                TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 0),
                ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 20),
                CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 5, 4f, 0.1f, 1),
                DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 0f),

            };

            return sprintWeaponData;
        }

        public void SetLevel(int level)
        {
            throw new System.NotImplementedException();
        }
    }
}