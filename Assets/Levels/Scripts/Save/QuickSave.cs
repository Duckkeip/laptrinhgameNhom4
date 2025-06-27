using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickSave : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveSystem.SaveGame("quick_save");
            Debug.Log("Game saved!");
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            SaveData data = SaveSystem.LoadGame("quick_save");
            if (data != null)
            {
                GameData.Instance.playerHealth = data.playerHealth;
                GameData.Instance.playerMana = data.playerMana;
                GameData.Instance.playerSTA = data.playerSTA;
                GameData.Instance.playerScore = data.playerScore;
                GameData.Instance.playerPosition = data.playerPosition.ToVector3();
                SceneManager.LoadScene(data.sceneName); // quay lại scene đã lưu
            }
        }
    }
}
