
namespace InventoryDTO.Weapons;

public class ThrowableWeaponData : WeaponData, ICreateDefaultValues
{
    public ThrowableWeaponData()
    {
        
    }
    public static ItemData CreateDefaultValues(string id)
    {
        ThrowableWeaponData throwableWeaponData = new ThrowableWeaponData
        {
            Type = "ThrowableWeapon",
            Id = id,
            Name = "Big Bullet",
            TotalDamage = 20,
            ChiCost = 40
        };

        return throwableWeaponData;
    }
}