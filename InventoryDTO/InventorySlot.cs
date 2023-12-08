using System;

namespace InventoryDTO;

public class InventorySlot
{
    public Item item { get; set; }
    public int amount { get; set; }

    public Action<InventorySlot> OnAfterUpdate = delegate { };
    public Action<InventorySlot> OnBeforeUpdate = delegate { };

    public InventorySlot()
    {
        
    }
    public InventorySlot(Item item, int amount)
    {
        UpdateSlot(item, amount);
    }

    public InventorySlot(Item item)
    {
        this.item = item;
        item = new Item();
        amount = 1;
    }

    public bool IsFilled()
    {
        if (item.Id >= 0)
            return true;
        return false;
    }

    public void UpdateSlot(Item _item, int _amount)
    {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);

        item = _item;
        amount = _amount;

        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }

    // Checks if can place item from mouseDrag in a slot
    public bool CanPlaceInSlot(Item item)
    {
        if (item.Id >= 0)
        {
            return true;
        }

        return false;
    }
}