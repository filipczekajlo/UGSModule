using System.Text.Json;

namespace InventoryDTO.Weapons;

public class SprintWeaponData : WeaponData, ICreateDefaultValues
{

    public static ItemData CreateDefaultValues(string id)
    {
        SprintWeaponData sprintWeaponData = new SprintWeaponData
        {
            Type = "SprintWeapon",
            Id = id,
            Name = "Sprint Weapon",
            TotalDamage = 20,
            ChiCost = 50
        };

        return sprintWeaponData;

    }
}