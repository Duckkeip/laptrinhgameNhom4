using UnityEngine;
using System.Collections.Generic;
public class GameData : MonoBehaviour
{
    public static GameData Instance;
    

    public bool isLoadingFromSave = false;
    public int playerHealth = 100;
    public int playerSTA = 100;     
    public int playerMana = 100;    
    public int playerScore = 0;
    public string sceneName;
    public Vector3 playerPosition;
    public List<SerializableInventoryItem> inventoryItems = new List<SerializableInventoryItem>();

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
