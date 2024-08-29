
namespace InventoryDTO.Weapons;

public class ConeweaponData : WeaponData, ICreateDefaultValues
{
    public ItemData CreateDefaultValues(string itemType, string element)
    {
        ConeweaponData coneweaponData = new ConeweaponData
        {
            ItemType = itemType,
            Id = itemType + element,
            Name = "Cone Weapon",
            TotalDamage = 15,
            ChiCost = 30
        };

        return coneweaponData;
    }
}