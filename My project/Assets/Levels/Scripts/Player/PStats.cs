using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PStats : MonoBehaviour
{
    public static PStats instance;

    public Slider healthBar;
    public Slider manaBar;
    public Slider staminaBar;

    public float stamina = 1f;
    public float maxStamina = 1f;
    private float maxMana = 1f;
    private float maxHealth = 1f;

    public bool isJumping = false;
    private float regenDelay = 2f;
    private float regenTimer = 0f;
    private float staminaRegenRate = 0.3f;
    private float manaRegenRate = 0.01f;

    private bool isConsuming = false;
    private bool exhausted = false;

    public bool isLoadingFromSave = false;

    public bool IsExhausted => exhausted;
    public bool IsLowStamina => stamina < 0.5f;
    public LayerMask groundLayer;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (!GameData.Instance.isLoadingFromSave)
        {
            Debug.Log("cuutoimoi");
            healthBar.value = 1f;
            manaBar.value = 1f;
            stamina = 1f;
        }
        else
        {
            healthBar.value = GameData.Instance.playerHealth / 100f;
            manaBar.value = GameData.Instance.playerMana / 100f;
            stamina = GameData.Instance.playerSTA / 100f;
            GameData.Instance.isLoadingFromSave = false;
        }

        staminaBar.value = stamina;
    }

    void Update()
    {
        // Đánh dấu exhausted nếu cạn kiệt stamina
        if (stamina <= 0f && !exhausted)
        {
            exhausted = true;
            regenTimer = 0f;
        }

        if (exhausted)
        {
            regenTimer += Time.deltaTime;
            if (regenTimer >= regenDelay)
            {
                stamina += Time.deltaTime * staminaRegenRate * 1.2f; // hồi nhanh khi exhausted

                if (stamina >= maxStamina)
                {
                    stamina = maxStamina;
                    exhausted = false;
                    regenTimer = 0f;
                }
            }
        }
        else
        {
            if (!isConsuming && !isJumping)
            {
                regenTimer += Time.deltaTime;

                if (regenTimer >= regenDelay && stamina < maxStamina)
                {
                    stamina += Time.deltaTime * staminaRegenRate;
                }
            }
            else
            {
                regenTimer = 0f;
            }
        }

        // Reset sau mỗi khung hình
        isConsuming = false;

        // Clamp & cập nhật thanh bar
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        staminaBar.value = stamina;

        healthBar.value = Mathf.Clamp01(healthBar.value);
        manaBar.value = Mathf.Clamp01(manaBar.value);

        RegenMP(0.01f);
    }

    public void RegenHP(float amountHP)
    {
        healthBar.value += amountHP;
        healthBar.value = Mathf.Clamp01(healthBar.value);
    }

    public void RegenMP(float amountMP)
    {
        if (manaBar.value < 1f)
        {
            manaBar.value += Time.deltaTime * manaRegenRate;
            manaBar.value = Mathf.Clamp01(manaBar.value);
        }
    }

    public void RegenSTA(float amount)
    {
        stamina += amount;
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        staminaBar.value = stamina;
    }

    public void ReduceStamina(float amount)
    {
        if (exhausted) return;

        stamina -= amount;
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        staminaBar.value = stamina;
        isConsuming = true;
    }

    public void SetJumping(bool value)
    {
        isJumping = value;
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

    public bool UseMana(float amount)
    {
        if (manaBar.value >= amount)
        {
            manaBar.value -= amount;
            manaBar.value = Mathf.Clamp01(manaBar.value);
            return true;
        }
        else
        {
            Debug.Log("Không đủ mana!");
            return false;
        }
    }

    private void Die()
    {
        SceneManager.LoadScene("GameOverScene");
        Debug.Log("Player đã chết!");
        gameObject.SetActive(false);
    }
}
