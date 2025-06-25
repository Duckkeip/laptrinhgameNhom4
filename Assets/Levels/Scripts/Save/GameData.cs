using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public int playerHealth = 100;
    public int playerScore = 0;
    public Vector3 playerPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // giữ lại xuyên scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

