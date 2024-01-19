using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotSelector;
    private int slotNumber;
    [SerializeField] private List<float> hotbarXValue;
    [SerializeField] private List<Slot> hotbarSlots;
    [SerializeField] private List<Slot> inventorySlots;


    // Start is called before the first frame update
    void Start()
    {
        slotNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SlotScroll();
    }

    private void SlotScroll()
    {
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

        slotSelector.GetComponent<RectTransform>().anchoredPosition = new Vector2(hotbarXValue[slotNumber], 
            slotSelector.GetComponent<RectTransform>().anchoredPosition.y);
    }
}
