using UnityEngine;

public class Nam_TranPhamMinhDuc : MonoBehaviour
{
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player_TranPhamMinhDuc"))
        {
             Destroy(gameObject);
         
        }
    }
}