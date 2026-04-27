using UnityEngine;

public class CollisionAvoidance : SteeringBehaviour
{
    [Header("Posiciones")]
    private Transform target;

    private Transform Neighbor;
    public override Vector2 GetSteering(SteeringContext ctx)
    {
            
        return Vector2.zero;
    }
}
