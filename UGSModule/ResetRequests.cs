using System.Diagnostics;
using System.Security.Cryptography;
using InventoryDTO;
using Newtonsoft.Json;
using Unity.Services.CloudCode.Apis;
using Unity.Services.CloudCode.Core;
using Unity.Services.CloudSave.Model;

namespace UGS_Module;

public class ResetRequests
{
    [CloudCodeFunction(nameof(DeleteAgent))]
    public async Task DeleteAgent(IExecutionContext ctx, IGameApiClient apiClient, string element)
    {
        await apiClient.CloudSaveData.DeleteItemAsync(ctx, ctx.AccessToken, element, ctx.ProjectId, ctx.PlayerId);
    }
    
    [CloudCodeFunction(nameof(DeleteAllSavedData))]
    public async Task<string> DeleteAllSavedData(IExecutionContext ctx, IGameApiClient apiClient)
    {
        var allKeys = apiClient.CloudSaveData.GetKeysAsync(ctx, ctx.AccessToken, ctx.ProjectId, ctx.PlayerId).Result;

        List<KeyMetadata> keyMetadatas = allKeys.Data.Results;
        var allKeysList = keyMetadatas.Select(k => k.Key).ToList();
        
        var items = apiClient.CloudSaveData.GetItemsAsync(ctx, ctx.AccessToken, ctx.ProjectId, ctx.PlayerId, allKeysList).Result;
        
        
            
        string allFoundSavedKeys = "ALL FOUND KEYS. NOW DELETING: \n";
        foreach (var key in allKeysList)
        {
            
            allFoundSavedKeys += key;
            allFoundSavedKeys += ",";
            allFoundSavedKeys += "\n";

                
            await apiClient.CloudSaveData.DeleteItemAsync(ctx, ctx.AccessToken, key, ctx.ProjectId, ctx.PlayerId);
            
        }
        // await apiClient.CloudSaveData.DeleteItemsAsync(ctx, ctx.AccessToken, ctx.ProjectId, ctx.PlayerId);

        return allFoundSavedKeys;

    }
    
    [CloudCodeFunction(nameof(ResetPlayer))]
    public async Task<string> ResetPlayer(IExecutionContext ctx, IGameApiClient apiClient)
    {
        PlayerCloudData defaultPlayerCloudData = new PlayerCloudData();
        string serializedDefaultPlayerCloudData = JsonConvert.SerializeObject(defaultPlayerCloudData);

        InventorySystem inventorySystem = new InventorySystem();
        await inventorySystem.SaveToCloudSave(ctx, apiClient, "PlayerCloudData", serializedDefaultPlayerCloudData);

        return serializedDefaultPlayerCloudData;
    }
}