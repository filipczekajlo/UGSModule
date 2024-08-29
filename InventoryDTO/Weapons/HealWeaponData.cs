
namespace InventoryDTO.Weapons;

public class HealWeaponData : WeaponData, ICreateDefaultValues
{
    public ItemData CreateDefaultValues(string itemType, string element)
    {
        HealWeaponData healWeaponData = new HealWeaponData
        {
            ItemType = itemType,
            Id = itemType + element,
            Name = "Heal Weapon",
            TotalDamage = 0,
            ChiCost = 20
        };

        return healWeaponData;
    }
}