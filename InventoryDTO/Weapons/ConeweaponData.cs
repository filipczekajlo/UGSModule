
namespace InventoryDTO.Weapons;

public class ConeweaponData : WeaponData, ICreateDefaultValues
{
    public static ItemData CreateDefaultValues(string id)
    {
        ConeweaponData coneweaponData = new ConeweaponData
        {
            Type = "ConeWeapon",
            Id = id,
            Name = "Cone Weapon",
            TotalDamage = 15,
            ChiCost = 30
        };

        return coneweaponData;
    }
}