﻿using System.Collections.Generic;

namespace InventoryDTO.Weapons
{
    public class FieldWeaponData : WeaponData, IUpgradeable
    {
        public FieldWeaponData()
        {
        }
        public ItemFloatProperty Duration { get; set; } = new ItemFloatProperty(StringConsts.Duration, 4);

        public FieldWeaponData(string itemType, string element)
        {
            Id = itemType + element;
            ItemType = itemType;
            Element = element;
            Name = "Wall Weapon";
               
            TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 24, 200, 12, 1);
            ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 25);
            CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 6);
            DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 4f);

            CreateGeneralProperties();

            SpecificProperties = new List<ItemFloatProperty>()
            {
                Duration
            };
        }
        // public ItemData CreateDefaultValues(string itemType, string element)
        // {
        //     FieldWeaponData fieldWeaponData = new FieldWeaponData
        //     {
        //         Id = itemType + element,
        //         ItemType = itemType,
        //         Element = element,
        //         Name = "Wall Weapon",
        //         
        //         TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 24, 200, 12, 1),
        //         ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 25),
        //         CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 6, 5f, 0.2f, 4),
        //         DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 4f),
        //         
        //         SpecificProperties = new List<ItemFloatProperty>()
        //         {
        //             Duration
        //         }
        //
        //     };
        //
        //     return fieldWeaponData;
        // }

        
    }
}