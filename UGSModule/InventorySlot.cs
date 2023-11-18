namespace UGS_Module;

public class InventorySlot
{
    public InventorySlot()
    {
        item = new Item();
        amount = 1;
    }
    public Item item { get; set; }
    public int amount { get; set; }
}