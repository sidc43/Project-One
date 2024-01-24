using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Item item;
    [SerializeField] private int count;
    public TMP_Text countText;

    void Update()
    {
        if (this.transform.childCount == 0)
        {
            item = null;
            count = 0;
        }
    }

    public void UpdateCount(int countIncrement)
    {
        this.count += countIncrement;
        if (this.transform.childCount > 0 && this.item.stackable && this.count > 1)
        {
            countText = this.transform.GetComponentInChildren<TMP_Text>();
            countText.text = count + "";
        }
    }
    public Item GetItem()
    {
        return this.item;
    }
    public int GetCount()
    {
        return this.count;
    }
    public void SetItem(Item item)
    {
        this.item = item;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        NewDragDrop dd = droppedItem.GetComponent<NewDragDrop>();

        if (transform.childCount == 0)
        {
            dd.parentAfterDrag = transform;
        }

        // TODO: Stack and split if same item
        if (transform.childCount > 0)
        {
            if (dd.item == this.item)
            {
                Debug.Log("Same");
            }
        }
    }
}
