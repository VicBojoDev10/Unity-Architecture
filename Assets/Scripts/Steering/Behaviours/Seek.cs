using UnityEngine;

[System.Serializable]
public class Seek : SteeringBehaviour
{
    public Transform target;

    public override Vector3 GetSteering(SteeringContext ctx)
    {
        if(target == null || !enabled) return Vector2.zero;

        Vector3 targetPos = target.position;
        Vector3 desired = (targetPos - ctx.position).normalized 
                          * ctx.maxSpeed;
        return desired - ctx.velocity;
    }    
    
}
