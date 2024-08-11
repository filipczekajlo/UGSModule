using System;

namespace InventoryDTO;


public abstract class ItemData
{
    public ItemData()
    {
        Id = "";
        TotalDamage = 0;
        ChiCost = 0;
        Name = "";
        DeleteMe = 0;
    }
    
    public int DeleteMe { get; set; }
    public string Id { get; set; } 
    public string Name { get; set; } 
    public int TotalDamage { get; set; }
    public int ChiCost { get; set; }
}