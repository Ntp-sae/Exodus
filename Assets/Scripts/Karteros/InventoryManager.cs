using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour 
{
    //[SerializeField] PlayerController playerController;
    [SerializeField] GameObject InventoryItemsPanel;
    [SerializeField] GameObject InventoryPanel;

    public bool inventoryOpen = false;

    public int maxItems = 10;

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


    public void AddItem(int itemID)
    {
        foreach (ItemData item in allItems)
        {
            if (item.id == itemID)
            {
                // Finds an empty slot if there is one
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    if (inventoryItems[i] == null)
                    {
                        inventoryItems[i] = item;

                    }
                   
                }

                // Adds a new item if the inventory has space
                if (inventoryItems.Count < maxItems)
                {
                    inventoryItems.Add(item);

                    Button inventorySlot = Instantiate(item.itemButton);

                    inventorySlot.transform.parent = InventoryItemsPanel.transform;

                    inventorySlot.GetComponent<ItemButton>().data = item;

                }

                Debug.Log("No space in the inventory");
            }
        }
        
        

        
       

    }
}
