
namespace InventoryDTO.Weapons;

public class ConeweaponData : WeaponData, ICreateDefaultValues
{
    public ItemData CreateDefaultValues(string itemType, string element)
    {
        ConeweaponData coneweaponData = new ConeweaponData
        {
            Id = itemType + element,
            ItemType = itemType,
            Element = element,
            Name = "Cone Weapon",
            TotalDamage = 15,
            ChiCost = 30
        };

        return coneweaponData;
    }
}