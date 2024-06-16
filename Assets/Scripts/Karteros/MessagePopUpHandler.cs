using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePopUpHandler : MonoBehaviour
{

    MeshRenderer newMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        newMeshRenderer = GetComponent<MeshRenderer>();
        newMeshRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            newMeshRenderer.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            newMeshRenderer.enabled = false;
        }
    }
}
