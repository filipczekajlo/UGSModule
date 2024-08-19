using InventoryDTO;
using InventoryDTO.Weapons;

namespace UGS_Module;

public class ItemFactory
{
    private readonly Dictionary<string, Func<string, ItemData>> _defaultItemCreators;
    
    public ItemFactory()
    {
        _defaultItemCreators = new Dictionary<string, Func<string, ItemData>>
        {
            { "BigBullet", data => ThrowableWeaponData.CreateDefaultValues(data) },
            { "Cone", data => ConeweaponData.CreateDefaultValues(data) },
            { "Field", data => DamageOverTimeData.CreateDefaultValues(data) },
            { "Ground", data => GroundWeaponData.CreateDefaultValues(data) },
            { "Heal", data => HealWeaponData.CreateDefaultValues(data) },
            { "SmallBullet", data => HitscanWeaponData.CreateDefaultValues(data) },
            { "Sprint", data => SprintWeaponData.CreateDefaultValues(data) },
            { "Wall", data => DamageOverTimeData.CreateDefaultValues(data) },
        };
    }
    
    public ItemData CreateDefaultItem(string itemName)
    {
        if (_defaultItemCreators.TryGetValue(itemName, out var creator))
        {
            return creator(itemName);
        }
        
        throw new Exception("Could not create default item! Unknown item name: " + itemName);
        return null;
    }
}