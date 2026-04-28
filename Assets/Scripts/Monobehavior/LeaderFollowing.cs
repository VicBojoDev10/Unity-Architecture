using UnityEngine;

public class LeaderFollowing : MonoBehaviorSteeringBehavior
{
    public override Vector2 GetSteering(SteeringContext ctx)
    {
        return Vector2.zero;
    }
}
