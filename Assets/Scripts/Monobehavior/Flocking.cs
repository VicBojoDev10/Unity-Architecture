using UnityEngine;

public class Flocking : MonoBehaviorSteeringBehavior
{
    public override Vector2 GetSteering(SteeringContext ctx)
    {
        return Vector2.zero;
    }
}
