using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class UIManager : MonoBehaviour
{

    void Awake()
    {
        EnsureGameDataExists();
    }
    public Button startButton;
    public Button continueButton;
    public GameObject settingsPanel;
    public Button exitButton;

    void Start()
    {
        startButton.onClick.AddListener(() => LoadScene("Scene1"));
        continueButton.onClick.AddListener(ContinueGame);
        exitButton.onClick.AddListener(Exit);

        // Disable Continue button nếu không có save
        string path = SaveSystem.GetPath("quick_save");
        if (!File.Exists(path))
        {
            continueButton.interactable = false;
        }
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void ContinueGame()
    {
        SaveData data = SaveSystem.LoadGame("quick_save");

        if (data != null)
        {
            // Lưu lại dữ liệu
        GameData.Instance.playerHealth = data.playerHealth;
        GameData.Instance.playerSTA = data.playerSTA;
        GameData.Instance.playerMana = data.playerMana;
        GameData.Instance.playerScore = data.playerScore;
        GameData.Instance.playerPosition = data.playerPosition.ToVector3();
        GameData.Instance.sceneName = data.sceneName;
        GameData.Instance.inventoryItems = data.inventoryItems;
        GameData.Instance.isLoadingFromSave = true;

        SceneManager.LoadScene(data.sceneName);
            }
        else
        {
            Debug.LogWarning("Không tìm thấy file save!");
        }
    }

    public void Exit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
    void EnsureGameDataExists()
    {
        if (GameData.Instance == null)
        {
            GameObject prefab = Resources.Load<GameObject>("GameData");
            Instantiate(prefab);
        }
    }
}
