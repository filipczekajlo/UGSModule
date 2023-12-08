using System;

namespace InventoryDTO;

public class Item
{
    public Item()
    {
        Id = -1;
        TotalDamage = 0;
        ChiCost = 0;
        Name = "";
    }
    public int Id { get; set; } 
    public string Name { get; set; } 
    public int TotalDamage { get; set; }
    public int ChiCost { get; set; }
}