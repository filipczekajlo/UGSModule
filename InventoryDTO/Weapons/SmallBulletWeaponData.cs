using System.Collections.Generic;

namespace InventoryDTO.Weapons
{
    public class SmallBulletWeaponData : WeaponData, ICreateDefaultValues, IUpgradeable
    {
        public SmallBulletWeaponData()
        {
            
        }
        public SmallBulletWeaponData(string itemType, string element)
        {
            Id = itemType + element;
            ItemType = itemType;
            Element = element;
            Name = "Hitscan Weapon";
                
            TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 2, 12, 1f, 1);
            ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 8);
            CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 2);
            DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 4f);

            GeneralProperties = CreateGeneralProperties();
            
        }
        
        // public ItemData CreateDefaultValues(string itemType, string element)
        // {
        //     SmallBulletWeaponData smallBulletWeaponData = new SmallBulletWeaponData
        //     {
        //         Id = itemType + element,
        //         ItemType = itemType,
        //         Element = element,
        //         Name = "Hitscan Weapon",
        //         
        //         TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 2, 12, 1f, 1),
        //         ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 8),
        //         CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 2, 1.5f, 0.1f, 2),
        //         DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 4f),
        //
        //     };
        //
        //     return smallBulletWeaponData;
        // }

        public void SetLevel(int level)
        {
            throw new System.NotImplementedException();
        }
    }
}