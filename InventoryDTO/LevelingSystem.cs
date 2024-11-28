using System;

namespace InventoryDTO
{
    public class LevelingSystem
    {
        public struct GrantExperienceResult
        {
            public double XPToGain;
            public double TotalXP;
            public int CurrentLevel;

            public GrantExperienceResult(double xpToGain, double totalXP, int currentLevel)
            {
                XPToGain = xpToGain;
                TotalXP = totalXP;
                CurrentLevel = currentLevel;
            }
        }


        public double GetTotalXPForLevel(LevelData levelData, int level)
        {
            if (level <= 0) return 0;

            return levelData.XPForFirstLevel * Math.Pow(level, levelData.Exponent);
        }

        public double GetXPForNextLevel(int currentLevel)
        {
            // return GetTotalXPForLevel(currentLevel + 1) - GetTotalXPForLevel(currentLevel);
            return -1;
        }

        public int Updatelevel(LevelData LevelData)
        {
            int newLevel = CalculateLevelFromTotalXP(LevelData, LevelData.TotalXP);

            // Cap the level at MaxLevel
            if (newLevel > LevelData.MaxLevel)
            {
                newLevel = LevelData.MaxLevel;
                LevelData.TotalXP = GetTotalXPForLevel(LevelData, LevelData.MaxLevel);
            }

            // Update the level if it has changed
            if (newLevel != LevelData.Level)
            {
                int oldLevel = LevelData.Level;
                LevelData.Level = newLevel;

                if (newLevel > oldLevel)
                {
                    Console.WriteLine($"Congratulations! You've reached Level {LevelData.Level}.");
                }
                else
                {
                    Console.WriteLine($"Your level has decreased to Level {LevelData.Level}.");
                }
            }

            double totalXPForCurrentLevel = GetTotalXPForLevel(LevelData, LevelData.Level);
            double totalXPForNextLevel = GetTotalXPForLevel(LevelData, LevelData.Level + 1);

            // Ensure CurrentLevelXP is not negative
            LevelData.CurrentLevelXP = Math.Max(0, LevelData.TotalXP - totalXPForCurrentLevel);

            // Calculate XPToNextLevel to reflect the remaining XP needed to level up
            LevelData.XPToNextLevel = totalXPForNextLevel - LevelData.TotalXP;

            return LevelData.Level;
        }

        public int CalculateLevelFromTotalXP(LevelData levelData, double totalXP)
        {
            // Ensure totalXP is not negative
            if (totalXP < 0)
            {
                return 0; // Minimum level if XP is negative
            }

            // Calculate the level based on totalXP without any additional offset
            double level = Math.Pow(totalXP / levelData.XPForFirstLevel, 1 / levelData.Exponent);

            // Cap the level at MaxLevel
            if (level >= levelData.MaxLevel)
            {
                return levelData.MaxLevel;
            }

            return (int)Math.Floor(level);
        }

        // ToDo: Separate events handling from leveling system
        public GrantExperienceResult GrantExperience(LevelData LevelData, double xpToGain)
        {
            // Add XP, but do not exceed the XP required for the maximum level
            LevelData.TotalXP = Math.Max(0, Math.Min(LevelData.TotalXP + xpToGain, GetTotalXPForLevel(LevelData, LevelData.MaxLevel)));

            var currentLevel = Updatelevel(LevelData);

            return new GrantExperienceResult(xpToGain, LevelData.TotalXP, currentLevel);
        }
    }
}