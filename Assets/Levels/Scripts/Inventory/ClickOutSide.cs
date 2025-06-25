using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOutside : MonoBehaviour, IPointerClickHandler
{
    public GameObject popupToHide; // Gán ở Inspector: là ItemOptionsPopup

    public void OnPointerClick(PointerEventData eventData)
    {
        // Nếu click không phải vào nút con
        if (!IsClickOnUIElement(eventData))
        {
            popupToHide.SetActive(false);
            gameObject.SetActive(false); // Ẩn cả panel nền
        }
    }

    private bool IsClickOnUIElement(PointerEventData eventData)
    {
        // Kiểm tra xem click có rơi vào một nút, text hoặc element con nào không
        return eventData.pointerEnter != null &&
               eventData.pointerEnter.transform.IsChildOf(popupToHide.transform);
    }
}
