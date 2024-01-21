using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<Slot> hotbarSlots;
    [SerializeField] private List<Slot> playerSlots;
    [SerializeField] private List<GameObject> slotSelectors;
    [SerializeField] private GameObject itemInSlot;

    private int slotNumber;

    #region debug

    public Button addSword;
    public Item sword;
    public GameObject inventory;
    public Button addApple;
    public Item apple;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        slotNumber = 0;
        for (int i = 0; i < slotSelectors.Count; i++)
        {
            if (i == 0)
                slotSelectors[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            slotSelectors[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        addSword.onClick.AddListener(delegate {AddItem(sword, 1, playerSlots);});
        addApple.onClick.AddListener(delegate {AddItem(apple, 1, playerSlots);});
    }

    // Update is called once per frame
    void Update()
    {
        SlotScroll();
    }

    private void SlotScroll()
    {
        slotSelectors[slotNumber].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            if (slotNumber == 0) {
                slotNumber = 6;
            } else {
                slotNumber--;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            if (slotNumber == 6) {
                slotNumber = 0;
            } else {
                slotNumber++;
            }
        }
        slotSelectors[slotNumber].GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    private void AddItem(Item item, int count, List<Slot> slots)
    {   
        foreach (Slot slot in slots)
        {   
            Item currentItem = slot.GetItem();
            if (slot.transform.childCount != 0 && currentItem.itemName == item.itemName) 
            {
                if (currentItem.stackable && slot.GetCount() < currentItem.maxStack)
                {
                    UpdateSlot(slot, item, count);
                    return;
                } 
            }
        }

        foreach (Slot slot in slots)
        {   
            Item currentItem = slot.GetItem();
            if (slot.transform.childCount == 0)
            {
                slot.SetItem(item);
                UpdateSlot(slot, item, count);
                break;
            }
        }
    }

    private void UpdateSlot(Slot slot, Item item, int count)
    {
        if (slot.transform.childCount == 0) 
        {
            GameObject tempItem = Instantiate(itemInSlot);
            tempItem.transform.SetParent(slot.transform);
            tempItem.GetComponent<Image>().sprite = item.sprite;
            tempItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        slot.UpdateCount(count);
    }
}   

