using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_TranPhamMinhDuc : MonoBehaviour
{
    public string targetScene;
    public string destinationPortalID;

    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.SetString("SpawnPointID", destinationPortalID);
            Debug.Log("Loading scene: " + targetScene);
            SceneManager.LoadScene(targetScene);
        }
    }
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player_TranPhamMinhDuc"))
        {
            Debug.Log("aljnsdlawndlawd");
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player_TranPhamMinhDuc"))
        {
            isPlayerNearby = false;
        }
    }
}
