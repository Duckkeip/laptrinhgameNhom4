using System.Collections.Generic;
using UnityEngine;
//quan ly inven
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<InventoryItem> items = new List<InventoryItem>();

  
    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChangedCallback;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

   
    public void AddItem(ItemData itemToAdd)
    {
        // Kiểm tra xem item này đã có trong inventory chưa
        InventoryItem existingItem = items.Find(x => x.data == itemToAdd);
        if (existingItem != null)
        {
            existingItem.quantity++;
        }
        else
        {
            items.Add(new InventoryItem(itemToAdd));
        }
        onInventoryChangedCallback?.Invoke();
    }
        public void RemoveItem(ItemData itemToRemove)
    {
        InventoryItem existingItem = items.Find(x => x.data == itemToRemove);
        if (existingItem != null)
        {
            existingItem.quantity--;
            if (existingItem.quantity <= 0)
            {
                items.Remove(existingItem);
            }
            onInventoryChangedCallback?.Invoke();
        }
    }

    public void LoadInventory(List<SerializableInventoryItem> savedItems)
    {
        items.Clear();
        foreach (SerializableInventoryItem sItem in savedItems)
        {
            ItemData itemData = ItemDatabase.GetItemByID(sItem.itemID);
            if (itemData != null)
            {
                InventoryItem newItem = new InventoryItem(itemData, sItem.quantity);
                items.Add(newItem);
            }
        }
        onInventoryChangedCallback?.Invoke();
    }

    public List<SerializableInventoryItem> SaveInventory()
    {
        List<SerializableInventoryItem> savedItems = new List<SerializableInventoryItem>();
        foreach (var item in items)
        {
            savedItems.Add(new SerializableInventoryItem
            {
                itemID = item.data.itemID,
                quantity = item.quantity
            });
        }
        return savedItems;
    }
}
