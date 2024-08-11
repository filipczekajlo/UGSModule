using System.Text.Json;

namespace InventoryDTO.Weapons;

public class HealWeaponData : WeaponData, ICreateDefaultValues
{
    public static string CreateDefaultValues(string id)
    {
        HealWeaponData healWeaponData = new HealWeaponData
        {
            Id = id,
            Name = "Heal Weapon",
            TotalDamage = 0,
            ChiCost = 20
        };

        return JsonSerializer.Serialize(healWeaponData);
    }
}