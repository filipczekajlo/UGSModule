using System;

namespace InventoryDTO;

public class InventorySlot
{
    public ItemData ItemData { get; set; }
    public int amount { get; set; }

    public Action<InventorySlot> OnAfterUpdate = delegate { };
    public Action<InventorySlot> OnBeforeUpdate = delegate { };

    public InventorySlot()
    {
        
    }
    public InventorySlot(ItemData itemData, int amount)
    {
        UpdateSlot(itemData, amount);
    }

    public InventorySlot(WeaponData itemData)
    {
        ItemData = itemData;
        amount = 1;
    }

    public bool IsFilled()
    {
        if (ItemData.Id != "")
            return true;
        return false;
    }

    public void UpdateSlot(ItemData itemData, int _amount)
    {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);

        ItemData = itemData;
        amount = _amount;

        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }

    // Checks if can place item from mouseDrag in a slot
    public bool CanPlaceInSlot(ItemData itemData)
    {
        if (itemData.Id != "")
        {
            return true;
        }

        return false;
    }
}