
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class MoverForward : MonoBehaviour
{

public float speed = 1; 
private bool canMove = false;
    private void Update(){
        if (canMove)
        {
            {
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(transform.localScale.x, 0) * speed;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            canMove = true;
        }
    }
    


}