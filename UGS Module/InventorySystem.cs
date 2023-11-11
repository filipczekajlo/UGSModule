using System.Text.Json;
using Unity.Services.CloudCode.Apis;
using Unity.Services.CloudCode.Core;
using Unity.Services.CloudSave.Model;

namespace UGS_Module;

public class InventorySystem
{
    [CloudCodeFunction("RequestAgent")]
    public async Task<string> RequestAgent(IExecutionContext ctx, IGameApiClient apiClient)
    {
        Agent inventories = await GetAgent(ctx, apiClient);

        if (inventories == null)
        {
            CreateDefaultAgent();
            return "Inventories are null!";
        }
        var serializedInventories = JsonSerializer.Serialize(inventories);
       
        return serializedInventories;
    }
    
    private async Task<Agent?> GetAgent(IExecutionContext ctx, IGameApiClient apiClient)
    {
        var result = await apiClient.CloudSaveData.GetItemsAsync(
            ctx, ctx.AccessToken,
            ctx.ProjectId, 
            ctx.PlayerId,
            new List<string> { "Agent" }
            );

        if (result.Data.Results.Count == 0) return null;

        return JsonSerializer.Deserialize<Agent>(result.Data.Results.First().Value.ToString());
    }
    
    private async Task<string> SetAgent(IExecutionContext ctx, IGameApiClient apiClient, string key, string value)
    {
        var result = await apiClient.CloudSaveData
            .SetItemAsync(ctx, ctx.AccessToken, ctx.ProjectId, ctx.PlayerId, new SetItemBody(key, value));

        return result.Data.ToJson();
    }

    private void CreateDefaultAgent()
    {
        var test = new Agent();
        JsonSerializer.Serialize(test);
    }
}