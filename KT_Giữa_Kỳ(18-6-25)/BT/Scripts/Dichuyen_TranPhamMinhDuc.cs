using UnityEngine;
using System.Collections;

public class Dichuyen_TranPhamMinhDuc : MonoBehaviour
{

    private Rigidbody2D body;
    private SpriteRenderer spr;
    private Animator ani;
    private BoxCollider2D boxcoll;
    private Transform trans;
    
    private bool growthMode;
    private bool canJump ;
    public Collision2D collision;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpSpeed = 3f;
    private float horizontalInput;

    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        boxcoll = GetComponent<BoxCollider2D>();
        trans = GetComponent<Transform>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        ani.SetBool("Run", horizontalInput != 0);
        ani.SetBool("Jump", !isGrounded()    );
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
         if (horizontalInput > 0)
            spr.flipX = false;
        else if (horizontalInput < 0)
            spr.flipX = true;
        
    }

    void FixedUpdate(){
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
    }
     private void Jump()
    {
       if(isGrounded()){
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
            ani.SetTrigger("Jumpa");
            canJump = false;
        }
        
    }
     private void Die()
    {   
        //SceneManager.LoadScene("GameOverScene");
        ScoreManager_TranPhamMinhDuc.instance.Over();
        gameObject.SetActive(false); // hoặc Destroy(gameObject);
        
    }
    private bool isGrounded(){
        return canJump;
    }
     public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_TranPhamMinhDuc"))
        {
            if(growthMode == true ){
                Shrink_TranPhamMinhDuc();
            }
            else{
                Die();
                }
        }
        
        if (collision.gameObject.CompareTag(("Ground_TranPhamMinhDuc")))
        {
            canJump = true ;
        }       
        if(collision.gameObject.CompareTag("Mushroom_TranPhamMinhDuc"))
        {
           Growth_TranPhamMinhDuc();
        }
           
    }
    void Growth_TranPhamMinhDuc(){
        growthMode = true;
        trans.localScale += new Vector3(1.15f,1.15f,0); 
    }
    void Shrink_TranPhamMinhDuc(){
        growthMode = false;
        trans.localScale = new Vector3(1f, 1f, 0);
    }
}
