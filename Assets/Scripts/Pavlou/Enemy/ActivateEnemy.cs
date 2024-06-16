using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemy : MonoBehaviour
{
    [SerializeField] NewEnemyController enemy;
    [SerializeField]bool debuger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(debuger)Debug.Log("ActivateEnemy: I detect player");
            enemy.ChasePlayer();
        }
    }

}
