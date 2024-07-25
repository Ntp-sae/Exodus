using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public InputReader reader;

    public Camera playerCamera;
    [SerializeField] Animator playerAnimator;

    public WeaponManager weaponManager;

    private Weapon currentWeapon;
    public WeaponSO currentWeaponSO;

    private bool isFiring = false;
    private bool isReloading = false;

    private float currentAmmo;

    void OnEnable()
    {
        reader.shootPressEvent += StartFiring;
        reader.shootReleaseEvent += StopFiring;
        reader.reloadEvent += ReloadWeapon;

        isReloading = false;
        playerAnimator.SetBool("isReloading", false);
    }
    void OnDisable()
    {
        reader.shootPressEvent -= StartFiring;
        reader.shootReleaseEvent -= StopFiring;
        reader.reloadEvent -= ReloadWeapon;
    }
    private void Start()
    {
        if (weaponManager == null)
        {
            weaponManager = WeaponManager.instance;
            currentAmmo = currentWeaponSO.maxAmmo;
        }
    }

    private void Update()
    {
        animationEvents();
    }

    private void StartFiring()
    {
        isFiring = true;
        currentAmmo--;
        Debug.Log("Weapon is Firing!!!!");

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

    private void StopFiring()
    {
        Debug.Log("WeaponStopFiring");
        isFiring = false;
    }

    private void ReloadWeapon()
    {
        Debug.Log("Weapon is Reloading");
       StartCoroutine(Reloading());
    }

    private IEnumerator Reloading()
    {
        isReloading = true;
        
        yield return new WaitForSeconds(currentWeaponSO.baseReloadSpeed);
        currentAmmo = currentWeaponSO.maxAmmo;

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
        if (isFiring)
        {
            playerAnimator.SetBool("isFiring", true);
            playerAnimator.SetBool("isReloading", false);
            if (!isReloading)
            {
                playerAnimator.SetBool("isFiring", true);
                playerAnimator.SetBool("isReloading", false);
            }
        }
        else if (isReloading)
        {
            playerAnimator.SetBool("isReloading", true);
            playerAnimator.SetBool("isFiring", false);
            if (!isFiring)
            {
                playerAnimator.SetBool("isReloading", true);
                playerAnimator.SetBool("isFiring", false);
            }
        }
        else
        {
            playerAnimator.SetBool("isReloading", false);
            playerAnimator.SetBool("isFiring", false);
        }
    }
}
