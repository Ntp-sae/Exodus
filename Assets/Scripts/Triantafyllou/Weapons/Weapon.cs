using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public InputReader reader;

    public Camera playerCamera;
    [SerializeField] Animator playerAnimator;

    public Transform weaponHolder;
    public WeaponManager weaponManager;

    private Weapon currentWeapon;
    private WeaponSO currentWeaponSO;

    private bool isFiring = false;
    private bool isReloading = false;

    private Vector3 originalWeaponPosition;
    public float swayAmount = 0.02f;
    public float swaySmoothness = 2.0f;


    void OnEnable()
    {
        reader.shootEvent += FireWeapon;
    }
    void OnDisable()
    {
        reader.shootEvent -= FireWeapon;
    }
    private void Start()
    {
        if (weaponManager == null)
        {
            weaponManager = WeaponManager.instance;
        }
    }

    private void Update()
    {
        if (isFiring && currentWeapon != null && !isReloading)
        {
            FireWeapon();
        }
        animationEvents();
    }

    private void FireWeapon()
    {
        isFiring = true;

        // Add logic for firing the weapon
        Debug.Log("Firing " + currentWeaponSO.name);

        // Instantiate muzzle particle
        if (currentWeaponSO.MuzzleParticle != null)
        {
            Instantiate(currentWeaponSO.MuzzleParticle, currentWeapon.transform.position, Quaternion.identity);
        }

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, currentWeaponSO.range))
        {
            Debug.Log("Hit: " + hit.transform.name);
           // Add damage to Enemy
        }
    }
    private IEnumerator ReloadWeapon()
    {
        isReloading = true;
        playerAnimator.SetBool("isReloading", true);

        yield return new WaitForSeconds(currentWeaponSO.baseReloadSpeed);

        isReloading = false;
    }

    private void EquipWeapon()
    {

    }

    public void OnSwitchWeapon()
    {

    }

    private void animationEvents()
    {
        if (isFiring && !isReloading)
        {
            playerAnimator.SetBool("isFiring", true);
        }
        else if (isReloading)
        {
            playerAnimator.SetBool("isReloading", true);
        }
        else
        {
            return;
        }
    }
}
