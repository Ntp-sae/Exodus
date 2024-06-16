using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DynamicInventory : MonoBehaviour 
{
    public int maxItems = 10;

    public List<ItemInstance> items = new List<ItemInstance>();

    public void AddItem(ItemInstance itemToAdd)
    {
        // Finds an empty slot if there is one
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                
            }
        }

        // Adds a new item if the inventory has space
        if (items.Count < maxItems)
        {
            items.Add(itemToAdd);
            
        }

        Debug.Log("No space in the inventory");
        
    }
}
