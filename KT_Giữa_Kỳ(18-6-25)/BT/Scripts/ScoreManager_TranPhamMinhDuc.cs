using UnityEngine;
using UnityEngine.UI;

public class ScoreManager_TranPhamMinhDuc : MonoBehaviour
{

    public static ScoreManager_TranPhamMinhDuc instance;
    
    public Text scoreText;
    public Text OverscoreText;
    public GameObject gameOverPanel;


    public bool isGameOver = false;
    private long diem = 1;

    
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
        scoreText.text = "diem: " + diem;
        
    }
    public void Over(){
        OverscoreText.text =  "diem " + diem;
        isGameOver = true;
        gameOverPanel.SetActive(true);
        //Time.timeScale = 0f; // (tuỳ chọn) dừng game
    }
    
}
