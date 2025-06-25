using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public string spawnID; // ID trùng với destinationPortalID từ Portal

    void Start()
    {
        string spawnPointID = PlayerPrefs.GetString("SpawnPointID", "");

        if (spawnID == spawnPointID)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player_TranPhamMinhDuc");
            if (player != null)
            {
                player.transform.position = transform.position;
            }

            // Xoá sau khi spawn để không ảnh hưởng scene sau
            PlayerPrefs.DeleteKey("SpawnPointID");
        }
    }
}
