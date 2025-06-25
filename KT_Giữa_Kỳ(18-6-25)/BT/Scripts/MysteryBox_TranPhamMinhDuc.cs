using UnityEngine;

public class MysteryBox_TranPhamMinhDuc : MonoBehaviour
{
   public GameObject mushroomPrefab; // Prefab nấm
    public Transform spawnPoint;      // Vị trí sinh nấm
    private bool activated = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (activated) return;

        if (collision.gameObject.CompareTag("Player_TranPhamMinhDuc"))
        {
                activated = true;
                Instantiate(mushroomPrefab, spawnPoint.position, Quaternion.identity);
                Destroy(gameObject);
        }
    }
    
}
