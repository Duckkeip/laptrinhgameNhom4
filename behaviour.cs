using UnityEngine;

public class Behaviour : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer spr;
    private Animator ani;
    private BoxCollider2D boxcoll;    
    private float wallJumpCooldown ;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float Jspeed = 10f;
   
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        boxcoll = GetComponent<BoxCollider2D>();
  
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Di chuyển ngang

        // Flip nhân vật
        if (body.linearVelocity.x < -0.01f)
            spr.flipX = true;
        else if (body.linearVelocity.x > 0.01f)
            spr.flipX = false;
        
        // Animator parameter
        
        ani.SetBool("Run", horizontalInput != 0);
        ani.SetBool("Jump", isGrounded());
        if  (wallJumpCooldown < 0.2f)
            {
                print(onWall());
                
                // Nhảy
                body.linearVelocity = new Vector2(horizontalInput * speed, body.velocity.y);
                if(onWall() && !isGrounded())
                {
                    
                    body.gravityScale = 0;
                    body.velocity =  Vector2.zero;
                    ani.SetTrigger("Jumpa");
                }
                else 
                {
                    body.gravityScale = 7;
                }
                 if (Input.GetKey(KeyCode.Space) )
                    {
                        Jump();
                    }
               
            }
            else
            {
                wallJumpCooldown += Time.deltaTime;
            }
    }


    private void Jump()

    {   
        if(isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, Jspeed);
            ani.SetTrigger("Jumpa");
    
        }
        if(onWall() && !isGrounded())
        {
            wallJumpCooldown = 0;
            body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

// check wall,ground when touched
    private bool isGrounded()
    {
        RaycastHit2D raycastHit  =  Physics2D.BoxCast(boxcoll.bounds.center ,boxcoll.bounds.size, 0,Vector2.down ,0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit  =  Physics2D.BoxCast(boxcoll.bounds.center ,boxcoll.bounds.size, 0,new Vector2(transform.localScale.x,0) ,0.1f, wallLayer);
        return raycastHit.collider != null;
    }




}
