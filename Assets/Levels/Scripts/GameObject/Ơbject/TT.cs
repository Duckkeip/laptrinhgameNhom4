using UnityEngine;

public class TT : MonoBehaviour
{
    [Header("UI hiển thị khi Player đi vào")]
    public GameObject tutorialUI;  // Gán UI hướng dẫn (Text/Image Panel) vào đây

    private bool isPlayerNearby ;

    private void Start()
    {
        if (tutorialUI != null)
            tutorialUI.SetActive(false); // Đảm bảo ẩn khi bắt đầu
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (tutorialUI != null)
                tutorialUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (tutorialUI != null)
                tutorialUI.SetActive(false);
        }
    }
}
