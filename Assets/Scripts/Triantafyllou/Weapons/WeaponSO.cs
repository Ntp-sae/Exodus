using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Weapon", order = 0)]
public class WeaponSO : ScriptableObject
{
    public enum GunType
    {
        Melee,
        Pistol,
        Grenade,
        Shotgun,
        Smg,
        Rifle,
        SniperRifle,
        Lmg,
        Explosives
    }

    [Header("General Info")]
    public int ID;
    public string name;
    public string description;
    public Sprite WeaponUiIcon;
    //public GameObject WeaponPrefab;
    //public ParticleSystem MuzzleParticle;

    //[Header("Weapon offset")]
    //public Vector3 SpawnPosition;
    //public Vector3 SpawnRotation;

    [Header("Weapon stats")]
    public float range;
    public float baseDamage;
    public float firingSpeed;
    public float baseReloadSpeed;
    public float maxAmmo;
    public float recoil;
    public Vector3 bulletSpread;

}