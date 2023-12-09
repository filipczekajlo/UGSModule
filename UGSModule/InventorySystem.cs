using System.Text.Json;
using InventoryDTO;
using Unity.Services.CloudCode.Apis;
using Unity.Services.CloudCode.Core;
using Unity.Services.CloudSave.Model;

namespace UGS_Module;

public class InventorySystem
{
    [CloudCodeFunction("TestCall")]
    public async Task<AgentData> TestCall(IExecutionContext ctx, IGameApiClient apiClient)
    {
        return new AgentData();

    }   
    
    [CloudCodeFunction("RequestPlayerCloudData")]
    public async Task<string> RequestPlayer(IExecutionContext ctx, IGameApiClient apiClient)
    {
        // return JsonSerializer.Serialize(new PlayerCloudData());
        var playerJson = await GetFromCloudSave(ctx, apiClient, "PlayerCloudData");
        if (playerJson != null)
        {
            return playerJson;
        }

        PlayerCloudData defaultPlayerCloudData = new PlayerCloudData();
         string serializedDefaultPlayerCloudData = JsonSerializer.Serialize(defaultPlayerCloudData);

        await SaveToCloudSave(ctx, apiClient, "PlayerCloudData", serializedDefaultPlayerCloudData);

        return serializedDefaultPlayerCloudData;
        return "";
    }

    [CloudCodeFunction("DeleteAgent")]
    public async Task DeleteAgent(IExecutionContext ctx, IGameApiClient apiClient, string element)
    {
        await apiClient.CloudSaveData.DeleteItemAsync(ctx, ctx.AccessToken, element, ctx.ProjectId, ctx.PlayerId);
        
    }
    
    [CloudCodeFunction("RequestAgentTest")]
    public async Task<string> RequestAgentTest(IExecutionContext ctx, IGameApiClient apiClient, string element)
    {
        // return ("LOLOLOLLOLOL");
        string defaultAgent = "";
        
        // LEVEL MAY BE MISSING IN DEFAULT AGENTS?
        
        string url = "";
        switch (element)
        {
            case "Air":
                url = "https://raw.githubusercontent.com/filipczekajlo/ALOTA-public/main/DefaultAgentAir.txt";
                break;
            case "Earth":
                url = "https://raw.githubusercontent.com/filipczekajlo/ALOTA-public/main/DefaultAgentEarth.txt";
                break;
            case "Fire":
                url = "https://raw.githubusercontent.com/filipczekajlo/ALOTA-public/main/DefaultAgentFire.txt";
                break;
            case "Water":
                url = "https://raw.githubusercontent.com/filipczekajlo/ALOTA-public/main/DefaultAgentWater.txt";
                break;
        }
        
        defaultAgent = await DownloadFile(url);
            

        // await SaveToCloudSave(ctx, apiClient, element, agentJson);

        
        // var deserilaizedAgent = JsonSerializer.Deserialize<AgentData>(defaultAgent);
        
        return defaultAgent;
         // return new AgentData();
    }

    
    [CloudCodeFunction("RequestAgent")]
    public async Task<AgentData> RequestAgent(IExecutionContext ctx, IGameApiClient apiClient, string element)
    {
        return new AgentData();
        // string agentJson = "";
        // var agentJson = await GetFromCloudSave(ctx, apiClient, element);
        // if (agentJson != null)
        // {
        //     return JsonSerializer.Deserialize<AgentData>(agentJson);
        // }

        // string url = "";
        // switch (element)
        // {
        //     case "Air":
        //         url = "https://raw.githubusercontent.com/filipczekajlo/ALOTA-public/main/DefaultAgentAir.txt";
        //         break;
        //     case "Earth":
        //         url = "https://raw.githubusercontent.com/filipczekajlo/ALOTA-public/main/DefaultAgentEarth.txt";
        //         break;
        //     case "Fire":
        //         url = "https://raw.githubusercontent.com/filipczekajlo/ALOTA-public/main/DefaultAgentFire.txt";
        //         break;
        //     case "Water":
        //         url = "https://raw.githubusercontent.com/filipczekajlo/ALOTA-public/main/DefaultAgentWater.txt";
        //         break;
        // }
        //
        // agentJson = await DownloadFile(url);
        //     
        //
        // await SaveToCloudSave(ctx, apiClient, element, agentJson);
        //
        // var deserilaizedAgent = JsonSerializer.Deserialize<AgentData>(agentJson);
        //
        // return deserilaizedAgent;
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

    public string LoadDefaultAgentFromJson()
    {
        var path = Path.Combine("DefaultAgents", "DefaultAgentAir.txt");
        var file = File.ReadAllText(path);
        return file;
    }

    public AgentData CreateDefaultAgent()
    {
        var test = new AgentData();
        var s = JsonSerializer.Serialize(test);
        var path = Path.Combine("DefaultAgents", "DefaultAgentAir.txt");

        File.WriteAllText(path, s);

        return test;
    }
}