using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image image;
    public Transform parentAfterDrag;
    [SerializeField] private Canvas canvas;
    //private Item item;

    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Canvas>();
        image = GetComponent<Image>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {   
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
