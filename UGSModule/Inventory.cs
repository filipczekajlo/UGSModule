namespace UGS_Module;

public class Inventory
{
    public List<InventorySlot> Slots { get; set; }
    public Inventory()
    {
        Slots = new List<InventorySlot>();
    }

    public Inventory(bool lol)
    {
        Slots = new List<InventorySlot>();
       Slots.Add(new InventorySlot());
       Slots.Add(new InventorySlot());
       
    }

}