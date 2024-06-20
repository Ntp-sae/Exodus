using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDescription : MonoBehaviour
{
    public GameObject tooltipPanel;
    public TMP_Text tooltipText;


    void Start()
    {
        HideTooltip();
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }

    public void ShowTooltip(string itemDescription)
    {
        tooltipText.text = itemDescription;
        tooltipPanel.SetActive(true);
    }
   
}
