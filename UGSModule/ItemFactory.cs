using InventoryDTO;
using InventoryDTO.Weapons;

namespace UGS_Module;

public class ItemFactory
{
    private readonly Dictionary<string, Func<string, string>> _defaultItemCreators;
    
    public ItemFactory()
    {
        _defaultItemCreators = new Dictionary<string, Func<string, string>>
        {
            { "SprintWeapon", data => SprintWeaponData.CreateDefaultValues(data) },
            { "HealWeapon", data => HealWeaponData.CreateDefaultValues(data) },
            { "GroundWeapon", data => GroundWeaponData.CreateDefaultValues(data) }
        };
    }
    
    public string CreateDefaultItem(string itemType)
    {
        if (_defaultItemCreators.TryGetValue(itemType, out var creator))
        {
            return creator(itemType);
        }
        
        return "";
    }
}