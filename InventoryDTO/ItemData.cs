using System.Collections.Generic;
using Newtonsoft.Json;

namespace InventoryDTO
{
    public abstract class ItemData
    {
        public ItemData()
        {
            Id = "";
            Name = "";
            ItemType = "";
            Element = "";
            LevelData = new LevelData();
        }


        public LevelData LevelData { get; set; }
        public string ItemType { get; set; } // Type discriminator for json
        public string Id { get; set; }
        public string Element { get; set; }
        public string Name { get; set; }
        
        public List<ItemFloatProperty> SpecificProperties { get; set; } = new List<ItemFloatProperty>();


    }
}