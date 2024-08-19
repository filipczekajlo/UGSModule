using System;

namespace InventoryDTO;

public class InventorySlot
{
    // public ItemData ItemData { get; set; }
    
    public string ItemDataID { get; set; }
    public int amount { get; set; }

    public Action<InventorySlot> OnAfterUpdate = delegate { };
    public Action<InventorySlot> OnBeforeUpdate = delegate { };

    public InventorySlot()
    {
        
    }
    public InventorySlot(ItemData itemData, int amount = 1)
    {
        UpdateSlot(itemData.Id, amount);
    }
    
    public InventorySlot(string itemDataID, int amount = 1)
    {
        UpdateSlot(itemDataID, amount);
    }

    public bool IsFilled()
    {
        if (ItemDataID != "")
            return true;
        return false;
    }

    public void UpdateSlot(string itemDataID, int _amount)
    {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);

        ItemDataID = itemDataID;
        amount = _amount;

        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }
    
    public bool CanPlaceInSlot(string itemDataID)
    {
        if (itemDataID != "")
        {
            return true;
        }

        return false;
    }
}