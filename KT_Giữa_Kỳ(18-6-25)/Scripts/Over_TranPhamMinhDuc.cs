using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_TranPhamMinhDuc : MonoBehaviour
{
    void Start(){
        Time.timeScale = 1f;

    }
    public void RestartGame()
    {
        
        SceneManager.LoadScene("Scene1");
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
