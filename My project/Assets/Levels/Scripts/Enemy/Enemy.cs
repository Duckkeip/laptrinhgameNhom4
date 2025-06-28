using UnityEngine;
using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    public EnemyData data;
    private int currentHP;

    void Start()
    {
        if (data != null)
        {
            currentHP = data.maxHP;
            GetComponent<SpriteRenderer>().sprite = data.sprite;
            transform.localScale = data.scale; // đảm bảo đúng scale
              // Tắt Rigidbody2D khi ở FightScene
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null && SceneManager.GetActiveScene().name == "FightScene")
            {
                rb.simulated = false; // hoặc rb.isKinematic = true;
            }
        }
        else
        {
            Debug.LogError("Thiếu EnemyData gán vào prefab!");
        }
    }

    // Gọi khi enemy nhận sát thương
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{data.displayName} đã chết.");
        Destroy(gameObject);
    }
}
