using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public Transform enemySpawnPoint;
    public Transform playerSpawnPoint;

    void Start()
    {   
            if (GameData.Instance == null)
    {
        Debug.LogError("GameData.Instance is null! Đảm bảo có GameData trong scene trước khi vào FightScene.");
        return;
    }
        string enemyID = GameData.Instance.enemyID;

        // Load dữ liệu ScriptableObject
        EnemyData data = Resources.Load<EnemyData>($"Enemies/{enemyID}");

        if (data != null)
        {
            // Load prefab tương ứng
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/Enemies/{enemyID}");
            if (prefab != null)
            {
                // Tạo enemy ở vị trí spawn
                GameObject enemy = Instantiate(prefab, enemySpawnPoint.position, Quaternion.identity);

                // Gán dữ liệu từ ScriptableObject
                Enemy enemyScript = enemy.GetComponent<Enemy>();
                if (enemyScript != null)
                {
                    enemyScript.data = data;
                }

                // Phóng to enemy: scale gốc * 1.5 (hoặc 2 nếu muốn to hơn nữa)
                enemy.transform.localScale = GameData.Instance.enemyScale * 2.5f;


                SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.flipX = true; // ✅ Lật sprite theo trục X
                }
                Animator ani = enemy.GetComponent<Animator>();
                if (ani != null)
                {
                     ani.enabled = false;
                    
                }
                // Tắt Rigidbody2D để enemy đứng yên
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.simulated = false;
                    rb.linearVelocity = Vector2.zero;
                }
            }
            else
            {
                Debug.LogError($"Không tìm thấy prefab tại Prefabs/Enemies/{enemyID}");
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy EnemyData: " + enemyID);
        }

         // Tìm player và đưa tới vị trí cố định trong FightScene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && playerSpawnPoint != null)
        {
            player.transform.position = playerSpawnPoint.position;
        }
    }

    public void EndBattle()
    {
        string sceneToReturn = GameData.Instance.sceneName;
        SceneManager.LoadScene(sceneToReturn);
    }
}
