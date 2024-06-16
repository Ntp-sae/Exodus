using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootBoxController : MonoBehaviour
{
    [SerializeField] GameObject ChestItemsPanel;
    [SerializeField] GameObject ChestPanel;
    [SerializeField] List<ItemData> data;
    private ItemData item;
    public bool chestCanOpen = false;
    public bool chestIsOpen = false;

    void Start()
    {

        for (int i = 0; i < 10; i++)
        {

            item = data[Random.Range(0, data.Count)];
            Button chestSlot = Instantiate(item.itemButton);

            chestSlot.transform.parent = ChestItemsPanel.transform;
            chestSlot.GetComponent<ItemButton>().data = item;
 
        }

        ChestPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chestCanOpen=true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chestCanOpen=false;

            chestIsOpen = false;

            ChestPanel.SetActive(false);

        }
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && chestCanOpen && !chestIsOpen)
        {

            ChestPanel.SetActive(true);

            Cursor.lockState = CursorLockMode.Confined;

            chestIsOpen = true;
            
        }

        else if (Input.GetKeyDown(KeyCode.E) && chestCanOpen && chestIsOpen)
        {
            ChestPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;

            chestIsOpen = false;

        }

        


    }
}
