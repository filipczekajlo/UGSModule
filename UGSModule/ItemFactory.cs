using InventoryDTO;
using InventoryDTO.Weapons;

namespace UGS_Module;

public class ItemFactory
{
    private readonly Dictionary<string, Func<string, string, ItemData>> _defaultItemCreators;
    
    public ItemFactory()
    {
        // Here: CreateDefaultValues can not be static! Otherwise Unity exceptions will be thrown when deserializing cloud code
        // var bigBulletWeaponData = new BigBulletWeaponData();
        // var coneweaponData = new ConeWeaponData();
        // var fieldWeaponData = new FieldWeaponData();
        // var groundWeaponData = new GroundWeaponData();
        // var healWeaponData = new HealWeaponData();
        // var smallBulletWeaponData = new SmallBulletWeaponData();
        // var sprintWeaponData = new SprintWeaponData();
        // var wallWeaponData = new WallWeaponData();
        
        _defaultItemCreators = new Dictionary<string, Func<string, string, ItemData>>
        {
            { StringConsts.BigBullet, (type, element) => new BigBulletWeaponData(type, element) },
            { StringConsts.Cone, (type, element) => new ConeWeaponData(type, element) },
            { StringConsts.Field, (type, element) => new FieldWeaponData(type, element) },
            { StringConsts.Ground, (type, element) => new GroundWeaponData(type, element) },
            { StringConsts.Heal, (type, element) => new HealWeaponData(type, element) },
            { StringConsts.SmallBullet, (type, element) => new SmallBulletWeaponData(type, element) },
            { StringConsts.Sprint, (type, element) => new SprintWeaponData(type, element) },
            { StringConsts.Wall, (type, element) => new WallWeaponData(type, element) },
        };
    }
    
    public ItemData CreateDefaultItem(string itemType, string element)
    {
        if (_defaultItemCreators.TryGetValue(itemType, out var creator))
        {
            return creator(itemType, element);
        }
        
        throw new Exception("Could not create default item! Unknown item name: " + itemType);
        return null;
    }
}