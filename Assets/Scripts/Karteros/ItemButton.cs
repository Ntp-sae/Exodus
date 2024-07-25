using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    //private ItemDescription tooltip;
    
    public ItemData data;
    public TMP_Text amount;
    


    //private void Start()
    //{
    //    tooltip = FindObjectOfType<ItemDescription>();
    //}

    public void AddToInventory()
    {

        PlayerInventory.Instance.AddToInventory(data, data.chestAmount);
        data.inventoryAmount += data.chestAmount;
        

        Destroy(gameObject, 0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Test");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("TestExx");
    }





}
