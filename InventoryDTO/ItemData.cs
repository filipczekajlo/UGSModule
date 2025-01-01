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
        }


        public string ItemType { get; set; } // Type discriminator for json
        public string Id { get; set; }
        public string Element { get; set; }
        public string Name { get; set; }
        
        public List<ItemFloatProperty> GeneralFloatProperties { get; set; } = new List<ItemFloatProperty>();
        public List<ItemFloatProperty> SpecificProperties { get; set; } = new List<ItemFloatProperty>();


    }
}