using System;

namespace InventoryDTO;

public class LevelingSystem
{
    private const double BaseXP = 1000; // Adjust as needed
    private const double GrowthRate = 1.2; // Adjust as needed
    private const int MaxLevel = 100; // Set a level cap if desired

    public double GetTotalXPForLevel(int level)
    {
        return BaseXP * (Math.Pow(GrowthRate, level) - 1) / (GrowthRate - 1);
    }

    public double GetXPForNextLevel(int currentLevel)
    {
        return GetTotalXPForLevel(currentLevel + 1) - GetTotalXPForLevel(currentLevel);
    }

    public void UpdateAgentLevel(AgentData agentData)
    {
        int newLevel = CalculateLevelFromTotalXP(agentData.LevelData.TotalXP);
        if (newLevel > MaxLevel)
        {
            newLevel = MaxLevel;
            agentData.LevelData.TotalXP = GetTotalXPForLevel(MaxLevel);
        }

        if (newLevel > agentData.LevelData.Level)
        {
            agentData.LevelData.Level = newLevel;
            Console.WriteLine($"Congratulations! You've reached Level {agentData.LevelData.Level}.");
        }

        double totalXPForCurrentLevel = GetTotalXPForLevel(agentData.LevelData.Level);
        double totalXPForNextLevel = GetTotalXPForLevel(agentData.LevelData.Level + 1);
        agentData.LevelData.CurrentLevelXP = agentData.LevelData.TotalXP - totalXPForCurrentLevel;
        agentData.LevelData.XPToNextLevel = totalXPForNextLevel - agentData.LevelData.TotalXP;
    }
    

    private int CalculateLevelFromTotalXP(double totalXP)
    {
        double level = Math.Log((totalXP * (GrowthRate - 1) / BaseXP) + 1, GrowthRate);
        return (int)Math.Floor(level);
    }
    
    // ToDo: Seperate events handling from leveling system
    public void GrantExperience(AgentData agentData, double xpGained)
    {
        agentData.LevelData.TotalXP += xpGained;
        UpdateAgentLevel(agentData);
    }
}
