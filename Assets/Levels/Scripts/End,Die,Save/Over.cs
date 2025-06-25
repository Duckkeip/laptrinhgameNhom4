using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Start(){
        Time.timeScale = 1f;
        

    }
    public void RestartGame()
    {
        
        SceneManager.LoadScene("Scene1");
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
