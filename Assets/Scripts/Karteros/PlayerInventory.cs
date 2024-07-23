using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int bullets;
    public int scrap;
    public int medkit;

    public TMP_Text bulletsText;
    public TMP_Text scrapText;
    public TMP_Text medkitText;
    public static PlayerInventory Instance;
    public GameObject inventoryPanel;

    private bool inventoryOpen;
    private void Awake()
    {
        Instance = this;
    }

    public void AddToInventory(ItemData data ,int amount)
    {
        if(data.itemType == ItemData.ItemType.bullets)
        {
            bullets += amount;
        }
        if(data.itemType == ItemData.ItemType.scrap)
        {
            scrap += amount;
        }
        if(data.itemType == ItemData.ItemType.medkit)
        {
            medkit += amount;
        }
    }

    public void RemoveFromInventory(ItemData data, int amount)
    {
        if (data.itemType == ItemData.ItemType.bullets)
        {
            bullets -= amount;
        }
        if (data.itemType == ItemData.ItemType.scrap)
        {
            scrap -= amount;
        }
        if (data.itemType == ItemData.ItemType.medkit)
        {
            medkit -= amount;
        }
    }


    public void Update()
    {
        bulletsText.text = bullets.ToString();
        scrapText.text = scrap.ToString();
        medkitText.text = medkit.ToString();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryOpen)
            {
                inventoryPanel.SetActive(true);

                Cursor.lockState = CursorLockMode.Confined;

                inventoryOpen = true;
            }

            else
            {
                inventoryPanel.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;

                inventoryOpen = false;
            }
        }

    }
}
