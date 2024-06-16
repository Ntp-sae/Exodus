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
    public Image itemIcon;
    public string itemTitle;
    public GameObject itemPrefab;
    public Button itemButton;

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