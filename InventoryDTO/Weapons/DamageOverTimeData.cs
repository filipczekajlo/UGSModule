using System.Text.Json;

namespace InventoryDTO.Weapons;

public class DamageOverTimeData : WeaponData, ICreateDefaultValues
{
    public static string CreateDefaultValues(string id)
    {
        DamageOverTimeData damageOverTimeData = new DamageOverTimeData
        {
            Id = id,
            Name = "Damage Over Time Weapon",
            TotalDamage = 5,
            ChiCost = 5
        };

        return JsonSerializer.Serialize(damageOverTimeData);
    }
}