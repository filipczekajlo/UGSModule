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

        public void Upgrade()
        {
            Level++;
            SetLevel(Level);
        }

        public void Downgrade()
        {
            Level--;
            SetLevel(Level);
        }
        public virtual void SetLevel(int level)
        {
            if(level == 0)
            {
                level = Level + 1;
            }
            
            Level = level;
            
            foreach (var property in GeneralProperties)
            {
                
                 property.UpdateLevel(Level);
                // property.NextValue = property.SetValuesForLevel(Level + 1, MaxLevel);

                // if (prop.UpgradeInterval > 0)
                // {
                //     int totalUpgrades = Level / prop.UpgradeInterval;
                //     prop.CurrentValue = Math.Min(prop.InitialValue + totalUpgrades * prop.UpgradeAmount, prop.BestValue);
                // }
            }
        
            foreach (var property in SpecificProperties)
            {
                 property.UpdateLevel(Level);
                // property.NextValue = property.SetValuesForLevel(Level + 1, MaxLevel);
                // if (property.UpgradeInterval > 0)
                // {
                //     int totalUpgrades = Level / property.UpgradeInterval;
                //     property.CurrentValue = Math.Min(property.InitialValue + totalUpgrades * property.UpgradeAmount, property.BestValue);
                // }
            }
        }
        
        public  List<ItemFloatProperty> CreateGeneralProperties()
        {
            return new List<ItemFloatProperty>()
            {
                TotalDamage,
                ChiCost,
                CooldownTime,
                DisableMovementDuration
            };
        }
        
        

    }
}