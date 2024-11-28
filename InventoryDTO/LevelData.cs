using System;
using Newtonsoft.Json;

namespace InventoryDTO
{
    public class LevelData
    {
        public int Level { get; set; } = 0;
        public double TotalXP { get; set; } = 0;
        public double CurrentLevelXP { get; set; } = 0;
        public double XPToNextLevel { get; set; } = 0;
        
        // Upgrading data        
        public double XPForFirstLevel { get; } = 100;
        public double Exponent { get; } = 1f;
        public int MaxLevel { get; } = 100;


        [JsonIgnore] public Action<InventorySlot> LevelUpped = delegate { };
    }
}