using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu (fileName = "Gun", menuName = "Guns/Gun", order = 0)]
public class Gun : ScriptableObject
{
    public GunType Type;
    public string Name;
    public GameObject ModelPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;

    public GameObject BulletSpawnPoint; //Edw exei paixtei malakia, mou bgazei Type Mismatch.

    public ShootingConfigurator ShootConfig;
    public TrailConfigurator TrailConfig;

    private MonoBehaviour ActiveMonoBehavior;
    private GameObject Model;
    private ParticleSystem ShootSystem;
    private ObjectPool<TrailRenderer> TrailPool;

    private float LastShootTime;

    public void Spawn(Transform Parent, MonoBehaviour ActiveMonoBehavior)
    {
        this.ActiveMonoBehavior = ActiveMonoBehavior;
        LastShootTime = 0; // in build it will properly reset;
        TrailPool = new ObjectPool<TrailRenderer>(CreateTrail);
        Model = Instantiate(ModelPrefab);
        Model.transform.SetParent(Parent, false);
        Model.transform.localPosition = SpawnPoint;
        Model.transform.localRotation = Quaternion.Euler(SpawnRotation);

        ShootSystem = Model.GetComponentInChildren<ParticleSystem>();
    }

    public void Shoot()
    {
        Debug.Log("Soot Method initiated");
        if (Time.deltaTime > ShootConfig.FireRate + LastShootTime)
        {
            Debug.Log("Starting to shoot");
            LastShootTime = Time.deltaTime;
            ShootSystem.Play();

            Vector3 shootDirection = BulletSpawnPoint.transform.forward
                + new Vector3(
                    Random.Range(-ShootConfig.Spread.x, ShootConfig.Spread.x),
                    Random.Range(-ShootConfig.Spread.y, ShootConfig.Spread.y),
                    Random.Range(-ShootConfig.Spread.z, ShootConfig.Spread.z)
                    );
            shootDirection.Normalize();

            if (Physics.Raycast(BulletSpawnPoint.transform.position, shootDirection, out RaycastHit hit, float.MaxValue, ShootConfig.HitMask))
            {
                Debug.Log("if1");
                ActiveMonoBehavior.StartCoroutine(
                    PlayTrail(BulletSpawnPoint.transform.position, hit.point, hit)
                    );
            }
            else
            {
                Debug.Log("if2");
                ActiveMonoBehavior.StartCoroutine(
                    PlayTrail(BulletSpawnPoint.transform.position, BulletSpawnPoint.transform.position
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
            instance.transform.position = Vector3.Lerp(StartPoint, EndPoint, Mathf.Clamp01(1- (remainingDistance / distance)));
            remainingDistance -= TrailConfig.SimulationSpeed * Time.deltaTime;

            yield return  null;
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

}
