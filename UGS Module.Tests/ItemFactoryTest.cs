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
        var sprintWeaponData = itemFactory.CreateDefaultItem("Sprint");
        var healWeaponData = itemFactory.CreateDefaultItem("Heal");
        
        var castedSprintWeapon = sprintWeaponData as SprintWeaponData;
        var castedHealWeapon = healWeaponData as HealWeaponData;
        
        sprintWeaponData.Should().NotBeNull();
        sprintWeaponData.Type.Should().Be("SprintWeapon");
        castedSprintWeapon.Should().NotBeNull();
        
        
        healWeaponData.Should().NotBeNull();
        healWeaponData.Type.Should().Be("HealWeapon");
        castedHealWeapon.Should().NotBeNull();

}
}