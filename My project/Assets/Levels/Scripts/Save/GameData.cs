using UnityEngine;
using System.Collections.Generic;
public class GameData : MonoBehaviour
{
    public static GameData Instance;
    

    public bool isLoadingFromSave = false;
    public int playerHealth = 100;
    public int playerSTA = 100;     
    public int playerMana = 100;    

    public string enemyID;
    public Vector3 enemyScale; // scale gốc từ scene 1
    public Vector3 playerPositionBeforeBattle;
    
    public int Aura = 0;
    
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
