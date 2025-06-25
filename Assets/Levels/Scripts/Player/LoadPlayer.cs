using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    void Start()
    {
        if (GameData.Instance != null && PStats.instance != null)
        {
            PStats.instance.healthBar.value = GameData.Instance.playerHealth / 100f;
            // ScoreManager.instance.score = GameData.Instance.playerScore;

            transform.position = GameData.Instance.playerPosition;
        }
        else
        {
            Debug.LogWarning("GameData hoặc PStats chưa sẵn sàng để khôi phục dữ liệu.");
        }
    }
}