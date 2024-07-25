using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //[SerializeField] PlayerController playerController;
    [SerializeField] GameObject InventoryItemsPanel;
    [SerializeField] GameObject InventoryPanel;


    public bool inventoryOpen = false;

    public int maxItems;

    public static InventoryManager instance;

    public List<ItemData> inventoryItems = new List<ItemData>();

    public List<ItemData> allItems = new List<ItemData>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        

        //playerController = GetComponent<PlayerController>();
        InventoryPanel.SetActive(false);


    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {



            if (!inventoryOpen)
            {
                InventoryPanel.SetActive(true);

                Cursor.lockState = CursorLockMode.Confined;

                inventoryOpen = true;
            }

            else
            {
                InventoryPanel.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;

                inventoryOpen = false;
            }





        }



    }


    //public void AddItem(int itemID, int amount)
    //{
    //    foreach (ItemData item in allItems)
    //    {
    //        if (item.id == itemID)
    //        {
    //            item.inventoryAmount += item.chestAmount;
    //            // Finds an empty slot if there is one
    //            for (int i = 0; i < inventoryItems.Count; i++)
    //            {
    //                if (inventoryItems[i] == null)
    //                {
    //                    inventoryItems[i] = item;
    //                    //item.inventoryAmount += item.chestAmount;

    //                }

    //                if (inventoryItems[i] != null)
    //                {
    //                    //inventoryItems[i].inventoryAmount += amount;
    //                }

    //            }



    //            // Adds a new item if the inventory has space
    //            if (inventoryItems.Count < maxItems)
    //            {
    //                inventoryItems.Add(item);

    //                Button inventorySlot = Instantiate(item.inventoryButton);

    //                inventorySlot.transform.SetParent(InventoryItemsPanel.transform, false);

    //                if(inventorySlot.GetComponent<ItemButton>() != null)
    //                {
    //                    inventorySlot.GetComponent<ItemButton>().data = item;
    //                    inventorySlot.GetComponentInChildren<TMP_Text>().text = item.inventoryAmount.ToString();
    //                    Image image = inventorySlot.GetComponentInChildren<Image>();
    //                    if (image != null)
    //                    {
    //                        image.sprite = item.itemIcon;
    //                    }
    //                }
                    

    //                if(inventorySlot.GetComponentInChildren<InventoryItem>() != null)
    //                {
    //                    inventorySlot.GetComponent<InventoryItem>().itemData = item;
    //                    Image image = inventorySlot.GetComponentInChildren<Image>();
    //                    if (image != null)
    //                    {
    //                        image.sprite = item.itemIcon;
    //                    }
    //                }

                    

                    

    //            }
    //            else
    //            {
    //                Debug.Log("No space in the inventory");
    //            }

                
    //        }
    //    }
    //}
}
