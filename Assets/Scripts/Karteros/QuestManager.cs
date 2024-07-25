using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    
    public ItemData data;
    public bool QuestCanStart = false;
    
    
    [SerializeField] GameObject targetDoor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && QuestCanStart == true)
        {
            //UI -> 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered"); 
            QuestCanStart = true;
            
            if (data.inventoryAmount >= 200)
            {
                //UI -> the door is now open

                Destroy(targetDoor, 2f);
                data.inventoryAmount -= 200;
                Debug.Log("Scrap Removed"); 
            }

            else
            {
                Debug.Log("Not enough scrap");
                
                //UI -> you need more scraps
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        QuestCanStart = false;
    }

}
