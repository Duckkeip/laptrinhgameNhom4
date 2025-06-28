using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    [Header("Danh sách tất cả các vật phẩm")]
    public List<ItemData> allItems;

    private Dictionary<string, ItemData> itemDict;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            BuildItemDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void BuildItemDictionary()
    {
        itemDict = new Dictionary<string, ItemData>();
        foreach (ItemData item in allItems)
        {
            if (!itemDict.ContainsKey(item.itemID))
            {
                itemDict[item.itemID] = item;
            }
            else
            {
                Debug.LogWarning($"Trùng itemID: {item.itemID} trong ItemDatabase!");
            }
        }
    }

    /// <summary>
    /// Truy xuất ItemData từ itemID (dùng khi load inventory từ JSON)
    /// </summary>
    public static ItemData GetItemByID(string id)
    {
        if (Instance == null || Instance.itemDict == null)
        {
            Debug.LogError("ItemDatabase chưa được khởi tạo!");
            return null;
        }

        if (Instance.itemDict.TryGetValue(id, out ItemData data))
        {
            return data;
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy vật phẩm với itemID: {id}");
            return null;
        }
    }
}
