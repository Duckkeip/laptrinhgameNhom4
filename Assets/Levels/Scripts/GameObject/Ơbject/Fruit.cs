using UnityEngine;

public class Fruit : MonoBehaviour
{   
    public ItemData itemToAdd;

    private float amountHP = 0.3f;
    private float amount =0.2f;
    private Animator ani;
    public GameObject pressEUI;
    private bool isPlayerNearby = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created 
      void Start()
    {
        if (pressEUI != null)
            pressEUI.SetActive(false);
    }
    
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Inventory.Instance.AddItem(itemToAdd);
            ScoreManager.instance.AddScore(10);
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (pressEUI != null)
                pressEUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (pressEUI != null)
                pressEUI.SetActive(false);
        }
    }
    
}
