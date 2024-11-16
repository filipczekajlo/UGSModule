namespace InventoryDTO
{
    public class WeaponData : ItemData
    {
        public WeaponData()
        {
            TotalDamage = 0;
            ChiCost = 0;
            CooldownTime = 0;
            Duration = 0;
        }
        
        public int TotalDamage { get; set; }
        public int ChiCost { get; set; }
        public float CooldownTime { get; set; }
        public float Duration { get; set; }

    }
}