
namespace InventoryDTO.Weapons;

public class HealWeaponData : WeaponData, ICreateDefaultValues, IUpgradeable
{
    public ItemData CreateDefaultValues(string itemType, string element)
    {
        HealWeaponData healWeaponData = new HealWeaponData
        {
            ItemType = itemType,
            Id = itemType + element,
            Name = "Heal Weapon",
            TotalDamage = 0,
            ChiCost = 20
        };

        return healWeaponData;
    }

    public void Upgrade(int level)
    {
        throw new System.NotImplementedException();
    }
}