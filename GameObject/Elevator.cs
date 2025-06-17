using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 2f;          // Tốc độ di chuyển
    public float topLimit = 5f;       // Giới hạn cao nhất
    public float bottomLimit = 0f;    // Giới hạn thấp nhất

    private bool movingUp = true;
    private GameObject playerOnPlatform = null;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        MoveElevator();

        if (playerOnPlatform != null)
        {
            // Di chuyển player cùng tốc độ với thang
            playerOnPlatform.transform.position += transform.position - lastPosition;
        }

        lastPosition = transform.position;
    }

    void MoveElevator()
    {
        if (movingUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            if (transform.position.y >= topLimit)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            if (transform.position.y <= bottomLimit)
            {
                movingUp = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = null;
        }
    }
}
