using UnityEngine;

[System.Serializable]
public class Pursuit : SteeringBehaviour
{
    [Header ("Valores")]
    public float t;
    public Transform pursuitTarget;

    [Header ("Velocidad")]
    private Vector2 _currentVelocity;

    private Vector2 _targetVelocity;
    public Transform agentPos;
    
    public override Vector2 GetSteering(SteeringContext ctx)
    {
        if(pursuitTarget == null || !enabled) return Vector2.zero;

        TargetVelocity();
        Vector2 target= pursuitTarget.position;
        Vector2 agent= agentPos.position;
        DrawVectors();
        
        float distance = Vector2.Distance(agent, target);
        t = distance / ctx.maxSpeed;

        Vector2 futurePos = target + _targetVelocity * t;
        Vector2 desired = (futurePos - agent).normalized * ctx.maxSpeed;
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

    public void DrawVectors()
    {
        Vector2 target= pursuitTarget.position;
        Vector2 agent= agentPos.position;
        Debug.DrawLine(agent, target, Color.red);
    }
    
}
