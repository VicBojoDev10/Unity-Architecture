using UnityEngine;

public class Alignment : MonoBehaviorSteeringBehavior
{
   public override Vector2 GetSteering(SteeringContext ctx)
   {
      return Vector2.zero;
   }
}
