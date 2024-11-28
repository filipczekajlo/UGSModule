using System.Collections.Generic;

namespace InventoryDTO.Weapons
{
    public class HealWeaponData : WeaponData, ICreateDefaultValues, IUpgradeable
    {
        public ItemFloatProperty TotalHealingAmount { get; set; } = new ItemFloatProperty(StringConsts.TotalHealAmount, 18, 60, 4, 1);
        public ItemFloatProperty Duration { get; set; } = new ItemFloatProperty(StringConsts.Duration, 8, 20, 1, 1);
        public ItemData CreateDefaultValues(string itemType, string element)
        {
            HealWeaponData healWeaponData = new HealWeaponData
            {
                Id = itemType + element,
                ItemType = itemType,
                Element = element,
                Name = "Heal Weapon",
                
                TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, -1),
                ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 25),
                CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 4, 5f, 0.1f, 1),
                DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 0.5f),
                
                SpecificProperties = new List<ItemFloatProperty>()
                {
                    TotalHealingAmount,
                    Duration
                }
            };

            return healWeaponData;
        }

        public void Upgrade(int level)
        {
            throw new System.NotImplementedException();
        }
    }
}