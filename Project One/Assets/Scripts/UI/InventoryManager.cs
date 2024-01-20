using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<float> hotbarXValue;
    [SerializeField] private List<Slot> hotbarSlots;
    [SerializeField] private List<Slot> inventorySlots;
    [SerializeField] private List<GameObject> slotSelectors;

    private int slotNumber;

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
}

