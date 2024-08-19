using Newtonsoft.Json;

namespace InventoryDTO;


public abstract class ItemData
{
    public ItemData()
    {
        Id = "";
        TotalDamage = 0;
        ChiCost = 0;
        Name = "";
        Type = "";
    }
    
    public string Type { get; set; } // Type discriminator
    public string Id { get; set; } 
    public string Name { get; set; } 
    public int TotalDamage { get; set; }
    public int ChiCost { get; set; }
}