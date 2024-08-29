
namespace InventoryDTO.Weapons;

public class DamageOverTimeData : WeaponData, ICreateDefaultValues
{
    public ItemData CreateDefaultValues(string itemType, string element)
    {
        DamageOverTimeData damageOverTimeData = new DamageOverTimeData
        {
            ItemType = itemType,
            Id = itemType + element,
            Name = "Damage Over Time Weapon",
            TotalDamage = 5,
            ChiCost = 5
        };

        return damageOverTimeData;
    }
}