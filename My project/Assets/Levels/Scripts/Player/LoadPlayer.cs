using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerInitializer : MonoBehaviour
{
    void Start()
    {
        if (GameData.Instance == null || PStats.instance == null) return;

        // Nếu chưa có ItemDatabase thì load
        if (ItemDatabase.Instance == null)
        {
            GameObject itemDBPrefab = Resources.Load<GameObject>("ItemDatabase");
            if (itemDBPrefab != null)
                Instantiate(itemDBPrefab);
            else
                Debug.LogError("Không tìm thấy prefab ItemDatabase trong Resources!");
        }

        // Luôn gán lại chỉ số
        PStats.instance.healthBar.value = GameData.Instance.playerHealth / 100f;
        PStats.instance.manaBar.value = GameData.Instance.playerMana / 100f;
        PStats.instance.staminaBar.value = GameData.Instance.playerSTA / 100f;
        PStats.instance.stamina = GameData.Instance.playerSTA / 100f;

        // Gán lại điểm Aura
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.diem = GameData.Instance.Aura;
            ScoreManager.instance.scoreText.text = "Aura: " + GameData.Instance.Aura;
        }

        // Nếu là load từ Save file
        if (GameData.Instance.isLoadingFromSave)
        {
            transform.position = GameData.Instance.playerPosition;
            GameData.Instance.isLoadingFromSave = false;
        }
        else
        {
            // Ngược lại: Load lại vị trí trước khi vào chiến đấu (nếu có)
            if (!string.IsNullOrEmpty(GameData.Instance.sceneName)) // vừa quay từ fight
            {
                transform.position = GameData.Instance.playerPositionBeforeBattle;
                GameData.Instance.sceneName = ""; // reset lại tránh dùng nhầm
            }
        }

        // Load Inventory
        if (Inventory.Instance != null)
        {
            Inventory.Instance.LoadInventory(GameData.Instance.inventoryItems);
        }
    }
}
