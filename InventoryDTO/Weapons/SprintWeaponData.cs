namespace InventoryDTO.Weapons
{
    public class SprintWeaponData : WeaponData
    {
        public SprintWeaponData()
        {
            
        }
        public SprintWeaponData(string itemType, string element)
        {
            Id = itemType + element;
            ItemType = itemType;
            Element = element;
            Name = "Sprint Weapon";
                
            TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 0);
            ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 20);
            CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 5);
            DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 0f);

            CreateGeneralProperties();

        }
    }
}