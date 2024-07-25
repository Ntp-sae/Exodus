using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerWeaponAction : MonoBehaviour
{
    [SerializeField] private PlayerGunSelector GunSelector;

    public InputReader reader;

    private void OnEnable()
    {
        reader.shootEvent += Shooting;
    }    private void OnDisable()
    {
        reader.shootEvent -= Shooting;
    }

    private void Shooting()
    {
        if (GunSelector.ActiveGun != null)
        {
            GunSelector.ActiveGun.Shoot();
        }
    }
}
