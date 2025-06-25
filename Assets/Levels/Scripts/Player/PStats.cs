using UnityEngine;
using UnityEngine.UI;
using  UnityEngine.SceneManagement;
public class PStats : MonoBehaviour
{
    public static PStats instance;

    public Slider healthBar;
    public Slider manaBar;
    public Slider staminaBar;

    public float stamina = 1f;
    public float maxStamina = 1f;
    public bool isJumping = false;

    
    private float regenDelay = 2f;             // Delay trước khi bắt đầu hồi
    private float regenTimer = 0f;
    private float staminaRegenRate = 0.3f;     // Tốc độ hồi (có thể chỉnh)

    private bool isConsuming = false;
    private bool exhausted = false;

    public bool IsExhausted => exhausted;
    public bool IsLowStamina => stamina < 0.5f;
    public LayerMask groundLayer;


    void Awake(){
        instance = this;
    }
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
                        RegenSTA(0.2f);
                        //stamina += Time.deltaTime * staminaRegenRate;
                        //stamina = Mathf.Clamp(stamina, 0f, maxStamina);
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

    public void RegenHP(float amountHP){
        healthBar.value += amountHP;        
        healthBar.value = Mathf.Clamp01(healthBar.value);

    }
    public void RegenMP(float amountMP){
        manaBar.value += amountMP;
        manaBar.value = Mathf.Clamp01(manaBar.value);
    }
    public void RegenSTA(float amount){
    stamina += amount;
    stamina = Mathf.Clamp(stamina, 0f, maxStamina);
    staminaBar.value = stamina;
    }
    

    /*public void RegenSTA(float amount){
            staminaBar.value += amount;
            staminaBar.value = Mathf.Clamp01(staminaBar.value);
            staminaBar.value = stamina;
        }*/
    
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

    private void Die()
    {   
        SceneManager.LoadScene("GameOverScene");
        //ScoreManager.instance.Over();
        Debug.Log("Player đã chết!");
        gameObject.SetActive(false); // hoặc Destroy(gameObject);
        
    }


}