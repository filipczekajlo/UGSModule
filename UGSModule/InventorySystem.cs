﻿using System.Diagnostics;
using InventoryDTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.Services.CloudCode.Apis;
using Unity.Services.CloudCode.Core;
using Unity.Services.CloudSave.Model;

namespace UGS_Module;

public class InventorySystem
{
    [CloudCodeFunction("RequestPlayerCloudData")]
    public async Task<string> RequestPlayer(IExecutionContext ctx, IGameApiClient apiClient)
    {
        var playerJson = await GetSingleJsonStringFromCloudSave(ctx, apiClient, StringConsts.PlayerCloudData);
        if (playerJson != null)
        {
            return playerJson;
        }

        // Create new PlayerData if non is found in the cloud. player has probably started game for the first time.
        ResetRequests resetRequests = new ResetRequests();
        return resetRequests.ResetPlayer(ctx, apiClient).Result;
    }
    
    [CloudCodeFunction(nameof(RequestSavePlayerData))]
    public async Task<string> RequestSavePlayerData(IExecutionContext ctx, IGameApiClient apiClient, string element)
    {
        var playerJson = await GetSingleJsonStringFromCloudSave(ctx, apiClient, StringConsts.PlayerCloudData);
        if (playerJson != null)
        {
            var deserializedCloudData = JsonConvert.DeserializeObject<PlayerCloudData>(playerJson);

            deserializedCloudData.CurrentAgentKey = element;

            var serializedCloudData = JsonConvert.SerializeObject(deserializedCloudData);

            SaveToCloudSave(ctx, apiClient, StringConsts.PlayerCloudData, serializedCloudData);

            return serializedCloudData;
        }

        return null;
    }
    
    [CloudCodeFunction(nameof(RequestSaveAgentData))]
    public async Task<string> RequestSaveAgentData(IExecutionContext ctx, IGameApiClient apiClient, string element, string data)
    {
        await SaveToCloudSave(ctx, apiClient, element, data);
        
        return "Saved to CloudSave!!!";
    }
    
    
    
    [CloudCodeFunction(nameof(RequestAgent))]
    public async Task<string> RequestAgent(IExecutionContext ctx, IGameApiClient apiClient, string element)
    {
        var agentJson = await GetSingleJsonStringFromCloudSave(ctx, apiClient, element);
        if (agentJson != null)
        {
            return agentJson;
        }

        
        AgentData agentData = new AgentData();
        agentData.Inventories = CreateDefaultInventories(element);
        
        var settings = new JsonSerializerSettings();
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        settings.Converters.Add(new ItemDataJsonConverter());
        
        var serializedAgentData = JsonConvert.SerializeObject(agentData);
        await SaveToCloudSave(ctx, apiClient,element , serializedAgentData);

        return serializedAgentData;
    }
    
    
    public Inventories CreateDefaultInventories(string element)
    {
        // Create default inventories if non is found in the cloud. player has probably started game for the first time.

        ItemFactory itemFactory = new ItemFactory();
        var inventories = new Inventories();
        inventories.EquippedAttacks.Slots.Add(new InventorySlot(itemFactory.CreateDefaultItem(StringConsts.BigBullet, element)));
        inventories.EquippedAttacks.Slots.Add(new InventorySlot(itemFactory.CreateDefaultItem(StringConsts.Cone, element)));
        inventories.EquippedAttacks.Slots.Add(new InventorySlot(itemFactory.CreateDefaultItem(StringConsts.Field, element)));
        inventories.EquippedAttacks.Slots.Add(new InventorySlot(itemFactory.CreateDefaultItem(StringConsts.Ground, element)));
        inventories.UnequippedAttacks.Slots.Add(new InventorySlot(itemFactory.CreateDefaultItem(StringConsts.Heal, element)));
        inventories.UnequippedAttacks.Slots.Add(new InventorySlot(itemFactory.CreateDefaultItem(StringConsts.SmallBullet, element)));
        inventories.UnequippedAttacks.Slots.Add(new InventorySlot(itemFactory.CreateDefaultItem(StringConsts.Sprint, element)));
        inventories.UnequippedAttacks.Slots.Add(new InventorySlot(itemFactory.CreateDefaultItem(StringConsts.Wall, element)));
        
        return inventories;
    }

    
    
    [CloudCodeFunction(nameof(RequestAllItemData))]
    public async Task<List<string>> RequestAllItemData(IExecutionContext ctx, IGameApiClient apiClient)
    {
        var allAttackItems = StringConsts.GetAllAttackItems();

        // var AllAttackItemIDs = new List<string>();
        
        
        List<string> foundItems = await GetJsonStringsFromCloudSave(ctx, apiClient, allAttackItems.Select(x => x.ID).ToList());

        return foundItems;
        
        // foreach (var item in allAttackItems)
        // {
        //     string retrievedItem = await GetSingleJsonStringFromCloudSave(ctx, apiClient, item.ID);
        //
        //     if (retrievedItem != null)
        //     {
        //         AllAttackItemIDs.Add(retrievedItem);
        //     }
        //     
        //     else
        //     {
        //         var newItem = await RequestCreateNewItemData(ctx, apiClient, item.Name, item.Element);
        //         AllAttackItemIDs.Add(newItem);
        //         
        //     }
        // }
        //
        // return AllAttackItemIDs;
    }
    
    [CloudCodeFunction(nameof(RequestCreateNewItemData))]
    public async Task<string> RequestCreateNewItemData(IExecutionContext ctx, IGameApiClient apiClient, string itemName, string element)
    {
        var itemFactory = new ItemFactory();
        var newItem = itemFactory.CreateDefaultItem(itemName, element);

        if (newItem != null)
        {
            string serializedNewItem = JsonConvert.SerializeObject(newItem);
            await SaveToCloudSave(ctx, apiClient, itemName + element, serializedNewItem);
            return serializedNewItem;
        }
        else
        {
            return null;
        }
    }

    // TODO: We can probably get multiple items at once with this method. Use this to your advantage! Get all agents and items at once!
    private async Task<string?> GetSingleJsonStringFromCloudSave(IExecutionContext ctx, IGameApiClient apiClient, string keyName)
    {
        var result = await apiClient.CloudSaveData.GetItemsAsync(
            ctx,
            ctx.AccessToken,
            ctx.ProjectId,
            ctx.PlayerId,
            new List<string> { keyName }
        );

        if (result.Data.Results.Count == 0) return null;
        return result.Data.Results.First().Value.ToString();
    }

    private async Task<List<string?>> GetJsonStringsFromCloudSave(IExecutionContext ctx, IGameApiClient apiClient, List<string> keyNames)
    {
        var result = await apiClient.CloudSaveData.GetItemsAsync(
            ctx,
            ctx.AccessToken, 
            ctx.ProjectId, 
            ctx.PlayerId, 
            keyNames
            );
        
        return result.Data.Results.Select(x => x.Value.ToString()).ToList();
    }


    public async Task<string> SaveToCloudSave(IExecutionContext ctx, IGameApiClient apiClient, string key, string value)
    {
        var result = await apiClient.CloudSaveData
            .SetItemAsync(ctx, ctx.AccessToken, ctx.ProjectId, ctx.PlayerId, new SetItemBody(key, value));

        return result.Data.ToString();
    }
    

    [CloudCodeFunction(nameof(RequestRewardPlayer))]
    public async Task<string> RequestRewardPlayer(IExecutionContext ctx, IGameApiClient apiClient,string element,  int xpPoints)
    {
        var agent = RequestAgent(ctx, apiClient, element).Result;
        var playerJson = await GetSingleJsonStringFromCloudSave(ctx, apiClient, StringConsts.PlayerCloudData);
        
        if (agent != null && playerJson != null)
        {
            var deserializedAgent = JsonConvert.DeserializeObject<AgentData>(agent);
            
            LevelingSystem levelingSystem = new LevelingSystem();
            
            var result = levelingSystem.GrantExperience(deserializedAgent.LevelData, xpPoints);
            
            // Player coin-test. TODO: Refactor!
            var deserializedCloudData = JsonConvert.DeserializeObject<PlayerCloudData>(playerJson);
            deserializedCloudData.Coins += 15;
            var serializedCloudData = JsonConvert.SerializeObject(deserializedCloudData);
            await SaveToCloudSave(ctx, apiClient, StringConsts.PlayerCloudData, serializedCloudData);
            
            var serializedAgent = JsonConvert.SerializeObject(deserializedAgent);
            
            await SaveToCloudSave(ctx, apiClient, element, serializedAgent);
            return $"Agent {element} has been rewarded with {xpPoints} XP points. New XP: {result.TotalXP}. Current Level: {result.CurrentLevel}";
        }
        
        return $"Could not add xp to agent with agentKey '{element}' Agent not found";
    }

    [CloudCodeFunction(nameof(RequestChangeLevel))]
    public async Task<string> RequestChangeLevel(IExecutionContext ctx, IGameApiClient apiClient, string itemID, bool upgrade)
    {
        var itemData = await GetSingleJsonStringFromCloudSave(ctx, apiClient, itemID);
        
        if (itemData != null)
        {
            var deserializedItemData = JsonConvert.DeserializeObject<WeaponData>(itemData);
            
            if(upgrade)
                deserializedItemData?.Upgrade();
            else
                deserializedItemData?.Downgrade(); 
            
            var serializedItem = JsonConvert.SerializeObject(deserializedItemData);
            await SaveToCloudSave(ctx, apiClient, itemID, serializedItem);
            return serializedItem;
        }
        else
        {
            return "Provided ItemData was not of type WeaponData!";
        }
        
        return null;
    }
   

  

    [CloudCodeFunction("TestCall")]
    public async Task<AgentData> TestCall(IExecutionContext ctx, IGameApiClient apiClient)
    {
        return new AgentData();
    }
}