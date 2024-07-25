using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public Sprite itemIcon;
    public string itemTitle;
    public GameObject itemPrefab;
    public Button itemButtonChest;
    public Button inventoryButton; 
    public int minAmount;
    public int maxAmount;
    public int chestAmount;
    public int inventoryAmount;

    public enum ItemType
    {
        bullets,
        scrap,
        medkit,
        quest
    }
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
