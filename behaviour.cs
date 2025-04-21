using UnityEngine;

public class Behaviour : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer spr;
    private Animator ani;
    
    private bool isGrounded;

    [SerializeField] private float speed = 5f;
   
    
 

    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Di chuyển ngang
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // Flip nhân vật
        if (body.linearVelocity.x < -0.01f)
            spr.flipX = true;
        else if (body.linearVelocity.x > 0.01f)
            spr.flipX = false;

        
        // Nhảy
        if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        // Animator parameter
        
        ani.SetBool("Run", horizontalInput != 0);
        ani.SetBool("Jump", isGrounded);
        
    }

    private void Jump()

    {   
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        ani.SetTrigger("Jumpa");
        isGrounded = false;
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        isGrounded = true;
    }
}
