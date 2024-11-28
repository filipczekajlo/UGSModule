using System.Collections.Generic;

namespace InventoryDTO.Weapons
{
    public class ConeweaponData : WeaponData, ICreateDefaultValues
    {
        public ItemFloatProperty Distance { get; set; } = new ItemFloatProperty(StringConsts.Distance, 4, 8, 1, 3);

        public ItemData CreateDefaultValues(string itemType, string element)
        {
            ConeweaponData coneweaponData = new ConeweaponData
            {
                Id = itemType + element,
                ItemType = itemType,
                Element = element,
                Name = "Cone Weapon",
                
                TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 16, 160, 6, 1),
                ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 25),
                CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 3, 2f, 0.2f, 2),
                DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 0.5f),
                
                SpecificProperties = new List<ItemFloatProperty>()
                {
                    Distance
                }

            };
            
            return coneweaponData;
        }
    }
}