﻿using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using InventoryDTO;

namespace UGS_Module.Tests;

public class InventorySystem_Test
{

    [Fact]
    public async Task DownloadFile_Test()
    {
        var txt = await InventorySystem.DownloadFile(
            "https://raw.githubusercontent.com/filipczekajlo/ALOTA-public/main/DefaultAgentAir.txt");
        txt.Should().NotBeNullOrEmpty();
        txt.Should().Contain("Air Big Bullet");
    }
    
    [Fact]
    public async Task DownloadFileAndDeserialize_Test()
    {
        var txt = await InventorySystem.DownloadFile(
            "https://raw.githubusercontent.com/filipczekajlo/ALOTA-public/main/DefaultAgentAir.txt");

        AgentData agentData = JsonSerializer.Deserialize<AgentData>(txt);

        agentData.Should().NotBeNull();
        agentData.Inventories.EquippedAttacks.Slots[0].item.Name.Should().Be("Air Big Bullet");
        agentData.Inventories.EquippedAttacks.Slots[1].item.Name.Should().Be("Air Cone");
        txt.Should().NotBeNullOrEmpty();
        txt.Should().Contain("Air Big Bullet");
    }

    [Fact]
    public async Task GetAgent_Test()
    {
        
    }
    
    [Fact]
    public void LoadDefaultAgentfromJson_Test()
    {
        var inventorySystem = new InventorySystem();
        // string test = inventorySystem.LoadDefaultAgentFromJson();


        // test.Should().NotBeNull();
        
    }

    [Fact]
    public void SerializeInventorySlot_Test()
    {
        var inventorySlot = new InventorySlot(new Item());

        var serialized = JsonSerializer.Serialize(inventorySlot);
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
        defaultAgent.Inventories.EquippedAttacks.Slots[0].item.Id.Should();
    }
}