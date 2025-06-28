[System.Serializable]
public class InventoryItem
{
    public ItemData data;
    public int quantity;

    public InventoryItem(ItemData data)
    {
        this.data = data;
        quantity = 1;
    }

    public InventoryItem(ItemData data, int quantity)
    {
        this.data = data;
        this.quantity = quantity;
    }
}
