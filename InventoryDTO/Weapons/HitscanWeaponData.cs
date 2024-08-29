
namespace InventoryDTO.Weapons;

public class HitscanWeaponData : WeaponData, ICreateDefaultValues
{
    public ItemData CreateDefaultValues(string itemType, string element)
    {
        HitscanWeaponData hitscanWeaponData = new HitscanWeaponData
        {
            ItemType = itemType,
            Id = itemType + element,
            Name = "Hitscan Weapon",
            TotalDamage = 10,
            ChiCost = 10
        };

        return hitscanWeaponData;
    }
}