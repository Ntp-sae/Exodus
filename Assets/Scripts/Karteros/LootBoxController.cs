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
    public bool chestCanOpen;
    public bool chestIsOpen = false;



    void Start()
    {
        chestCanOpen = false;

        for (int i = 0; i < 4; i++)
        {

            item = data[Random.Range(0, data.Count)];
            int quantity = Random.Range(item.minAmount, item.maxAmount);
            Button chestSlot = Instantiate(item.itemButton);


            chestSlot.transform.SetParent(ChestItemsPanel.transform, false);


            chestSlot.GetComponent<ItemButton>().data = item;

            Image image = chestSlot.GetComponentInChildren<Image>();

            if (image != null)
            {
                image.sprite = item.itemIcon;
            }

            Text quantityText = chestSlot.GetComponentInChildren<Text>();

            if (quantityText != null)
            {
                quantityText.text = "x" + quantity.ToString();
                Debug.Log("Text not found!");
            }
        }

           

        

        ChestPanel.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chestCanOpen = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chestCanOpen = false;

            chestIsOpen = false;

            ChestPanel.SetActive(false);

        }
    }




    public void Update()
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

    public bool ChestIsOpen()
    {
        return chestIsOpen;
    }
}
