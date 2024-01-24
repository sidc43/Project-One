using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class NewDragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    public Item item;
    public int count;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;

        Slot droppedSlot = transform.parent.GetComponent<Slot>();
        droppedSlot.SetItem(item);
        droppedSlot.UpdateCount(count);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Get item data to move to the target slot
        item = GetComponentInParent<Slot>().GetItem();
        count = GetComponentInParent<Slot>().GetCount();
    }
}
