using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }


    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        if (droppedItem != null)
        {
            droppedItem.transform.SetParent(transform);
            
            droppedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            droppedItem.transform.SetAsLastSibling(); // draw on top in slot
        }
    }



}

