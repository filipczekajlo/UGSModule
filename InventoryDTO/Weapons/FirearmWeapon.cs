using System.Text.Json;

namespace InventoryDTO.Weapons;

public class FirearmWeapon : WeaponData, ICreateDefaultValues
{
    public static string CreateDefaultValues(string id)
    {
        FirearmWeapon firearmWeapon = new FirearmWeapon
        {
            Id = id,
            Name = "Firearm Weapon",
            TotalDamage = 20,
            ChiCost = 40
        };

        return JsonSerializer.Serialize(firearmWeapon);
    }
}