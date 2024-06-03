using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField] private GunType Gun;
    [SerializeField] private Transform GunParent;
    [SerializeField] private List<Gun> Guns;
    //[SerializeField] private PlayerIK InverseKinematics;

    public Gun ActiveGun;

    private void Start()
    {
        Gun gun = Guns.Find(gun => gun.Type == Gun);

        if (gun == null)
        {
            Debug.Log($"No Gun Found for GunType: {gun}");
            return;
        }

        ActiveGun = gun;
        gun.Spawn(GunParent, this);

        //Implement IK System
    }

}
