using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Makes the weapon manager open in public
   public static WeaponManager instance { get; private set; }

    public InputReader reader;
    public List<Weapon> WeaponsOnPlayer;
    public GameObject weapon1;
    public GameObject weapon2;
    public Animator weapon1Animator;
    public Animator weapon2Animator;

    private void Awake()
    {
        weapon1.SetActive(true);
        weapon2.SetActive(false);

        // Keeps the same weapon manager at all scenes if there is no other loaded
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        reader.weaponFirstPressEvent += OnEquipFirstWeapon;
        reader.weaponSecondPressEvent += OnEquipSecondWeapon;
    }

    private void OnDisable()
    {
        reader.weaponFirstPressEvent -= OnEquipFirstWeapon;
        reader.weaponSecondPressEvent -= OnEquipSecondWeapon;
    }

    private void OnEquipFirstWeapon()
    {
        weapon2Animator.SetBool("Holster", true);
        weapon2.SetActive(false);
        weapon1.SetActive(true);
        weapon1Animator.SetBool("Equip", true);
    }

    public void OnEquipSecondWeapon()
    {
        weapon1Animator.SetBool("Holster", true);
        weapon1.SetActive(false);
        weapon2.SetActive(true);
        weapon2Animator.SetBool("Equip", true);
    }
}
