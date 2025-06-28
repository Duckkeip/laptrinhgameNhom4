using System.Collections.Generic;
[System.Serializable]
public class SaveData
{
    

    public string saveName;
    public int playerHealth;
    public int playerSTA;
    public int playerMana;
    public int Aura;
    public Vector3Serializable playerPosition;
    public string sceneName;

    public List<SerializableInventoryItem> inventoryItems;
    

}
