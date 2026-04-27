using UnityEngine;

[System.Serializable]
public class Evade : SteeringBehaviour
{ 
    [Header("Agent&EvaderPositions")]
    public Transform agentPos;
    public Transform evadeTarget;
    [Header("Vectores")]
    private Vector2 _targetVelocity;
    public override Vector2 GetSteering(SteeringContext ctx)
    {
        if(evadeTarget == null || !enabled) return Vector2.zero;
        
        TargetVelocity();
        Vector2 evader = evadeTarget.position;
        Vector2 agent = agentPos.position;
        
        float distance = Vector2.Distance(agent, evader);
        float t = distance / ctx.maxSpeed;


        Vector2 futurePos = agent + _targetVelocity * t; 
        Vector2 desired = (evader - futurePos).normalized * ctx.maxSpeed;
        Vector2 steering = desired - _targetVelocity;
        return steering;
        
    }
    private Vector2 TargetVelocity()
    {
        Vector2 currentPos = agentPos.position;
        _targetVelocity = currentPos;
        Vector2 currentVelocity = (currentPos - _targetVelocity) / Time.deltaTime;
        return currentVelocity; 
    }
}