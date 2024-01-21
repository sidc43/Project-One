using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ToggleInventory();

        if (inventory.activeSelf)
        {
            Time.timeScale = 0;
        } else 
        {
            Time.timeScale = 1;
        }
    }

    private void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.SetActive(!inventory.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && inventory.activeSelf) 
        {
            inventory.SetActive(false);
        }
    }
}
