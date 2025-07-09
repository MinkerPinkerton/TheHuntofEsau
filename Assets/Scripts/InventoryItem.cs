using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform originalParent;


    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (originalParent == null)
            originalParent = (RectTransform)transform.parent;
   

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform, true); // pull out for dragging
        transform.SetAsLastSibling();                // render on top
        canvasGroup.blocksRaycasts = false;
    }


    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<EquipmentSlot>() == null)
        {
            transform.SetParent(originalParent); // Snap back to grid

            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
