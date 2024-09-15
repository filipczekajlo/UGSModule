﻿
namespace InventoryDTO.Weapons;

public class GroundWeaponData : WeaponData, ICreateDefaultValues, IUpgradeable
{
    public ItemData CreateDefaultValues(string itemType, string element)
    {
        GroundWeaponData groundWeaponData = new GroundWeaponData
        {
            ItemType = itemType,
            Id = itemType + element,
            Name = "Ground Weapon",
            TotalDamage = 10,
            ChiCost = 20
        };

        return groundWeaponData;
    }

    public void Upgrade(int level)
    {
        throw new System.NotImplementedException();
    }
}