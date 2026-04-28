using UnityEngine;

public class PathFollowing : MonoBehaviorSteeringBehavior
{
    public override Vector2 GetSteering(SteeringContext ctx)
    {
        return Vector2.zero;
    }
}
