
namespace InventoryDTO.Weapons;

public class HealWeaponData : WeaponData, ICreateDefaultValues
{
    public static ItemData CreateDefaultValues(string id)
    {
        HealWeaponData healWeaponData = new HealWeaponData
        {
            Type = "HealWeapon",
            Id = id,
            Name = "Heal Weapon",
            TotalDamage = 0,
            ChiCost = 20
        };

        return healWeaponData;
    }
}