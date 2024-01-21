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
        Debug.Log(GetComponentInParent<Slot>().GetItem());
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        canvasGroup.blocksRaycasts = false; 
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        item = GetComponentInParent<Slot>().GetItem();
        count = GetComponentInParent<Slot>().GetCount();
        countText = GetComponentInParent<Slot>().countText;
    }
}
