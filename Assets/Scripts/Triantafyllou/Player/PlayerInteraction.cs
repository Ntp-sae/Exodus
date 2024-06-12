using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script need to change to Interface !!!!!!!!
public class PlayerInteraction : MonoBehaviour
{
    public GameObject spawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickableItem"))
        {
            int pickableWeaponID = other.gameObject.GetComponent<WeaponID>().ID;

            for (int i = 0; i < WeaponManager.instance.WeaponsOnLevel.Count; i++)
            {
                if (WeaponManager.instance.WeaponsOnLevel[i].ID == pickableWeaponID)
                {
                    GameObject spawnedWeapon = Instantiate(WeaponManager.instance.WeaponsOnLevel[i].WeaponPrefab);
                    spawnedWeapon.transform.SetParent(spawnPoint.transform, false);
                    spawnedWeapon.transform.localPosition = WeaponManager.instance.WeaponsOnLevel[i].SpawnPosition;
                    spawnedWeapon.transform.localRotation = Quaternion.Euler
                        (
                        WeaponManager.instance.WeaponsOnLevel[i].SpawnRotation.x,
                        WeaponManager.instance.WeaponsOnLevel[i].SpawnRotation.y,
                        WeaponManager.instance.WeaponsOnLevel[i].SpawnRotation.z
                        );

                    Destroy(other.gameObject);
                }
            }
        }
    }
}