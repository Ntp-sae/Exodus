using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ScriptsToUse : MonoBehaviour
{
    [Header("Fire Weapon Functions")]
    float FireRate; //Firerate of the weapon
    float LastShootTime; // Last time the weapon fired
    ParticleSystem ParticleSystem; // Particle system to spawn in front of a weapon
    GameObject Muzzle; //Empty Game Objecty to spawn the bullets
    Vector3 Spread; // Bullet spread
    LayerMask HitMask; // What the weapon hit
    MonoBehaviour ActiveMonoBehavior; //To call CoRoutine


    [Header("Trail Functions")]
    TrailConfigurator TrailConfig; // Bullet Trail
    ObjectPool<TrailRenderer> TrailPool; // Trail Pool

    [Header("Reload Functions")]
    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;

    private void Start()
    {
        TrailPool = new ObjectPool<TrailRenderer>(CreateTrail); // For trail renderer

        currentAmmo = maxAmmo; // For reload
    }

    public void Fire()
    {

        
        Debug.Log("Soot Method initiated");
        if (Time.deltaTime > FireRate + LastShootTime)
        {
            Debug.Log("Starting to shoot");
            LastShootTime = Time.deltaTime;
            ParticleSystem.Play();

            Vector3 shootDirection = Muzzle.transform.forward
                + new Vector3(
                    Random.Range(-Spread.x, Spread.x),
                    Random.Range(-Spread.y, Spread.y),
                    Random.Range(-Spread.z, Spread.z)
                    );
            shootDirection.Normalize();

            if (Physics.Raycast(Muzzle.transform.position, shootDirection, out RaycastHit hit, float.MaxValue, HitMask))
            {
                Debug.Log("if1");
                ActiveMonoBehavior.StartCoroutine(
                    PlayTrail(Muzzle.transform.position, hit.point, hit)
                    );
            }
            else
            {
                Debug.Log("if2");
                ActiveMonoBehavior.StartCoroutine(
                    PlayTrail(Muzzle.transform.position, Muzzle.transform.position
                    + (shootDirection * TrailConfig.MissDistance), new RaycastHit())
                    );

            }
        }
    }
    private IEnumerator PlayTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit Hit)
    {
        Debug.Log("Trail Played");
        TrailRenderer instance = TrailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = StartPoint;

        yield return null; // Avoids the move of last position from last fram if reused

        instance.emitting = true;

        float distance = Vector3.Distance(StartPoint, EndPoint);
        float remainingDistance = distance;
        while (remainingDistance > 0)
        {
            instance.transform.position = Vector3.Lerp(StartPoint, EndPoint, Mathf.Clamp01(1 - (remainingDistance / distance)));
            remainingDistance -= TrailConfig.SimulationSpeed * Time.deltaTime;

            yield return null;
        }

        instance.transform.position = EndPoint;

        if (Hit.collider != null)
        {
            //Add impact effects
            //SurfaceManager.Instance.HandleImpact();
        }

        yield return new WaitForSeconds(TrailConfig.Duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        TrailPool.Release(instance);
    }
    private TrailRenderer CreateTrail()
    {
        Debug.Log("Trail Created");
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = TrailConfig.Color;
        trail.material = TrailConfig.Material;
        trail.widthCurve = TrailConfig.WidthCurve;
        trail.time = TrailConfig.Duration;
        trail.minVertexDistance = TrailConfig.MinVertexDistance;

        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }
    public void Reload()
    {
        
    }

}
