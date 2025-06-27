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
        private float maxMana = 1f; // nếu cần
        private float maxHealth = 1f;
        public bool isJumping = false;

        
        private float regenDelay = 2f;             // Delay trước khi bắt đầu hồi
        private float regenTimer = 0f;
        private float staminaRegenRate = 0.3f;     // Tốc độ hồi (có thể chỉnh)
        private float manaRegenRate = 0.01f;


        private bool isConsuming = false;
        private bool exhausted = false;
        public bool isLoadingFromSave = false;//savee

        public bool IsExhausted => exhausted;
        public bool IsLowStamina => stamina < 0.5f;
        public LayerMask groundLayer;


        void Awake(){
            instance = this;
        }
        void Start()
    {
        if (!GameData.Instance.isLoadingFromSave)
        {
            healthBar.value = 1f;
            manaBar.value = 1f;
            stamina = 1f;
            staminaBar.value = stamina;
        }
        else
        {
            healthBar.value = GameData.Instance.playerHealth / 100f;
            manaBar.value = GameData.Instance.playerMana / 100f;

            float staNormalized = GameData.Instance.playerSTA / 100f;
            stamina = staNormalized;
            staminaBar.value = staNormalized;

            GameData.Instance.isLoadingFromSave = false;
        }
    }

        void Update()
        {
           if (stamina <= 0f)
            {
                
                exhausted = true;
                regenTimer += Time.deltaTime;

                if (regenTimer >= regenDelay)
                {
                    stamina += Time.deltaTime * staminaRegenRate * 5f; // hồi nhanh hơn bình thường
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
                        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
                    }
                }
                else
                {
                    regenTimer = 0f;
                }

                // Nếu xài tiếp mà tụt xuống 0 thì đánh dấu exhausted lần nữa
                if (stamina <= 0f)
                {
                    stamina = 0f;
                    exhausted = true;
                    regenTimer = 0f;
                }
}

            isConsuming = false;

            // Update thanh bar
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
            staminaBar.value = stamina;
            healthBar.value = Mathf.Clamp01(healthBar.value);    
            manaBar.value = Mathf.Clamp01(manaBar.value);
        
            RegenMP(0.01f);
        
        }

        public void RegenHP(float amountHP){
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
        public bool UseMana(float amount)
        {
        if (manaBar.value >= amount)
            {
                manaBar.value -= amount;
                manaBar.value = Mathf.Clamp01(manaBar.value);
                return true; // đủ mana, dùng được
            }
        else
            {
                Debug.Log("Không đủ mana!");
                return false; // thiếu mana
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