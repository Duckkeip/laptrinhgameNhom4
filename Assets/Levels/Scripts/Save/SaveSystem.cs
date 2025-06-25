using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static void SaveGame(string saveName)
    {
        GameData.Instance.playerHealth = Mathf.RoundToInt(PStats.instance.healthBar.value * 100f);
        SaveData data = new SaveData
        {
            saveName = saveName,
            playerHealth = GameData.Instance.playerHealth,
            playerScore = GameData.Instance.playerScore,
            playerPosition = new Vector3Serializable(GameData.Instance.playerPosition),
            sceneName = SceneManager.GetActiveScene().name
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPath(saveName), json);
    }

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
