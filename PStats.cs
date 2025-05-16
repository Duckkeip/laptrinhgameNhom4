using UnityEngine;
using UnityEngine.UI;

public class PStats : MonoBehaviour
{
    public Slider healthBar;
    public Slider manaBar;
    public Slider staminaBar;

    private float stamina = 1f;
    private bool isJumping = false;

    public bool IsExhausted => stamina <= 0f;

    void Start()
    {
        healthBar.value = 1f;
        manaBar.value = 1f;
        staminaBar.value = stamina;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("press H");
            healthBar.value -= 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("press M");
            manaBar.value -= 0.1f;
        }

        // Hồi stamina nếu không nhảy
        

        stamina = Mathf.Clamp01(stamina);
        healthBar.value = Mathf.Clamp01(healthBar.value);
        manaBar.value = Mathf.Clamp01(manaBar.value);
        staminaBar.value = stamina;
    }

    public void RestoreSta(float amount ){
            stamina += Time.deltaTime * 0.5f;
        
    }

    public void ReduceStamina(float amount)
    {
        stamina -= amount;
        stamina = Mathf.Clamp01(stamina);
        staminaBar.value = stamina;
    }

    public void SetJumping(bool value)
    {
        isJumping = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Chạm Enemy.");
            healthBar.value -= 0.1f;
        }
    }
}
