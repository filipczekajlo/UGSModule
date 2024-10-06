
namespace InventoryDTO.Weapons;

public class ThrowableWeaponData : WeaponData, ICreateDefaultValues, IUpgradeable
{
    public ThrowableWeaponData()
    {
        
    }
    public ItemData CreateDefaultValues(string itemType, string element)
    {
        ThrowableWeaponData throwableWeaponData = new ThrowableWeaponData
        {
            Id = itemType + element,
            ItemType = itemType,
            Element = element,
            Name = "Big Bullet",
            TotalDamage = 20,
            ChiCost = 40
        };

        return throwableWeaponData;
    }

    public void Upgrade(int level)
    {
        throw new System.NotImplementedException();
    }
}