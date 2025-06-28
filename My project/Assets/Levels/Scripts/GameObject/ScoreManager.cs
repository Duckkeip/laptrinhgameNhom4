using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    
    public Text scoreText;
    public Text OverscoreText;
    public GameObject gameOverPanel;


    public bool isGameOver = false;
    public int diem = 0;

    
    void Awake()
    {
        if (instance == null){
            instance = this;
                }
        else{
            Destroy(gameObject);
                }
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            gameOverPanel.SetActive(!gameOverPanel.activeSelf);
            
            }
    }
    public void AddScore(int amount)
    {
        diem += amount;
        scoreText.text = "Aura: " + diem;
        
    }
    public void Over(){
        OverscoreText.text =  "Total " + diem;
        isGameOver = true;
        gameOverPanel.SetActive(true);
        //Time.timeScale = 0f; // (tuỳ chọn) dừng game
    }
    
}
