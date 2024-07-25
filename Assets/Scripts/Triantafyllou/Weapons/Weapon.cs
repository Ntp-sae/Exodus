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
    public ParticleSystem muzzleEffect;
    public GameObject impactEffect;

    private bool isFiring = false;
    private bool isReloading = false;

    private float currentAmmo;
    private float nextTimeToFire = 0f;

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
        if(currentAmmo <= 0)
        {
            ReloadWeapon();
            return;
        }
        animationEvents();
    }

    private void StartFiring()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / currentWeaponSO.firingSpeed;
            isFiring = true;
            currentAmmo--;
            Debug.Log("Weapon is Firing!!!!");

            // Instantiate muzzle particle
            muzzleEffect.Play();

            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, currentWeaponSO.range))
            {
                Debug.Log("Hit: " + hit.transform.name);
                //Target target = hit.transform.GetComponent<>(Target);
                //if (target != null)
                //{
                //    target.TakeDamage(damage);
                //}

                GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGo, 2f);
            }
        }
    }

    private void StopFiring()
    {
        Debug.Log("WeaponStopFiring");
        isFiring = false;
        muzzleEffect.Stop();
    }

    private void ReloadWeapon()
    {
        Debug.Log("Weapon is Reloading");
       StartCoroutine(Reloading());
        return;
    }

    private IEnumerator Reloading()
    {
        isReloading = true;

        yield return new WaitForSeconds(currentWeaponSO.baseReloadSpeed - 0.25f);
        isReloading = false;
        yield return new WaitForSeconds(0.25f);

        currentAmmo = currentWeaponSO.maxAmmo;
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
