using System.Collections.Generic;

namespace InventoryDTO.Weapons
{
    public class HealWeaponData : WeaponData, IUpgradeable
    {
        public ItemFloatProperty TotalHealingAmount { get; set; } = new ItemFloatProperty(StringConsts.TotalHealAmount, 18, 60, 4, 1);
        public ItemFloatProperty Duration { get; set; } = new ItemFloatProperty(StringConsts.Duration, 8, 20, 1, 1);
        
        public HealWeaponData()
        {
        }
        public HealWeaponData(string itemType, string element)
        {
            Id = itemType + element;
            ItemType = itemType;
            Element = element;
            Name = "Heal Weapon";
                
            TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 0, 0, 0, 0);
            ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 10);
            CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 0.5f);
            DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 0.5f);

            CreateGeneralProperties();

            SpecificProperties = new List<ItemFloatProperty>()
            {
                TotalHealingAmount,
                Duration
            };
        }
    }
}