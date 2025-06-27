using UnityEngine;

// Enum đặt bên ngoài class
public enum ItemType
{
    Consumable,
    Equipment,
    KeyItem,
    Material
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("General Info")]
    public string itemID;        // ID duy nhất để phân biệt khi lưu/đọc JSON
    public string itemName;
    public Sprite icon;
    public ItemType type;

    [Header("Effect Values")]
    public float healAmount = 0f;
    public float staminaAmount = 0f;
    public float manaAmount = 0f;

    // quantity sẽ được quản lý riêng bởi InventoryItem, không nên để ở đây
}
