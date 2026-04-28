using UnityEngine;

public class Separation : MonoBehaviorSteeringBehavior
{
    public override Vector2 GetSteering(SteeringContext ctx)
    {
        return Vector2.zero;
    }
}
