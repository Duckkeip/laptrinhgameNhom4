using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerHealth = 100;
    public float playerStamina = 1.0f;

    private void Awake()
    {
        // Chỉ giữ 1 GameManager duy nhất
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Không xóa khi load scene khác
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
