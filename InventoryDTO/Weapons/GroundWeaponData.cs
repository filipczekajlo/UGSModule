using System.Text.Json;

namespace InventoryDTO.Weapons;

public class GroundWeaponData : WeaponData, ICreateDefaultValues
{
    public static string CreateDefaultValues(string id)
    {
        GroundWeaponData groundWeaponData = new GroundWeaponData
        {
            Id = id,
            Name = "Ground Weapon",
            TotalDamage = 10,
            ChiCost = 20
        };

        return JsonSerializer.Serialize(groundWeaponData);
    }
}