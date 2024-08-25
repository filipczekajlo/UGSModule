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

    [Fact]
    public void CreateDefaultAgent_Test()
    {
        var inventorySystem = new InventorySystem();
        var defaultAgent = inventorySystem.CreateDefaultAgent();

        // Assert
        inventorySystem.Should().NotBeNull();
        defaultAgent.Inventories.EquippedAttacks.Slots.Should().NotBeNull();
        // defaultAgent.Inventories.EquippedAttacks.Slots[0].ItemData.Id.Should();
    }

    [Fact]
    public void CreateDefaultInventoriesTest()
    {
        var inventorySystem = new InventorySystem();
        var inventories = inventorySystem.CreateDefaultInventories();

        // Assert
        inventorySystem.Should().NotBeNull();
        inventories.Should().NotBeNull();
        inventories.EquippedAttacks.Slots.Should().NotBeNull();
        inventories.EquippedAttacks.Slots.Count.Should().Be(4);
        inventories.UnequippedAttacks.Slots.Count.Should().Be(4);
    }

    [Fact]
    public void DeserializeItemDataTest()
    {
        var itemFactory = new ItemFactory();
        var newItem = itemFactory.CreateDefaultItem("BigBullet");
        Console.WriteLine($"Created item type: {newItem?.GetType().FullName ?? "null"}");

        var settings = new JsonSerializerSettings();
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        settings.Converters.Add(new ItemDataJsonConverter());

        try
        {
            var serialized = JsonConvert.SerializeObject(newItem);
            Console.WriteLine($"Serialized: {serialized}");

            // Deserialize using the custom converter
            var result = JsonConvert.DeserializeObject<ItemData>(serialized, settings);
            Console.WriteLine($"Deserialized type: {result?.GetType().FullName ?? "null"}");

            // Rest of your assertions...
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex}");
            throw;
        }
    }
}