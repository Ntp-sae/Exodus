using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ItemData data; 
    
    public void AddToInventory()
    {
        InventoryManager.instance.AddItem(data.id);

        Destroy(gameObject, 0.2f);
    }
    
    public void ShowMoreInfo()
    {

    }
    

}
