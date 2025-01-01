using System;
using System.Collections.Generic;

namespace InventoryDTO
{
    public class WeaponData : ItemData
    {
        // public LevelData LevelData { get; set; }
        public ItemIntProperty Level { get; protected set; } = new ItemIntProperty(StringConsts.Level, 1, 50, 1, 1);
        public ItemFloatProperty TotalDamage { get; protected set; } = new ItemFloatProperty(StringConsts.TotalDamage, 0, 100, 1, 10);
        public ItemFloatProperty ChiCost { get; protected set; } = new ItemFloatProperty(StringConsts.ChiCost, 10, 20, 1, 5);
        public ItemFloatProperty CooldownTime { get; protected set; } = new ItemFloatProperty(StringConsts.CooldownTime, 0.5f,5f, 0.1f, 1);
        public ItemFloatProperty DisableMovementDuration { get; protected set; } = new ItemFloatProperty(StringConsts.CooldownTime, 0.5f);
        
        public WeaponData()
        {
        }

        public void Upgrade()
        {
            var newLevel = Level.CurrentValue + 1;
            SetLevel(newLevel);
        }

        public void Downgrade()
        {
            var newLevel = Level.CurrentValue - 1;
            SetLevel(newLevel);
        }
        public virtual void SetLevel(int newLevel)
        {
            Level.UpdateLevel(newLevel);
            
            foreach (var property in GeneralFloatProperties)
            {
                 property.UpdateLevel(newLevel);
            }
        
            foreach (var property in SpecificProperties)
            {
                 property.UpdateLevel(newLevel);
            }
        }
        
        public List<ItemFloatProperty> CreateGeneralProperties()
        {
            var props =   new List<ItemFloatProperty>()
            {
                TotalDamage,
                ChiCost,
                CooldownTime,
                DisableMovementDuration
            };

            GeneralFloatProperties.AddRange(props);
            return props;
        }
        
        

    }
}