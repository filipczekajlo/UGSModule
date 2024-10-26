using System.Collections.Generic;

namespace InventoryDTO
{
    public static class StringConsts
    {
        // Element
        public const string AirElement = "Air";
        public const string EarthElement = "Earth";
        public const string FireElement = "Fire";
        public const string WaterElement = "Water";

        // Item Type // ToDo: "Type" is used in WeaponData. Avoid confusion!
        public const string BigBullet = "BigBulletWeapon";
        public const string Cone = "ConeWeapon";
        public const string Field = "FieldWeapon";
        public const string Ground = "GroundWeapon";
        public const string Heal = "HealWeapon";
        public const string SmallBullet = "SmallBulletWeapon";
        public const string Sprint = "SprintWeapon";
        public const string Wall = "WallWeapon";

        public static List<ItemDataHolder> GetAllAttackItems()
        {
            var tempList = new List<ItemDataHolder>();

            tempList.Add(new ItemDataHolder(BigBullet, AirElement));
            tempList.Add(new ItemDataHolder(BigBullet, EarthElement));
            tempList.Add(new ItemDataHolder(BigBullet, FireElement));
            tempList.Add(new ItemDataHolder(BigBullet, WaterElement));

            tempList.Add(new ItemDataHolder(Cone, AirElement));
            tempList.Add(new ItemDataHolder(Cone, EarthElement));
            tempList.Add(new ItemDataHolder(Cone, FireElement));
            tempList.Add(new ItemDataHolder(Cone, WaterElement));

            tempList.Add(new ItemDataHolder(Field, AirElement));
            tempList.Add(new ItemDataHolder(Field, EarthElement));
            tempList.Add(new ItemDataHolder(Field, FireElement));
            tempList.Add(new ItemDataHolder(Field, WaterElement));

            tempList.Add(new ItemDataHolder(Ground, AirElement));
            tempList.Add(new ItemDataHolder(Ground, EarthElement));
            tempList.Add(new ItemDataHolder(Ground, FireElement));
            tempList.Add(new ItemDataHolder(Ground, WaterElement));

            tempList.Add(new ItemDataHolder(Heal, AirElement));
            tempList.Add(new ItemDataHolder(Heal, EarthElement));
            tempList.Add(new ItemDataHolder(Heal, FireElement));
            tempList.Add(new ItemDataHolder(Heal, WaterElement));

            tempList.Add(new ItemDataHolder(SmallBullet, AirElement));
            tempList.Add(new ItemDataHolder(SmallBullet, EarthElement));
            tempList.Add(new ItemDataHolder(SmallBullet, FireElement));
            tempList.Add(new ItemDataHolder(SmallBullet, WaterElement));

            tempList.Add(new ItemDataHolder(Sprint, AirElement));
            tempList.Add(new ItemDataHolder(Sprint, EarthElement));
            tempList.Add(new ItemDataHolder(Sprint, FireElement));
            tempList.Add(new ItemDataHolder(Sprint, WaterElement));

            tempList.Add(new ItemDataHolder(Wall, AirElement));
            tempList.Add(new ItemDataHolder(Wall, EarthElement));
            tempList.Add(new ItemDataHolder(Wall, FireElement));
            tempList.Add(new ItemDataHolder(Wall, WaterElement));

            return tempList;
        }

        public static List<string> GetAllAttackItemIDs()
        {
            var tempList = new List<string>();

            tempList.Add(BigBullet + AirElement);
            tempList.Add(BigBullet + EarthElement);
            tempList.Add(BigBullet + FireElement);
            tempList.Add(BigBullet + WaterElement);

            tempList.Add(Cone + AirElement);
            tempList.Add(Cone + EarthElement);
            tempList.Add(Cone + FireElement);
            tempList.Add(Cone + WaterElement);

            tempList.Add(Field + AirElement);
            tempList.Add(Field + EarthElement);
            tempList.Add(Field + FireElement);
            tempList.Add(Field + WaterElement);

            tempList.Add(Ground + AirElement);
            tempList.Add(Ground + EarthElement);
            tempList.Add(Ground + FireElement);
            tempList.Add(Ground + WaterElement);

            tempList.Add(Heal + AirElement);
            tempList.Add(Heal + EarthElement);
            tempList.Add(Heal + FireElement);
            tempList.Add(Heal + WaterElement);

            tempList.Add(SmallBullet + AirElement);
            tempList.Add(SmallBullet + EarthElement);
            tempList.Add(SmallBullet + FireElement);
            tempList.Add(SmallBullet + WaterElement);

            tempList.Add(Sprint + AirElement);
            tempList.Add(Sprint + EarthElement);
            tempList.Add(Sprint + FireElement);
            tempList.Add(Sprint + WaterElement);

            tempList.Add(Wall + AirElement);
            tempList.Add(Wall + EarthElement);
            tempList.Add(Wall + FireElement);
            tempList.Add(Wall + WaterElement);

            return tempList;
        }
    }
}