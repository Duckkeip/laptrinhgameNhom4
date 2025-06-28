[System.Serializable]
public class SerializableInventoryItem
{
    public string itemID;
    public int quantity;

    public SerializableInventoryItem() { } // 

    public SerializableInventoryItem(string id, int quantity)
    {
        this.itemID = id;
        this.quantity = quantity;
    }
}
