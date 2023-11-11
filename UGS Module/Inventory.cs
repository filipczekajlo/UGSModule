namespace UGS_Module;

public class Inventory
{
    public Inventory()
    {
        Slots = new List<InventorySlot>();
    }

    public Inventory(bool lol)
    {
       Slots =  new List<InventorySlot>();
       Slots.Add(new InventorySlot());
       Slots.Add(new InventorySlot());
    }
    public List<InventorySlot> Slots = new List<InventorySlot>();

}