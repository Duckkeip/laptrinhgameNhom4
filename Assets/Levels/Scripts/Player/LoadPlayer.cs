using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    void Start()
    {
        if (GameData.Instance != null && PStats.instance != null)
        {
            PStats.instance.healthBar.value = GameData.Instance.playerHealth / 100f;
            PStats.instance.manaBar.value = GameData.Instance.playerMana / 100f;
            PStats.instance.staminaBar.value = GameData.Instance.playerSTA / 100f;
            //ScoreManager.instance.score = GameData.Instance.playerScore;

            // Gán lại các biến float (nếu bạn dùng logic regen)
            PStats.instance.stamina = GameData.Instance.playerSTA / 100f;

            // Position
            transform.position = GameData.Instance.playerPosition;

            // Load Inventory
            if (Inventory.Instance != null)
            {
                Inventory.Instance.LoadInventory(GameData.Instance.inventoryItems);
            }

            GameData.Instance.isLoadingFromSave = false; // Reset lại
        }
    }
}
