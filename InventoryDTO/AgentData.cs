namespace InventoryDTO
{
    public class AgentData
    {
        public AgentData()
        {
            Inventories = new Inventories();
            LevelData = new LevelData();
            // Level = 1;
        }

        public Inventories Inventories { get; set; }

        public LevelData LevelData { get; set; }

        // public int Level { get; set; } = 1;
        // public double TotalXP { get; set; } = 0;
        // public double XPToNextLevel { get; set; }
        // public double CurrentLevelXP { get; set; }
    }
}