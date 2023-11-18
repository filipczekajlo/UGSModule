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
        Agent agent = await GetAgent(ctx, apiClient);

        if (agent != null)
        {
        }
        else
        {
            var airAgentURL = "https://github.com/filipczekajlo/ALOTA-public/blob/main/DefaultAgentAir.txt";
            var DefaultAirAgent = await DownloadFile(airAgentURL);
            return DefaultAirAgent;
        }


        var agentString = "";

        // var serializedAgent = JsonSerializer.Serialize(agent);

        return agentString;
    }

    private async Task<Agent?> GetAgent(IExecutionContext ctx, IGameApiClient apiClient)
    {
        var result = await apiClient.CloudSaveData.GetItemsAsync(
            ctx, ctx.AccessToken,
            ctx.ProjectId,
            ctx.PlayerId,
            new List<string> { "Air Agent" }
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