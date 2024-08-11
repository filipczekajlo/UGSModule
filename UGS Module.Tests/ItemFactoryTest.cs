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
        var sprintWeaponData = itemFactory.CreateDefaultItem("SprintWeapon");
        var healWeaponData = itemFactory.CreateDefaultItem("HealWeapon");
        
        var castedSprintWeapon = JsonSerializer.Deserialize<SprintWeaponData>(sprintWeaponData);
        var castedHealWeapon = JsonSerializer.Deserialize<SprintWeaponData>(healWeaponData);
        
        castedSprintWeapon.Should().NotBeNull();
        castedSprintWeapon.Id.Should().Be("SprintWeapon");
        
        castedHealWeapon.Should().NotBeNull();
        castedHealWeapon.Id.Should().Be("HealWeapon");
    }

}