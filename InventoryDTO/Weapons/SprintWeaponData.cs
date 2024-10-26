namespace InventoryDTO.Weapons
{
    public class SprintWeaponData : WeaponData, ICreateDefaultValues, IUpgradeable
    {
        public ItemData CreateDefaultValues(string itemType, string element)
        {
            SprintWeaponData sprintWeaponData = new SprintWeaponData
            {
                Id = itemType + element,
                ItemType = itemType,
                Element = element,
                Name = "Sprint Weapon",
                TotalDamage = 20,
                ChiCost = 50
            };

            return sprintWeaponData;
        }

        public void Upgrade(int level)
        {
            throw new System.NotImplementedException();
        }
    }
}