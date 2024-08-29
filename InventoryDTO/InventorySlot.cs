using System;
using Newtonsoft.Json;

namespace InventoryDTO;

public class InventorySlot
{
    // public ItemData ItemData { get; set; }
    
    public string ItemDataID { get; set; }
    
    public string ItemType { get; set; }
    
    public int amount { get; set; }
    
    [JsonIgnore]
    public Action<InventorySlot> OnAfterUpdate = delegate { };
    [JsonIgnore]
    public Action<InventorySlot> OnBeforeUpdate = delegate { };

    public InventorySlot()
    {
        
    }
    public InventorySlot(ItemData itemData, int amount = 1)
    {
        UpdateSlot(itemData.Id, itemData.ItemType, amount);
    }
    
    public InventorySlot(string itemDataID, string type, int amount = 1)
    {
        UpdateSlot(itemDataID, type, amount);
    }

    public bool IsFilled()
    {
        if (ItemDataID != "")
            return true;
        return false;
    }

    public void UpdateSlot(string itemDataID, string type, int _amount)
    {
        if (OnBeforeUpdate != null) 
            OnBeforeUpdate.Invoke(this);

        ItemDataID = itemDataID;
        ItemType = type;
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