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

    private TMP_Text countText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
        Debug.Log("Dropped");
        GameObject droppedItem = eventData.pointerDrag;
        DragDrop dd = droppedItem.GetComponent<DragDrop>();

        if (transform.childCount == 0)
        {
            dd.parentAfterDrag = transform;
        }
    }
}
