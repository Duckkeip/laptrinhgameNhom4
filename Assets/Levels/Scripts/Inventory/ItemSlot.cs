using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public ItemData itemData;
    public GameObject itemOptionsPopup; // kéo từ Canvas vào

    public void OnPointerClick(PointerEventData eventData)
    {
          if (itemData == null)
            {
                Debug.LogWarning("Slot chưa có itemData!");
                return;
            }

        Debug.Log("Đã click vào slot có item: " + (itemData != null ? itemData.itemName : "null"));
        
        itemOptionsPopup.SetActive(true);
        itemOptionsPopup.transform.position = Input.mousePosition;

        itemOptionsPopup.GetComponent<ItemOptionsPopup>().SetCurrentItem(itemData, this);
    }
}
