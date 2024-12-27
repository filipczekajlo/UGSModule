using System;

namespace InventoryDTO
{
    public class ItemFloatProperty 
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float WorstValue { get; set; }
        public float BestValue { get; set; }
        /// <summary>
        /// The value the property has at the current level
        /// </summary>
        public float CurrentValue { get; set; }
        /// <summary>
        /// The value the property will have at the next level. used for convenience
        /// </summary>
        public float NextValue { get; set; }
        public float InitialValue { get; set; }

        // After each level should this property be upgraded
        public int UpgradeInterval { get; set; }
        public float UpgradeAmount { get; set; }
        
        public ItemFloatProperty(string name, float worstValue, float bestValue, float upgradeAmount, int upgradeInterval)
        {
            Name = name;
            Level = 1;
            WorstValue = worstValue;    
            BestValue = bestValue;
            CurrentValue = worstValue;
            NextValue = CalculateValueForLevel(Level + 1);
            InitialValue = worstValue;
            UpgradeAmount = upgradeAmount;
            UpgradeInterval = upgradeInterval;
        }
        
        // Using this constructor for properties that can't be upgraded
        public ItemFloatProperty(string name, float value)
        {
            Name = name;
            Level = 1;
            WorstValue = -1;
            BestValue = -1;
            CurrentValue = value;
            NextValue = value;
        }
        
        public ItemFloatProperty()
        {
            Name = "";
            Level = 1;
            WorstValue = 0;
            BestValue = 0;
            CurrentValue = 0;
        }

        public void UpdateLevel(int level)
        {
            Level = level;
            CurrentValue = CalculateValueForLevel(level);
            NextValue = CalculateValueForLevel(level + 1);
        }

        public float CalculateValueForLevel(int level)
        {
            if (UpgradeInterval > 0)
            {
                int totalUpgrades = level / UpgradeInterval;
                return Math.Min(InitialValue + totalUpgrades * UpgradeAmount, BestValue);
            }

            return CurrentValue;
        }
    }
}