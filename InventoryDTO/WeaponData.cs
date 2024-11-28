using System;
using System.Collections.Generic;

namespace InventoryDTO
{
    public class WeaponData : ItemData, IUpgradeable
    {
        public ItemFloatProperty TotalDamage { get; protected set; } = new ItemFloatProperty(StringConsts.TotalDamage, 0, 100, 1, 10);
        public ItemFloatProperty ChiCost { get; protected set; } = new ItemFloatProperty(StringConsts.ChiCost, 10, 20, 1, 5);
        public ItemFloatProperty CooldownTime { get; protected set; } = new ItemFloatProperty(StringConsts.CooldownTime, 0.5f,5f, 0.1f, 1);
        public ItemFloatProperty DisableMovementDuration { get; protected set; } = new ItemFloatProperty(StringConsts.CooldownTime, 0.5f);

        
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        
        public WeaponData()
        {
        }
        public virtual void Upgrade(int level = 0)
        {
            if(level == 0)
            {
                level = Level + 1;
            }
            
            Level = level;
            foreach (var prop in SpecificProperties)
            {
                if (prop.UpgradeInterval > 0)
                {
                    int totalUpgrades = Level / prop.UpgradeInterval;
                    prop.CurrentValue = Math.Min(prop.InitialValue + totalUpgrades * prop.UpgradeAmount, prop.BestValue);
                }
            }
        }
        

    }
}