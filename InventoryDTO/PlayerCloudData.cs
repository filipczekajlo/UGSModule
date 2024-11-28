using System;
using Newtonsoft.Json;

namespace InventoryDTO
{
    public class PlayerCloudData
    {
        // The key to the current Agent.

        private string currentAgentKey;

        public string CurrentAgentKey
        {
            get
            {
                return currentAgentKey;
                ;
            }
            set
            {
                currentAgentKey = value;
                AgentSelected.Invoke(CurrentAgentKey);
            }
        }

        public int Level { get; set; }

        public int ExperiencePoints { get; set; }
        public int Coins { get; set; }

        [JsonIgnore] public Action<string> AgentSelected = delegate { };


        public PlayerCloudData()
        {
            CurrentAgentKey = "Air";
            Level = 1;
            ExperiencePoints = 0;
        }
    }
}