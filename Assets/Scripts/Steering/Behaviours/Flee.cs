using UnityEngine;
[System.Serializable]
public class Flee : SteeringBehaviour
{
    public Transform target;
    public float panicDistance = 5f;

    public override Vector2 GetSteering(SteeringContext ctx)
    {
        if (target == null || !enabled) return Vector3.zero;

        Vector2 targetPos = target.position;
        float dist = Vector3.Distance(ctx.position, targetPos);
        if (dist > panicDistance) return Vector3.zero;

        Vector2 desired = (ctx.position - targetPos).normalized * ctx.maxSpeed;
        return desired - ctx.velocity;
    }
}
