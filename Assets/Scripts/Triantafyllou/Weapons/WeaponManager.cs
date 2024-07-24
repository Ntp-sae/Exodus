using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Makes the weapon manager open in public
   public static WeaponManager instance { get; private set; }

    public List<Weapon> WeaponsOnPlayer;

    private void Awake()
    {
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
}
