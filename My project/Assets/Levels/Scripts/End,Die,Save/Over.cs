using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Start(){
        Time.timeScale = 1f;
        

    }
    public void MainMenu(){
       SceneManager.LoadScene("Start");
    }
    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
       public void SaveGame()
    {
        SaveSystem.SaveGame("quick_save");
        Debug.Log("Game Saved!");
    }
    public void ExitGame()
    {
        
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit(); // Dành cho build thật
    #endif
    }
}
