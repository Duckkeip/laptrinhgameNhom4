using UnityEngine;
using UnityEngine.UI;

public class ItemOptionsPopup : MonoBehaviour
{
    private ItemData currentItem;
    private ItemSlot currentSlot;

    public Button useButton;
    public Button dropButton;
    public Button cancelButton; // thêm dòng này


    private void OnEnable()
    {
        // Reset listeners để tránh thêm nhiều lần
        useButton.onClick.RemoveAllListeners();
        dropButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners(); // thêm dòng này



        cancelButton.onClick.AddListener(OnCancel);
        useButton.onClick.AddListener(OnUse);
        dropButton.onClick.AddListener(OnDrop);
    }

    public void SetCurrentItem(ItemData item, ItemSlot slot)
    {
        currentItem = item;
        currentSlot = slot;
    }

   private void OnUse()
        {
            if (currentItem == null)
                {
                    Debug.LogError("currentItem is null! Đảm bảo đã gọi SetCurrentItem() trước khi dùng.");
                    return;
                }
            Debug.Log("Dùng " + currentItem.itemName);

            // Tìm player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PStats stats = player.GetComponent<PStats>();
                if (stats != null)
                {
                    // Hồi máu
                    if (currentItem.healAmount > 0)
                    {
                        stats.RegenHP(currentItem.healAmount);
                    }

                    // Hồi stamina
                    if (currentItem.staminaAmount > 0)
                    {
                        stats.RegenSTA(currentItem.staminaAmount);
                    }
                    if (currentItem.manaAmount > 0)
                    {
                        stats.RegenMP(currentItem.manaAmount);
                    }
                }
            }
            Inventory.Instance.RemoveItem(currentItem);
            gameObject.SetActive(false);
}

    private void OnDrop()
    {
        Debug.Log("Bỏ " + currentItem.itemName);
        Inventory.Instance.RemoveItem(currentItem);
        // TODO: Thêm logic xoá item khỏi inventory
             if (InventoryUI.Instance != null)
        {
            InventoryUI.Instance.RefreshUI();
        }
        gameObject.SetActive(false);
     
    }
    private void OnCancel()
{
    gameObject.SetActive(false); // chỉ đơn giản là tắt popup
}
}
