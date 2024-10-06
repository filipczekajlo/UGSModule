
namespace InventoryDTO.Weapons;

public class HitscanWeaponData : WeaponData, ICreateDefaultValues, IUpgradeable
{
    public ItemData CreateDefaultValues(string itemType, string element)
    {
        HitscanWeaponData hitscanWeaponData = new HitscanWeaponData
        {
            Id = itemType + element,
            ItemType = itemType,
            Element = element,
            Name = "Hitscan Weapon",
            TotalDamage = 10,
            ChiCost = 10
        };

        return hitscanWeaponData;
    }

    public void Upgrade(int level)
    {
        throw new System.NotImplementedException();
    }
}