using System.Text.Json;
using FluentAssertions;
using InventoryDTO;
using InventoryDTO.Weapons;

namespace UGS_Module.Tests;

public class ItemFactoryTest
{
    [Fact]
    public async Task CreateDefaultItemTest()
    {

        var itemFactory = new ItemFactory();
        var sprintWeaponData = itemFactory.CreateDefaultItem(StringConsts.Sprint, StringConsts.AirElement);
        var healWeaponData = itemFactory.CreateDefaultItem(StringConsts.Heal, StringConsts.FireElement);
        
        var castedSprintWeapon = sprintWeaponData as SprintWeaponData;
        var castedHealWeapon = healWeaponData as HealWeaponData;
        
        sprintWeaponData.Should().NotBeNull();
        sprintWeaponData.ItemType.Should().Be("SprintWeapon");
        castedSprintWeapon.Should().NotBeNull();
        
        
        healWeaponData.Should().NotBeNull();
        healWeaponData.ItemType.Should().Be("HealWeapon");
        castedHealWeapon.Should().NotBeNull();

}
}