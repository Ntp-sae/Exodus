using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject _bullet; // Prefab of the bullet
    public Transform bulletSpawn; // Position that the bullet instatiates
    public float bulletVelocity = 30f; // Speed of a bullet
    public float bulletLifeTime = 3f;


    void Update()
    {
        // Gets Imput to do an action
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        // Instantiates the bullet
        GameObject bullet = Instantiate(_bullet, bulletSpawn.position, Quaternion.identity);
        // Shoots the bullet
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        // Destroy the bullet
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifeTime));


    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
