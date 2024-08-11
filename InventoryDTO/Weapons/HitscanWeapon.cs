using System.Text.Json;

namespace InventoryDTO.Weapons;

public class HitscanWeapon : WeaponData, ICreateDefaultValues
{
    public static string CreateDefaultValues(string id)
    {
        HitscanWeapon hitscanWeapon = new HitscanWeapon
        {
            Id = id,
            Name = "Hitscan Weapon",
            TotalDamage = 10,
            ChiCost = 10
        };

        return JsonSerializer.Serialize(hitscanWeapon);
    }
}