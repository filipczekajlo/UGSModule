using System.Collections.Generic;

namespace InventoryDTO;

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
    }

    public void SwapItems(InventorySlot slot1, InventorySlot slot2, bool forceSwap = false)
    {
        // if can place item 1 in item2slot and other way around
           if (forceSwap)
           {
               var temp = new InventorySlot(slot2.item, slot2.amount);
               slot2.UpdateSlot(slot1.item, slot1.amount);
               slot1.UpdateSlot(temp.item, temp.amount);
           }
           else if(slot2.CanPlaceInSlot(slot1.item) && slot1.CanPlaceInSlot(slot2.item))
           {
               var temp = new InventorySlot(slot2.item, slot2.amount);
               slot2.UpdateSlot(slot1.item, slot1.amount);
               slot1.UpdateSlot(temp.item, temp.amount);
           }
    }

}