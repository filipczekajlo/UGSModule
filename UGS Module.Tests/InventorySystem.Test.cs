using System.Text.Json.Serialization;
using FluentAssertions;
using InventoryDTO;
using InventoryDTO.Weapons;
using Newtonsoft.Json;

namespace UGS_Module.Tests;

public class InventorySystem_Test
{
    [Fact]
    public async Task DownloadDeafultAgentFile_Test()
    {
        var txt = await InventorySystem.DownloadFile(
            "https://raw.githubusercontent.com/filipczekajlo/ALOTA-public/main/DefaultAgentAir.txt");
        txt.Should().NotBeNullOrEmpty();
        txt.Should().Contain("Air Big Bullet");
    }

    [Fact]
    public async Task DownloadDefaultAgentFileAndDeserialize_Test()
    {
        var txt = await InventorySystem.DownloadFile(
            "https://raw.githubusercontent.com/filipczekajlo/ALOTA-public/main/DefaultAgentAir.txt");

        AgentData agentData = JsonConvert.DeserializeObject<AgentData>(txt);

        agentData.Should().NotBeNull();
        // agentData.Inventories.EquippedAttacks.Slots[0].ItemData.Name.Should().Be("Air Big Bullet");
        // agentData.Inventories.EquippedAttacks.Slots[1].ItemData.Name.Should().Be("Air Cone");
        txt.Should().NotBeNullOrEmpty();
        txt.Should().Contain("Air Big Bullet");
    }

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

    // [Fact]
    // public void CreateDefaultAgent_Test()
    // {
    //     var inventorySystem = new InventorySystem();
    //     var defaultAgent = inventorySystem.CreateDefaultAgent();
    //
    //     // Assert
    //     inventorySystem.Should().NotBeNull();
    //     defaultAgent.Inventories.EquippedAttacks.Slots.Should().NotBeNull();
    //     // defaultAgent.Inventories.EquippedAttacks.Slots[0].ItemData.Id.Should();
    // }

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

    // [Fact]
    // public void DeserializeTest()
    // {
    //     string package = @"{""ItemType"":""BigBulletWeapon"",""Id"":""BigBullet"",""Name"":""Big Bullet"",""TotalDamage"":20,""ChiCost"":40}";
    //     var settings = new JsonSerializerSettings();
    //     settings.Converters.Add(new ItemDataJsonConverter());
    //     
    //     var result = JsonConvert.DeserializeObject<ItemData>(package, settings);
    //     
    //     result.Should().NotBeNull();
    //     result.ItemType.Should().Be(StringConsts.BigBullet);
    //     result.Id.Should().Be("BigBulletWeapon");
    //     result.Name.Should().Be("Big Bullet");
    //
    // }
}