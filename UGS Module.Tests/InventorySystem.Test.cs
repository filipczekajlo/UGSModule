using System.Text.Json.Serialization;
using FluentAssertions;
using InventoryDTO;
using InventoryDTO.Weapons;
using Newtonsoft.Json;

namespace UGS_Module.Tests;

public class InventorySystem_Test
{
   
    [Fact]
    public async Task CreatePlayerCloudDataAndSerialize()
    {
        PlayerCloudData data = new PlayerCloudData();

        var serialized = JsonConvert.SerializeObject(data);


        serialized.Should().NotBeNull();
    }


    [Fact]
    public void SerializeInventorySlot_Test()
    {
        var inventorySlot = new InventorySlot(new WeaponData());

        var serialized = JsonConvert.SerializeObject(inventorySlot);
        serialized.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public void CreateDefaultInventoriesTest()
    {
        var inventorySystem = new InventorySystem();
        var inventories = inventorySystem.CreateDefaultInventories(StringConsts.AirElement);

        // Assert
        inventorySystem.Should().NotBeNull();
        inventories.Should().NotBeNull();
        inventories.EquippedAttacks.Slots.Should().NotBeNull();
        inventories.EquippedAttacks.Slots.Count.Should().Be(4);
        inventories.UnequippedAttacks.Slots.Count.Should().Be(4);
        inventories.EquippedAttacks.Slots[0].ItemDataID.Should().Be(StringConsts.BigBullet + StringConsts.AirElement);
    }
    
    // [Fact]
    // public void 
   

    [Fact]
    public void DeserializeItemDataTest()
    {
        var itemFactory = new ItemFactory();
        var newItem = itemFactory.CreateDefaultItem(StringConsts.BigBullet, StringConsts.AirElement);

        var settings = new JsonSerializerSettings();
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        settings.Converters.Add(new ItemDataJsonConverter());
        
            var serialized = JsonConvert.SerializeObject(newItem);

            // Deserialize using the custom converter
            var result = JsonConvert.DeserializeObject<ItemData>(serialized, settings);
            
            result.Should().NotBeNull();
            result.ItemType.Should().Be(StringConsts.BigBullet);
            result.Id.Should().Be(StringConsts.BigBullet + StringConsts.AirElement);
            result.Name.Should().Be("Big Bullet");

    }
}