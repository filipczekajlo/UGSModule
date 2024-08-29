using System.Text.Json;

namespace InventoryDTO.Weapons;

public class SprintWeaponData : WeaponData, ICreateDefaultValues
{

    public ItemData CreateDefaultValues(string itemType, string element)
    {
        SprintWeaponData sprintWeaponData = new SprintWeaponData
        {
            ItemType = itemType,
            Id = itemType + element,
            Name = "Sprint Weapon",
            TotalDamage = 20,
            ChiCost = 50
        };

        return sprintWeaponData;

    }
}