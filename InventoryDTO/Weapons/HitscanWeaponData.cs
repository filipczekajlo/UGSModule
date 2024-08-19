
namespace InventoryDTO.Weapons;

public class HitscanWeaponData : WeaponData, ICreateDefaultValues
{
    public static ItemData CreateDefaultValues(string id)
    {
        HitscanWeaponData hitscanWeaponData = new HitscanWeaponData
        {
            Type = "HitScanWeapon",
            Id = id,
            Name = "Hitscan Weapon",
            TotalDamage = 10,
            ChiCost = 10
        };

        return hitscanWeaponData;
    }
}