using System.Text.Json;

namespace InventoryDTO.Weapons;

public class ConeweaponData : WeaponData, ICreateDefaultValues
{
    public static string CreateDefaultValues(string id)
    {
        ConeweaponData coneweaponData = new ConeweaponData();
        coneweaponData.Id = id;
        coneweaponData.Name = "Cone Weapon";
        coneweaponData.TotalDamage = 15;
        coneweaponData.ChiCost = 30;
        
        return JsonSerializer.Serialize(coneweaponData);
    }
}