using UnityEngine;

[CreateAssetMenu(fileName = "Trail Config", menuName = "Guns/Gun Trail Config", order = 4)]
public class TrailConfigurator : ScriptableObject
{
    public Material Material;
    public AnimationCurve WidthCurve;
    public Gradient Color;
    public float Duration = 0.5f;
    public float MinVertexDistance = 0.1f;

    public float MissDistance = 100f;
    public float SimulationSpeed = 100f;

}
