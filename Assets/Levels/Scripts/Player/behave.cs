using UnityEngine;
using System.Collections;

public class Behave : MonoBehaviour
{

    public static Behave Instance;  

    public PStats pStats;

    private Rigidbody2D body;
    private SpriteRenderer spr;
    private Animator ani;
    private BoxCollider2D boxcoll;

    [SerializeField] private Transform wallCheck;

    private float horizontalInput;
    private float currentSpeed;
    private float wallJumpingCounter;
    private float wallJumpingDirection;
    private float bounceForce = 10f;


    private bool isFacingRight = true;
    private bool isWallSliding;
    private bool isWallJumping;
    private bool hasWallJumped = false;

    //falldame
    private float fallDamageThreshold = -50f; // Vận tốc rơi y dưới mức này sẽ gây sát thương
    private float lastYVelocity = 0f;
    
    
    [SerializeField] private float speed = 3f;
    [SerializeField] private float sprintSpeed = 5.5f;
    [SerializeField] private float jumpSpeed = 5.5f;

    [SerializeField] private float wallSlidingSpeed = 6f;
    [SerializeField] private float wallJumpingTime = 1f;
    [SerializeField] private float wallJumpingDuration = 0.2f;
    [SerializeField] public Vector2 wallJumpingPower = new Vector2(5f, 5.5f);

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private void Awake()
    {
          if (GameData.Instance == null)
            {
                GameObject prefab = Resources.Load<GameObject>("GameData");
                Instantiate(prefab);
            }
        body = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        boxcoll = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {

        if (GameData.Instance != null)
        {
                transform.position = GameData.Instance.playerPosition;
        }
        spr.flipX = false;
        pStats = GetComponent<PStats>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && !pStats.IsExhausted;
        currentSpeed = pStats.IsExhausted ? speed * 0.5f : (isSprinting ? sprintSpeed : speed);  // chậm lại khi exhausted


        ani.SetBool("Run", horizontalInput != 0);
        ani.SetBool("Jump", !isGrounded() && !pStats.IsExhausted && onWall()) ;
        ani.SetBool("Sprint", isSprinting && horizontalInput != 0 && !pStats.IsExhausted);
        ani.SetBool("Slide", isWallSliding);
        ani.SetBool("Tired_Idle", pStats.IsExhausted);
        ani.SetBool("Hurt", CompareTag("Enemy"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!pStats.IsExhausted && (isGrounded() || onWall()))// neu ko met va cham dat hoac tuong thi nhay mat Sta
            {
                Jump();
                pStats.ReduceStamina(0.065f);
                pStats.SetJumping(true);
                
            }
            else 
            {
                
                Debug.Log("ANBATUKAM!!");
                
                }
                
        }
        
       //here ScoreManager.instance.Over();
            
        

        if (isGrounded() || !isWallSliding)
        {
            hasWallJumped = false;
        }

        // Trừ stamina khi chạy
        if (isSprinting && horizontalInput != 0 )
        {
            pStats.ReduceStamina(Time.deltaTime * 0.02f);
        }

        
       if (pStats.stamina < 0.2f)
        {
            spr.color = new Color(1f, 0.7f, 0.7f); // stamina thấp = đỏ nhạt
                if (pStats.IsExhausted)
            {
                Tired(); // exhausted = màu xám
            }
        }
        else
        {
            spr.color = Color.white; // bình thường
        }
        WallJump();
        WallSlide();
        Flip();
        CheckFallDamage();

        //luu lai vi tri hien tai
        if (GameData.Instance != null)
        {
            GameData.Instance.playerPosition = transform.position;
        }
    }

    private void FixedUpdate()
    {
        float fallSpeed = body.linearVelocity.y;
        
        body.linearVelocity = new Vector2(horizontalInput * currentSpeed, body.linearVelocity.y);
        lastYVelocity = body.linearVelocity.y;
        if (fallSpeed < 0)
        {
            Debug.Log("Đang rơi với tốc độ: " + fallSpeed);
        }
    }
    private void Tired()
    {
        ani.SetTrigger("Tired_Idlea");
    }
    private void Jump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
            ani.SetTrigger("Jumpa");
        }
        else if (onWall() && !isGrounded())
        {
            float wallDirection = horizontalInput > 0 ? 1 : -1;
            body.linearVelocity = new Vector2(-wallDirection * wallJumpingPower.x, wallJumpingPower.y);
            ani.SetTrigger("Jumpa");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 bounceDirection = (transform.position - collision.transform.position).normalized;
            body.linearVelocity = Vector2.zero; // 
            body.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
            ani.SetTrigger("Hurta");
            GetComponent<PStats>().TakeDamage(0.2f); // 
        }

    }

    private void WallSlide()
    {
        if (onWall() && !isGrounded())
        {
            isWallSliding = true;
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Clamp(body.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
            ani.SetBool("Slide", true);
        }
        else
        {
            isWallSliding = false;
            ani.SetBool("Slide", false);
        }
    }

    private void WallJump()
    {
        if (isWallSliding && hasWallJumped)
        {
            wallJumpingDirection = -Mathf.Sign(transform.localScale.x);
            wallJumpingCounter = wallJumpingTime;
            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f && hasWallJumped && !pStats.IsExhausted)
        {
            isFacingRight = !isFacingRight;
            isWallJumping = true;
            hasWallJumped = true;

            body.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            Vector3 scale = transform.localScale;
            if ((wallJumpingDirection < 0 && scale.x > 0) || (wallJumpingDirection > 0 && scale.x < 0))
            {
                scale.x *= -1f;
                transform.localScale = scale;
            }

            pStats.ReduceStamina(0.05f);
            pStats.SetJumping(true);

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxcoll.bounds.center,
            boxcoll.bounds.size,
            0f,
            Vector2.down,
            0.1f,
            groundLayer
        );
        return raycastHit.collider != null;
    }

    
    private void CheckFallDamage()
    {
        // Khi chạm đất với tốc độ rơi lớn, gây sát thương
        if (isGrounded() && lastYVelocity < fallDamageThreshold)
        {
            float damage = Mathf.Abs(lastYVelocity) * 0.5f; // hệ số sát thương, có thể chỉnh
            PStats.instance.TakeDamage(damage);
            Debug.Log("Fall damage: " + damage);
        }

    }


    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxcoll.bounds.center,
            boxcoll.bounds.size,
            0f,
            new Vector2(transform.localScale.x, 0),
            0.1f,
            wallLayer
        );
        return raycastHit.collider != null;
    }

    private void Flip()
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}