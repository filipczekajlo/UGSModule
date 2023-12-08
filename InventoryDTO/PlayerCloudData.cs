namespace InventoryDTO;

public class PlayerCloudData
{
    // The key to the current Agent.
    public string CurrentAgentKey { get; set; }
    
    public int Level { get; set; }
    
    public int ExperiencePoints { get; set; }

    public PlayerCloudData()
    {
        CurrentAgentKey = "Air";
        Level = 1;
        ExperiencePoints = 0;
    }
}