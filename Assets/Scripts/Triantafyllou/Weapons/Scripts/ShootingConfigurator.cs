using UnityEngine;

[CreateAssetMenu (fileName = "Shooting Config", menuName = "Guns/Shoot Configuration", order = 2)]
public class ShootingConfigurator : ScriptableObject
{
    public LayerMask HitMask; // What the weapon hits
    public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.1f); //Bullet Spread
    public float FireRate = 0.25f; // Weapon fire rate

}
