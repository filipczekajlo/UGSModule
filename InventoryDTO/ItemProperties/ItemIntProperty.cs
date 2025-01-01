using System;
using Newtonsoft.Json;

namespace InventoryDTO
{
    public class ItemIntProperty
    {
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// The value the property has at the current level
        /// </summary>
        [JsonProperty]
        public int CurrentValue { get; set; }

        /// <summary>
        /// The value the property will have at the next level. used for convenience
        /// </summary>
        [JsonProperty]
        public int NextValue { get; set; }
        [JsonProperty]
        public int WorstValue { get; private set; }
        [JsonProperty]
        public int BestValue { get; private set; }

        [JsonProperty]
        private int Level { get; set; }
        [JsonProperty]
        private int InitialValue { get; set; }

        // After each level should this property be upgraded
        [JsonProperty]
        private int UpgradeInterval { get; set; }
        [JsonProperty]
        private int UpgradeAmount { get; set; }
        
        public ItemIntProperty(string name, int worstValue, int bestValue, int upgradeAmount, int upgradeInterval)
        {
            Name = name;
            Level = worstValue;
            WorstValue = worstValue;
            BestValue = bestValue;
            CurrentValue = worstValue;
            InitialValue = worstValue;
            UpgradeAmount = upgradeAmount;
            UpgradeInterval = upgradeInterval;
            NextValue = CalculateValueForLevel(Level + 1);
        }

        // Using this constructor for properties that can't be upgraded
        public ItemIntProperty(string name, int value)
        {
            Name = name;
            Level = 1;
            WorstValue = -1;
            BestValue = -1;
            CurrentValue = value;
            NextValue = value;
        }

        public ItemIntProperty()
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

        public int CalculateValueForLevel(int level)
        {
            if (UpgradeInterval > 0)
            {
                int totalUpgrades = level / UpgradeInterval;
                // int totalUpgrades = (level - 1) / UpgradeInterval;
                // return Math.Min(InitialValue + totalUpgrades * UpgradeAmount, BestValue);
                return Math.Min(InitialValue + totalUpgrades * UpgradeAmount, BestValue);
            }

            return CurrentValue;
        }
    }
}