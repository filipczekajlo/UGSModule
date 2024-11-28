namespace InventoryDTO
{
    public class ItemFloatProperty 
    {
        public string Name { get; set; }
        public float WorstValue { get; set; }
        public float BestValue { get; set; }
        public float CurrentValue { get; set; }
        public float InitialValue { get; set; }

        // After each level should this property be upgraded
        public int UpgradeInterval { get; set; }
        public float UpgradeAmount { get; set; }

        public ItemFloatProperty(string name, float worstValue, float bestValue, float upgradeAmount, int upgradeInterval)
        {
            Name = name;
            WorstValue = worstValue;
            BestValue = bestValue;
            CurrentValue = worstValue;
            UpgradeAmount = upgradeAmount;
            UpgradeInterval = upgradeInterval;
        }
        
        // Using this constructor for properties that can't be upgraded
        public ItemFloatProperty(string name, float value)
        {
            Name = name;
            WorstValue = -1;
            BestValue = -1;
            CurrentValue = value;
        }
        
        public ItemFloatProperty()
        {
            Name = "";
            WorstValue = 0;
            BestValue = 0;
            CurrentValue = 0;
        }
        
    }
}