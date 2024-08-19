namespace InventoryDTO;

public class AgentData
{
    public AgentData()
    {
        Inventories = new Inventories();
        Level = 1;
    }
    public Inventories Inventories { get; set; }

    public int Level { get; set; } = 1;
}