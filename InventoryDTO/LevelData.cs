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

        [JsonIgnore] public Action<InventorySlot> LevelUpped = delegate { };
    }
}