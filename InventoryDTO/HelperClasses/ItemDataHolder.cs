namespace InventoryDTO;

public class ItemDataHolder
{
    public string Name { get; set; }
    public string Element { get; set; }
    public string ID { get; set; }
    
    public ItemDataHolder(){}
    
    public ItemDataHolder(string name, string element)
    {
        Name = name;
        Element = element;
        ID = name + element;
    }
}