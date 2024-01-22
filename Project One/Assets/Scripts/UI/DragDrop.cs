using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image image;
    public Transform parentAfterDrag;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    public Item item;
    public int count;
    public TMP_Text countText;

    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Canvas>();
        image = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        // Block raycasts to unity can see what is under the item
        image.raycastTarget = false;
        canvasGroup.blocksRaycasts = false; 
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Move the item in relation to canvas scale
        this.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Set the parent to the slot the item has been dropped on
        transform.SetParent(parentAfterDrag);

        // Enable raycasts so that the item is pickupable again
        image.raycastTarget = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Get item data to move to the target slot
        item = GetComponentInParent<Slot>().GetItem();
        count = GetComponentInParent<Slot>().GetCount();
        countText = GetComponentInParent<Slot>().countText;

        Debug.Log(transform.parent.name);
    }
}
