using System;
using Newtonsoft.Json;

namespace InventoryDTO
{
    public class ItemFloatProperty
    {
        [JsonProperty]
        public string Name { get; set; }
        /// <summary>
        /// The value the property has at the current level
        /// </summary>
        [JsonProperty]
        public float CurrentValue { get; private set; }
        /// <summary>
        /// The value the property will have at the next level. used for convenience
        /// </summary>
        [JsonProperty]
        public float NextValue { get; private set; }
        [JsonProperty]
        public float WorstValue { get; private set; }
        [JsonProperty]
        public float BestValue { get; private set; }
        
        [JsonProperty]
        private int Level { get; set; }
        [JsonProperty]
        private float InitialValue { get; set; }

        [JsonProperty]
        // After each level should this property be upgraded
        private int UpgradeInterval { get; set; }
        [JsonProperty]
        private float UpgradeAmount { get; set; }
        
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