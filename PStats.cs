using UnityEngine;
using UnityEngine.UI;

public class PStats : MonoBehaviour
{
    public Slider healthBar;
    public Slider manaBar;
    public Slider staminaBar;

    public float stamina = 1f;
    public float maxStamina = 1f;
    public bool isJumping = false;

    private float regenDelay = 2f;             // Delay trước khi bắt đầu hồi
    private float regenTimer = 0f;
    private float staminaRegenRate = 0.2f;     // Tốc độ hồi (có thể chỉnh)

    private bool isConsuming = false;
    private bool exhausted = false;

    public bool IsExhausted => exhausted;
    public bool IsLowStamina => stamina < 0.2f;


    void Start()
    {
        healthBar.value = 1f;
        manaBar.value = 1f;
        staminaBar.value = stamina;
    }

    void Update()
    {
        if (exhausted)
        {
            // Hồi dần cho đến khi đầy thì thoát exhausted
            stamina += Time.deltaTime * staminaRegenRate;

            if (stamina >= maxStamina)
            {
                stamina = maxStamina;
                exhausted = false;
                regenTimer = 0f;
            }
        }
        else
        {
            if (!isConsuming && !isJumping)
            {
                regenTimer += Time.deltaTime;

                if (regenTimer >= regenDelay)
                {
                    // Nếu stamina < max thì mới hồi
                    if (stamina < maxStamina)
                    {
                        stamina += Time.deltaTime * staminaRegenRate;
                        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
                    }
                }
            }
            else
            {
                regenTimer = 0f;
            }

            // Nếu stamina tụt xuống 0 thì exhausted
            if (stamina <= 0f)
            {
                stamina = 0f;
                exhausted = true;
            }
        }

        isConsuming = false;

        // Cập nhật thanh bar
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        staminaBar.value = stamina;
        healthBar.value = Mathf.Clamp01(healthBar.value);
        manaBar.value = Mathf.Clamp01(manaBar.value);
    }

    public void ReduceStamina(float amount)
    {
        if (exhausted) return;

        stamina -= amount;
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        staminaBar.value = stamina;
        isConsuming = true;
    }

    public void TakeDamage(float amount)
    {
        healthBar.value -= amount;
        healthBar.value = Mathf.Clamp01(healthBar.value);

        Debug.Log("Player bị trúng đòn! HP còn lại: " + healthBar.value);

        if (healthBar.value <= 0f)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Player đã chết!");
        // Xử lý chết ở đây, ví dụ: tắt nhân vật
        gameObject.SetActive(false); // hoặc Destroy(gameObject);
    }    
    public void SetJumping(bool value)
    {
        isJumping = value;
    }
}
