
namespace InventoryDTO.Weapons;

public class DamageOverTimeData : WeaponData, ICreateDefaultValues
{
    public static ItemData CreateDefaultValues(string id)
    {
        DamageOverTimeData damageOverTimeData = new DamageOverTimeData
        {
            Type = "DamageOverTimeWeapon",
            Id = id,
            Name = "Damage Over Time Weapon",
            TotalDamage = 5,
            ChiCost = 5
        };

        return damageOverTimeData;
    }
}