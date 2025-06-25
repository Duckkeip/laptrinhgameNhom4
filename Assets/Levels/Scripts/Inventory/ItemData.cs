using UnityEngine;

// Tạo enum BÊN NGOÀI class để tránh lỗi
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
    public string itemName;
    public Sprite icon;
    public ItemType type;

    public float healAmount = 0f;      // hồi máu
    public float staminaAmount = 0f;   
    public float manaAmount = 0f;

    [HideInInspector]
    public int quantity = 1;
}