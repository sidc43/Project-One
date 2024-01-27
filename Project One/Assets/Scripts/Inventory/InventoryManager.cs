using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Slot fields
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

    void Start()
    {
        // Initialize slot number and set slot selectors
        slotNumber = 0;
        InitializeSelectors();

        // Debug
        AssignDebugButtons();
    }

    void Update()
    {
        SlotScroll();
    }
    public Item GetActiveItem()
    {
        return hotbarSlots[slotNumber].GetItem();
    }
    private void SlotScroll()
    {
        // Hide slot selector
        slotSelectors[slotNumber].GetComponent<Image>().color = new Color(1, 1, 1, 0);

        // Scroll forward
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            if (slotNumber == 0) {
                slotNumber = slotSelectors.Count - 1;
            } else {
                slotNumber--;
            }
        }

        // Scroll backward
        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            if (slotNumber == slotSelectors.Count - 1) {
                slotNumber = 0;
            } else {
                slotNumber++;
            }
        }

        // Show slot selector
        slotSelectors[slotNumber].GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
    public void AddItem(Item item, int count, List<Slot> slots)
    {
        // Check for stackable items in existing slots
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount > 0 && slot.GetItem().stackable && item.name == slot.GetItem().itemName)
            {
                int spaceLeftInSlot = slot.GetItem().maxStack - slot.GetCount();

                if (count <= spaceLeftInSlot)
                {
                    UpdateSlot(slot, item, count);
                    return;
                }
                else
                {
                    UpdateSlot(slot, item, spaceLeftInSlot);
                    count -= spaceLeftInSlot;
                }
            }
        }

        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount > 0 && slot.GetItem().stackable && item.name == slot.GetItem().itemName)
            {
                int spaceLeftInSlot = slot.GetItem().maxStack - slot.GetCount();

                if (count <= spaceLeftInSlot)
                {
                    UpdateSlot(slot, item, count);
                    return;
                }
            }
        }

        // If no item of same type was found add to first empty slot
        foreach (Slot slot in slots)
        {
            if (slot.transform.childCount == 0)
            {
                slot.SetItem(item);
                UpdateSlot(slot, item, count);
                return;
            }
        }
    }
    private void UpdateSlot(Slot slot, Item item, int count)
    {
        // Update item count and image in slot
        if (slot.transform.childCount == 0) 
        {
            GameObject tempItem = Instantiate(itemInSlot);
            tempItem.transform.SetParent(slot.transform);
            tempItem.GetComponent<Image>().sprite = item.sprite;
            tempItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        slot.UpdateCount(count);
    }
    private void AssignDebugButtons()
    {
        addSword.onClick.AddListener(delegate { AddItem(sword, 1, playerSlots); });
        addApple.onClick.AddListener(delegate { AddItem(apple, 5, playerSlots); });
    }
    private void InitializeSelectors()
    {
        for (int i = 0; i < slotSelectors.Count; i++)
        {
            if (i == 0)
                slotSelectors[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            slotSelectors[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }
}   