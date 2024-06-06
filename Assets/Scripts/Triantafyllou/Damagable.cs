using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public float health = 100f;  // Starting health for the enemy
    public float maxHealth = 100f;  // Maximum health for the enemy

    // Method to take damage
    public void TakeDamage(float damage)
    {
        // Reduce the enemy's health by the damage amount
        health -= damage;

        // Ensure health does not fall below zero
        if (health < 0)
        {
            health = 0;
        }

        // Check if the enemy's health is zero or less and trigger death if true
        if (health <= 0)
        {
            Die();
        }
    }

    // Method to handle the enemy's death
    private void Die()
    {
        // Here you can add what should happen when the enemy dies, e.g., play a death animation, drop loot, etc.
        Debug.Log("Enemy died!");

        // Destroy the enemy game object
        Destroy(gameObject);
    }
}
