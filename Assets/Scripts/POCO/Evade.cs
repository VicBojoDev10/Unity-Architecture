using UnityEngine;

[System.Serializable]
public class Evade : SteeringBehaviour
{
    public Transform evadeTarget;
    public override Vector2 GetSteering(SteeringContext ctx)
    {
        if(evadeTarget == null || !enabled) return Vector2.zero;
        
        
        return Vector2.zero;
    }
}