using UnityEngine;

public class Coin_TranPhamMinhDuc : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
     void OnCollisionEnter2D(Collision2D other)
    {
     if (other.gameObject.CompareTag("Player_TranPhamMinhDuc"))
            {
                ScoreManager_TranPhamMinhDuc.instance.AddScore(10);
                Destroy(gameObject);
            }
    }    
}
