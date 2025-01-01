using System.Collections.Generic;

namespace InventoryDTO.Weapons
{
    public class SmallBulletWeaponData : WeaponData, IUpgradeable
    {
        public SmallBulletWeaponData()
        {
            
        }
        public SmallBulletWeaponData(string itemType, string element)
        {
            Id = itemType + element;
            ItemType = itemType;
            Element = element;
            Name = "Small Bullet";
                
            TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 2, 12, 1f, 1);
            ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 8);
            CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 2);
            DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 4f);

            CreateGeneralProperties();
            
        }
    }
}