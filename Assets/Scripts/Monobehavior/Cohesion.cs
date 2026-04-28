using UnityEngine;

public class Cohesion : MonoBehaviorSteeringBehavior
{
    public override Vector2 GetSteering(SteeringContext ctx)
    {
        return Vector2.zero;
    }
}
