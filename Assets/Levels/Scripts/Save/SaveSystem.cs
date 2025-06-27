using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public static class SaveSystem
{
    public static void SaveGame(string saveName)
    {
        GameData.Instance.playerHealth = Mathf.RoundToInt(PStats.instance.healthBar.value * 100f);
        GameData.Instance.playerSTA = Mathf.RoundToInt(PStats.instance.staminaBar.value * 100f);
        GameData.Instance.playerMana = Mathf.RoundToInt(PStats.instance.manaBar.value * 100f);
        //here
        List<SerializableInventoryItem> savedItems = new List<SerializableInventoryItem>();
        foreach (InventoryItem item in Inventory.Instance.items)
        {
            savedItems.Add(new SerializableInventoryItem(item.data.itemID, item.quantity));
        }

        SaveData data = new SaveData
        {//importance
            saveName = saveName,
            playerHealth = GameData.Instance.playerHealth,
            playerSTA = GameData.Instance.playerSTA,
            playerMana = GameData.Instance.playerMana,

            //here
            playerScore = GameData.Instance.playerScore,
            playerPosition = new Vector3Serializable(GameData.Instance.playerPosition),
            sceneName = SceneManager.GetActiveScene().name,
            inventoryItems = savedItems
            
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPath(saveName), json);
    }
    //tao file json
    public static SaveData LoadGame(string saveName)
    {
        string path = GetPath(saveName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;
    }

    public static string[] GetAllSaveFiles()
    {
        return Directory.GetFiles(Application.persistentDataPath, "*.json");
    }

    public static string GetPath(string saveName)
    {
        return Path.Combine(Application.persistentDataPath, saveName + ".json");
    }
}
