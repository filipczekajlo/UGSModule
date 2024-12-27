namespace InventoryDTO.Weapons
{
    public class GroundWeaponData : WeaponData, ICreateDefaultValues, IUpgradeable
    {
        public ItemFloatProperty Radius { get; set; } = new ItemFloatProperty(StringConsts.Distance, 4, 8, 0.5f, 2);

        public GroundWeaponData()
        {
            
        }
        public GroundWeaponData(string itemType, string element)
        {
            Id = itemType + element;
            ItemType = itemType;
            Element = element;
            Name = "Ground Weapon";
                
            TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 22, 180, 10, 1);
            ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 18);
            CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 4);
            DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 4f);

            GeneralProperties = CreateGeneralProperties();

            SpecificProperties.Add(Radius);
        }
        
        // public ItemData CreateDefaultValues(string itemType, string element)
        // {
        //     GroundWeaponData groundWeaponData = new GroundWeaponData
        //     {
        //         Id = itemType + element,
        //         ItemType = itemType,
        //         Element = element,
        //         Name = "Ground Weapon",
        //         
        //         TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 22, 180, 10, 1),
        //         ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 18),
        //         CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 4, 3f, 0.2f, 4),
        //         DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 4f),
        //
        //     };
        //
        //     return groundWeaponData;
        // }

        public void SetLevel(int level)
        {
            throw new System.NotImplementedException();
        }
    }
}