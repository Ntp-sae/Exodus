using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    public string itemDescription;
    public string itemType;
    public Sprite itemIcon;
    public string itemTitle;
    public GameObject itemPrefab;
    public Button itemButton;
    public int minAmount;
    public int maxAmount;
    public int inventoryAmount;

}


[System.Serializable]
public class ItemInstance
{
    public ItemData itemType;
    public int condition;
    public int ammo;

    public ItemInstance(ItemData itemData)
    {
        itemType = itemData;
       
    }
}
