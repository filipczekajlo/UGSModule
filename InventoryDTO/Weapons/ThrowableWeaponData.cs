
namespace InventoryDTO.Weapons;

public class ThrowableWeaponData : WeaponData, ICreateDefaultValues
{
    public ThrowableWeaponData()
    {
        
    }
    public ItemData CreateDefaultValues(string itemType, string element)
    {
        ThrowableWeaponData throwableWeaponData = new ThrowableWeaponData
        {
            ItemType = itemType,
            Id = itemType + element,
            Name = "Big Bullet",
            TotalDamage = 20,
            ChiCost = 40
        };

        return throwableWeaponData;
    }
}