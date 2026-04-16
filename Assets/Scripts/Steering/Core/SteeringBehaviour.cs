using UnityEngine;

[System.Serializable]
public abstract class SteeringBehaviour
{
    [Range(0f, 1f)]
    public float weight = 1f;
    public bool enabled = true;

    public abstract Vector3 GetSteering(SteeringContext ctx);

}
