using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestTarget : MonoBehaviour
{
    [SerializeField] GameObject DoorMessage;

    private void Start()
    {
        DoorMessage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorMessage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorMessage.SetActive(false);
        }
    }

}
