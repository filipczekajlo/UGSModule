using System.Text.Json;

namespace InventoryDTO.Weapons;

public class SprintWeaponData : WeaponData, ICreateDefaultValues
{

    public static string CreateDefaultValues(string id)
    {
        SprintWeaponData sprintWeaponData = new SprintWeaponData
        {
            Id = id,
            Name = "Sprint Weapon",
            TotalDamage = 20,
            ChiCost = 50
        };

        return JsonSerializer.Serialize(sprintWeaponData);

    }
}