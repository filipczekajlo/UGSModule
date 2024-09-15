﻿using InventoryDTO;
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
        var playerJson = await GetFromCloudSave(ctx, apiClient, "PlayerCloudData");
        if (playerJson != null)
        {
            return playerJson;
        }

        // Create new PlayerData if non is found in the cloud. player has probably started game for the first time.

        return ResetPlayer(ctx, apiClient).Result;
    }
    
    [CloudCodeFunction(nameof(RequestSetPlayerData))]
    public async Task<string> RequestSetPlayerData(IExecutionContext ctx, IGameApiClient apiClient, string element)
    {
        var playerJson = await GetFromCloudSave(ctx, apiClient, "PlayerCloudData");
        if (playerJson != null)
        {
            var deserializedCloudData = JsonConvert.DeserializeObject<PlayerCloudData>(playerJson);

            deserializedCloudData.CurrentAgentKey = element;

            var serializedCloudData = JsonConvert.SerializeObject(deserializedCloudData);

            SaveToCloudSave(ctx, apiClient, "PlayerCloudData", serializedCloudData);

            return serializedCloudData;
        }

        return null;
    }
    
    [CloudCodeFunction(nameof(RequestAgent))]
    public async Task<string> RequestAgent(IExecutionContext ctx, IGameApiClient apiClient, string element)
    {
        var agentJson = await GetFromCloudSave(ctx, apiClient, element);
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
    
    [CloudCodeFunction(nameof(ResetPlayer))]
    public async Task<string> ResetPlayer(IExecutionContext ctx, IGameApiClient apiClient)
    {
        PlayerCloudData defaultPlayerCloudData = new PlayerCloudData();
        string serializedDefaultPlayerCloudData = JsonConvert.SerializeObject(defaultPlayerCloudData);

        await SaveToCloudSave(ctx, apiClient, "PlayerCloudData", serializedDefaultPlayerCloudData);

        return serializedDefaultPlayerCloudData;
    }


    [CloudCodeFunction(nameof(DeleteAgent))]
    public async Task DeleteAgent(IExecutionContext ctx, IGameApiClient apiClient, string element)
    {
        await apiClient.CloudSaveData.DeleteItemAsync(ctx, ctx.AccessToken, element, ctx.ProjectId, ctx.PlayerId);
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

  
    [CloudCodeFunction(nameof(RequestItemData))]
    public async Task<string> RequestItemData(IExecutionContext ctx, IGameApiClient apiClient, string itemID, string element)
    {
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new ItemDataJsonConverter());
            
        string item = await GetFromCloudSave(ctx, apiClient, itemID);
        if (item != null)
        {
            return item;
        
            // Deserialize using the custom converter
            // return JsonConvert.DeserializeObject<ItemData>(item, settings);
        }
        
        
        var itemFactory = new ItemFactory();
        var newItem = itemFactory.CreateDefaultItem(itemID, element);

        if (newItem != null)
        {
            var serializedNewItem = JsonConvert.SerializeObject(newItem);
            await SaveToCloudSave(ctx, apiClient, itemID, serializedNewItem);
            return serializedNewItem;
        }

        return null;
        // return newItem;
    }

    private async Task<string?> GetFromCloudSave(IExecutionContext ctx, IGameApiClient apiClient, string keyName)
    {
        var result = await apiClient.CloudSaveData.GetItemsAsync(
            ctx, ctx.AccessToken,
            ctx.ProjectId,
            ctx.PlayerId,
            new List<string> { keyName }
        );

        if (result.Data.Results.Count == 0) return null;
        return result.Data.Results.First().Value.ToString();
    }

    private async Task<string> SaveToCloudSave(IExecutionContext ctx, IGameApiClient apiClient, string key, string value)
    {
        var result = await apiClient.CloudSaveData
            .SetItemAsync(ctx, ctx.AccessToken, ctx.ProjectId, ctx.PlayerId, new SetItemBody(key, value));

        return result.Data.ToString();
    }

    public static async Task<string> DownloadFile(string fileUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Send a GET request to the URL
                HttpResponseMessage response = await client.GetAsync(fileUrl);

                // Ensure we received a successful response
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }

                // Read and return the content of the response
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }

    [CloudCodeFunction(nameof(RequestRewardPlayer))]
    public async Task<string> RequestRewardPlayer(IExecutionContext ctx, IGameApiClient apiClient,string element,  int xpPoints)
    {
        var agent = RequestAgent(ctx, apiClient, element).Result;
        if (agent != null)
        {
            var deserializedAgent = JsonConvert.DeserializeObject<AgentData>(agent);
            deserializedAgent.Progress += xpPoints;
            var serializedAgent = JsonConvert.SerializeObject(deserializedAgent);
            
            await SaveToCloudSave(ctx, apiClient, element, serializedAgent);
            return serializedAgent;
        }
        
        return null;
    }

    [CloudCodeFunction(nameof(RequestUpgradeWeapon))]
    public async Task<string> RequestUpgradeWeapon(IExecutionContext ctx, IGameApiClient apiClient, string itemID, int level)
    {
        var item = await GetFromCloudSave(ctx, apiClient, itemID);
        if (item != null)
        {
            var deserializedItem = JsonConvert.DeserializeObject<ItemData>(item);
            
            
            deserializedItem.TotalDamage += level;
            
            var serializedItem = JsonConvert.SerializeObject(deserializedItem);
            await SaveToCloudSave(ctx, apiClient, itemID, serializedItem);
            return serializedItem;
        }
        
        return null;
    }

  

    [CloudCodeFunction("TestCall")]
    public async Task<AgentData> TestCall(IExecutionContext ctx, IGameApiClient apiClient)
    {
        return new AgentData();
    }
}