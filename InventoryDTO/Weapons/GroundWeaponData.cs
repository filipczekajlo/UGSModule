
namespace InventoryDTO.Weapons;

public class GroundWeaponData : WeaponData, ICreateDefaultValues
{
    public ItemData CreateDefaultValues(string itemType, string element)
    {
        GroundWeaponData groundWeaponData = new GroundWeaponData
        {
            ItemType = itemType,
            Id = itemType + element,
            Name = "Ground Weapon",
            TotalDamage = 10,
            ChiCost = 20
        };

        return groundWeaponData;
    }
}