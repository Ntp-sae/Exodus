using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButton : MonoBehaviour //IPointerEnterHandler, IPointerExitHandler
{
    
    //private ItemDescription tooltip;
    
    public ItemData data;


    //private void Start()
    //{
    //    tooltip = FindObjectOfType<ItemDescription>();
    //}

    public void AddToInventory()
    {
        InventoryManager.instance.AddItem(data.id);

        Destroy(gameObject, 0.2f);
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    if (data != null) tooltip.ShowTooltip(data.itemDescription);
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    tooltip.HideTooltip();
    //}

    
    

}
