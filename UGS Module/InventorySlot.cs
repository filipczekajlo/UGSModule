namespace UGS_Module;

public class InventorySlot
{
    public InventorySlot()
    {
        item = new Item();
        amount = 1;
    }
    public Item item;
    public int amount;
}