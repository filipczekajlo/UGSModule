using System.Collections.Generic;

namespace InventoryDTO.Weapons
{
    public class ConeWeaponData : WeaponData
    {
        public ConeWeaponData()
        {
        }
        public ItemFloatProperty Distance { get; set; } = new ItemFloatProperty(StringConsts.Distance, 4, 8, 1, 3);
        
        public ConeWeaponData(string itemType, string element)
        {
            Id = itemType + element;
            ItemType = itemType;
            Element = element;
            Name = "Cone Weapon";

            TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 16, 160, 6, 1);
            ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 25);
            CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 3);
            DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 0.5f);

            CreateGeneralProperties();
            
            SpecificProperties = new List<ItemFloatProperty>()
            {
                Distance
            };
        }
        // public ItemData CreateDefaultValues(string itemType, string element)
        // {
        //     ConeWeaponData coneWeaponData = new ConeWeaponData
        //     {
        //         Id = itemType + element,
        //         ItemType = itemType,
        //         Element = element,
        //         Name = "Cone Weapon",
        //         
        //         TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 16, 160, 6, 1),
        //         ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 25),
        //         CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 3, 2f, 0.2f, 2),
        //         DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 0.5f),
        //         
        //         SpecificProperties = new List<ItemFloatProperty>()
        //         {
        //             Distance
        //         }
        //
        //     };
        //     
        //     GeneralProperties = SetGeneralProperties();
        //     
        //     return coneWeaponData;
        // }

       
    }
}