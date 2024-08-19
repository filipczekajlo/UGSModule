
namespace InventoryDTO.Weapons;

public class GroundWeaponData : WeaponData, ICreateDefaultValues
{
    public static ItemData CreateDefaultValues(string id)
    {
        GroundWeaponData groundWeaponData = new GroundWeaponData
        {
            Type = "GroundWeapon",
            Id = id,
            Name = "Ground Weapon",
            TotalDamage = 10,
            ChiCost = 20
        };

        return groundWeaponData;
    }
}