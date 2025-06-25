using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button startButton;
    public Button continueButton;
    public GameObject settingsPanel;

    public Button exitButton;
    void Start()
    {
        startButton.onClick.AddListener(() => LoadScene("Scene1"));
        continueButton.onClick.AddListener(() => LoadScene("ContinueScene"));
        //settingsButton.onClick.AddListener(() => ShowSettingsPanel());
        exitButton.onClick.AddListener(Exit);    
    }
    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void Exit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
