using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillProjectile : MonoBehaviour
{
    public float speed = 10f;
    private bool hasTriggered = false; // tránh gọi nhiều lần
    public float delayBeforeBattle = 1f; // khoảng trễ trước khi vào scene chiến đấu

   private void OnTriggerEnter2D(Collider2D collision)
        {   

            if (collision.CompareTag("Enemy"))
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null)
                {
                // id enemy va scene hien tai
                GameData.Instance.enemyID = collision.gameObject.name;

                GameData.Instance.sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

                // luu vi tri player
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    GameData.Instance.playerPositionBeforeBattle = player.transform.position;
                }

                // luu cac stats 
                GameData.Instance.playerHealth = Mathf.RoundToInt(PStats.instance.healthBar.value * 100f);
                GameData.Instance.playerSTA = Mathf.RoundToInt(PStats.instance.staminaBar.value * 100f);
                GameData.Instance.playerMana = Mathf.RoundToInt(PStats.instance.manaBar.value * 100f);
                GameData.Instance.enemyScale = collision.transform.localScale;

                 UnityEngine.SceneManagement.SceneManager.LoadScene("FightScene");
                StartCoroutine(DelayBeforeEnterBattle());
                // chuyen scene
               
                }
            }
        }
        private System.Collections.IEnumerator DelayBeforeEnterBattle()
    {
        // (Tùy chọn) có thể thêm hiệu ứng như làm chậm thời gian
        Time.timeScale = 0.5f; 

        yield return new WaitForSecondsRealtime(delayBeforeBattle); // đợi theo thời gian thực, không bị ảnh hưởng bởi Time.timeScale

        Time.timeScale = 1f; // khôi phục lại

        SceneManager.LoadScene("FightScene");
    }
}