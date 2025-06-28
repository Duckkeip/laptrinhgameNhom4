using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Tab bat Inven
public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public GameObject inventoryPanel;
    public Transform itemSlotParent;
    public GameObject itemSlotPrefab;
    private bool isOpen = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnEnable()
    {
        if (Inventory.Instance != null)
        Inventory.Instance.onInventoryChangedCallback += OnInventoryChanged;
         Time.timeScale = 1f; 
    }

    private void OnDisable()
    {
        if (Inventory.Instance != null)
        Inventory.Instance.onInventoryChangedCallback -= OnInventoryChanged;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);
            Time.timeScale = isOpen ? 0f : 1f;
            if (isOpen) RefreshUI();
        }
    }
     void OnInventoryChanged()
    {
        if (isOpen) RefreshUI(); // Chỉ refresh nếu đang mở
    }
    
    public void RefreshUI()
    {
        foreach (Transform child in itemSlotParent)
        {
            
            Destroy(child.gameObject);
        }

      foreach (InventoryItem item in Inventory.Instance.items)
        {
                GameObject slot = Instantiate(itemSlotPrefab, itemSlotParent);

                ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
                itemSlot.itemData = item.data;

                Image iconImage = slot.transform.Find("Icon").GetComponent<Image>();
                TMP_Text nameText = slot.GetComponentInChildren<TMP_Text>();

                iconImage.sprite = item.data.icon;
                nameText.text = item.data.itemName + " x" + item.quantity;
        }
    }
   
}
