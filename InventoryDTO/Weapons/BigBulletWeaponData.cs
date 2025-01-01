namespace InventoryDTO.Weapons
{
    public class BigBulletWeaponData : WeaponData
    {
        public BigBulletWeaponData()
        {
        }
        public BigBulletWeaponData(string itemType, string element)
        {
            Id = itemType + element;
            ItemType = itemType;
            Element = element;
            Name = "Big Bullet";
                
            TotalDamage = new ItemFloatProperty(StringConsts.TotalDamage, 10, 120, 4, 1);
            ChiCost = new ItemFloatProperty(StringConsts.ChiCost, 16);
            CooldownTime = new ItemFloatProperty(StringConsts.CooldownTime, 4);
            DisableMovementDuration = new ItemFloatProperty(StringConsts.DisableMovementDuration, 1f);
            
            CreateGeneralProperties();

        }
    }
}