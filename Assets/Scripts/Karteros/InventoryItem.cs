using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public TMP_Text text;
    public ItemData itemData;
    
    private void Update()
    {
        text.text = itemData.inventoryAmount.ToString();
    }
}
