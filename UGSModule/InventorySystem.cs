using System.Text.Json;
using Unity.Services.CloudCode.Apis;
using Unity.Services.CloudCode.Core;
using Unity.Services.CloudSave.Model;

namespace UGS_Module;

public class InventorySystem
{
    [CloudCodeFunction("RequestAgent")]
    public async Task<string> RequestAgent(IExecutionContext ctx, IGameApiClient apiClient, string element)
    {
        var agent = await GetAgent(ctx, apiClient, element);

        if (agent != null)
        {
            await SetAgent(ctx, apiClient, element, agent);
        }
        else
        {
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
            var DefaultAgent = await DownloadFile(url);
            
            await SetAgent(ctx, apiClient, element, DefaultAgent);
            
            return DefaultAgent;
        }


        // var agentString = "";

        // var serializedAgent = JsonSerializer.Serialize(agent);

         return "";
    }

    private async Task<string?> GetAgent(IExecutionContext ctx, IGameApiClient apiClient, string element)
    {
        var result = await apiClient.CloudSaveData.GetItemsAsync(
            ctx, ctx.AccessToken,
            ctx.ProjectId,
            ctx.PlayerId,
            new List<string> { element }
        );

        if (result.Data.Results.Count == 0) return null;
        return result.Data.Results.First().Value.ToString();
        // return JsonSerializer.Deserialize<Agent>(result.Data.Results.First().Value.ToString());
    }

    private async Task<string> SetAgent(IExecutionContext ctx, IGameApiClient apiClient, string key, string value)
    {
        var result = await apiClient.CloudSaveData
            .SetItemAsync(ctx, ctx.AccessToken, ctx.ProjectId, ctx.PlayerId, new SetItemBody(key, value));

        return result.Data.ToJson();
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

    public Agent CreateDefaultAgent()
    {
        var test = new Agent(true);
        var s = JsonSerializer.Serialize(test);
        var path = Path.Combine("DefaultAgents", "DefaultAgentAir.txt");

        File.WriteAllText(path, s);

        return test;
    }
}