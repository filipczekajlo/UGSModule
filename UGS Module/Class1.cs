using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Unity.Services.CloudCode.Core;
using Unity.Services.CloudCode.Apis;
using Unity.Services.CloudCode.Shared;
using Unity.Services.CloudSave.Api;
using Unity.Services.CloudSave.Model;
using Unity.Services.Economy.Api;
using Unity.Services.Economy.Model;

namespace UGS_Module;

public class Class1
{
    [CloudCodeFunction("SayHello")]
    public string Hello(string name)
    {
        return $"Hello, {name}!";
    }

    // __________________ Data Classes ___________________
    public class Quest
    {
        [JsonPropertyName("id")] public int ID { get; set; }

        [JsonPropertyName("name")] public string? Name { get; set; }

        [JsonPropertyName("reward")] public int Reward { get; set; }

        [JsonPropertyName("progress_required")]
        public int ProgressRequired { get; set; }

        [JsonPropertyName("progress_per_minute")]
        public int ProgressPerMinute { get; set; }
    }

    public class QuestData
    {
        public QuestData()
        {

        }

        public QuestData(string questName, int reward, int progressLeft, int progressPerMinute, DateTime questStartTime)
        {
            QuestName = questName;
            Reward = reward;
            ProgressLeft = progressLeft;
            ProgressPerMinute = progressPerMinute;
            QuestStartTime = questStartTime;
            LastProgressTime = new DateTime();
        }

        [JsonPropertyName("quest-name")] public string? QuestName { get; set; }

        [JsonPropertyName("reward")] public long Reward { get; set; }

        [JsonPropertyName("progress-left")] public long ProgressLeft { get; set; }

        [JsonPropertyName("progress-per-minute")]
        public long ProgressPerMinute { get; set; }

        [JsonPropertyName("quest-start-time")] public DateTime QuestStartTime { get; set; }

        [JsonPropertyName("last-progress-time")] public DateTime LastProgressTime { get; set; }
    }
    
    // _____________________ Cloud Code Setup For Dependency Injection ___________________________
    public class CloudCodeSetup : ICloudCodeSetup
    {
        public void Setup(ICloudCodeConfig config)
        {
            config.Dependencies.AddSingleton<IQuestService, QuestService>();
            config.Dependencies.AddSingleton<IGameApiClient>(s => GameApiClient.Create());
        }
    }
    
    // ________________________ Quest Service _________________________________
    public interface IQuestService
    {
        IList<Quest> GetAvailableQuests(IExecutionContext ctx);
    }

    public class QuestService : IQuestService
    {
        private readonly IGameApiClient _apiClient;

        public QuestService(IGameApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        private DateTime? CacheExpiryTime { get; set; }

        // Reminder: cache cannot be guaranteed to be consistent across all requests
        private IList<Quest>? QuestCache { get; set; }

        public IList<Quest> GetAvailableQuests(IExecutionContext ctx)
        {
            if (QuestCache == null || DateTime.Now > CacheExpiryTime)
            {
                var quests = FetchQuestsFromRC(ctx);
                QuestCache = quests;
                CacheExpiryTime = DateTime.Now.AddMinutes(5); // data in cache expires after 5 mins
            }

            return QuestCache;
        }

        private IList<Quest> FetchQuestsFromRC(IExecutionContext ctx)
        {
            var result = _apiClient.RemoteConfigSettings.AssignSettingsGetAsync(ctx, ctx.AccessToken, ctx.ProjectId,
                ctx.ProjectId, null, new List<string> { "QUESTS" });

            var settings = result.Result.Data.Configs.Settings;

            return JsonSerializer.Deserialize<List<Quest>>(settings["QUESTS"].ToString());
        }
    }
    
    // _______________________________ Quest Controller __________________________________
    
    public class QuestController
{
    [CloudCodeFunction("AssignQuest")]
    public async Task<string> AssignQuest(IExecutionContext ctx, IQuestService questService, IGameApiClient apiClient)
    {
        var questData = await GetQuestData(ctx, apiClient);

        if (questData?.QuestName != null) return "Player already has a quest in progress!";

        var availableQuests = questService.GetAvailableQuests(ctx);
        var random = new Random();
        var index = random.Next(availableQuests.Count);
        var quest = availableQuests[index];

        questData = new QuestData(quest.Name, quest.Reward, quest.ProgressRequired, quest.ProgressPerMinute,
            DateTime.Now);

        await SetQuestData(ctx, apiClient, "quest-data", JsonSerializer.Serialize(questData));
        
        return $"Player was assigned quest: {quest.Name}!";
    }

    [CloudCodeFunction("PerformAction")]
    public async Task<string> PerformAction(IExecutionContext ctx, IGameApiClient apiClient)
    {
        var questData = await GetQuestData(ctx, apiClient);

        if (questData?.QuestName == null) return "Player does not have a quest in progress!";

        if (questData.ProgressLeft == 0) return "Player has already completed their quest!";

        if (DateTime.Now < questData.LastProgressTime.AddSeconds(60 / questData.ProgressPerMinute)) return "Player cannot make quest progress yet!";

        questData.LastProgressTime = DateTime.Now;
        questData.ProgressLeft--;

        await SetQuestData(ctx, apiClient, "quest-data", JsonSerializer.Serialize(questData));

        return "Player made quest progress!";
    }

    private async Task<QuestData?> GetQuestData(IExecutionContext ctx, IGameApiClient apiClient)
    {
        var result = await apiClient.CloudSaveData.GetItemsAsync(
            ctx, ctx.AccessToken, ctx.ProjectId, ctx.PlayerId,
            new List<string> { "quest-data" });

        if (result.Data.Results.Count == 0) return null;

        return JsonSerializer.Deserialize<QuestData>(result.Data.Results.First().Value.ToString());
    }

    private async Task<string> SetQuestData(IExecutionContext ctx, IGameApiClient apiClient, string key, string value)
    {
        var result = await apiClient.CloudSaveData
            .SetItemAsync(ctx, ctx.AccessToken, ctx.ProjectId, ctx.PlayerId, new SetItemBody(key, value));

        return result.Data.ToJson();
    }
}
    
    
    // _________________ LootBox ____________________

    public class LootBoxSystem
    {
          private string[] currencies = { "COIN", "GEM", "PEARL", "DIAMOND" };
        
        [CloudCodeFunction("GetRandomLoot")]
        public async Task<string> GetRandomLoot(IExecutionContext ctx, IGameApiClient gameApiClient)
        {
            string currencyID = GetRandomCurrency();

             int amonunt = GetRandomCurrencyAmount(currencyID);

             await GrantCurrency(ctx, gameApiClient.EconomyCurrencies, ctx.ProjectId, ctx.PlayerId, currencyID, amonunt);

             return "Currency changed to " + currencyID + " " + amonunt;
        }
        

        private async Task<string> GrantCurrency(IExecutionContext ctx, IEconomyCurrenciesApi economyCurrencyAPI, string projectID, string playerID, string currencyID, int amount)
        {
           var req =  new CurrencyModifyBalanceRequest
           {
               Amount = amount
           };

           await economyCurrencyAPI.IncrementPlayerCurrencyBalanceAsync(ctx, ctx.AccessToken, projectID, playerID,
                currencyID, req);
        
            return "";
        }
        private string GetRandomCurrency()
        {
            Random rand = new Random();
            int randomIndex = rand.Next(currencies.Length);  // Generates a random number between 0 and the length of the array
            return currencies[randomIndex];
        }

        private int GetRandomCurrencyAmount(string currency)
        {
            Random rand = new Random();
            int randomIndex = rand.Next(1,22);  // Generates a random number between 0 and the length of the array
            return randomIndex;
        }
    }
    
    //___________________ Inventory ______________________

    public class InventorySystem
    {
        // [CloudCodeFunction("GetInventory")]
        // public async Task<string> GetInventory(IExecutionContext ctx, IGameApiClient apiClient)
        // {
        //     // ctx.
        // }
        //
        // [CloudCodeFunction("SaveInventoryRequest")]
        // public async Task<string> SaveInventoryRequest(IExecutionContext ctx, IGameApiClient apiClient)
        // {
        //     // ctx.
        // }
        
        

    }
}