using UnityEngine;
[System.Serializable]
public class Pursuit : SteeringBehaviour
{
    [Header ("Valores")]
    public float t;
    public Transform pursuitTarget;
    public Rigidbody2D rb;
    public Vector2 velocity => rb.linearVelocity;
    //private Vector3 

    public void Awake() => rb.GetComponent<Rigidbody2D>();
    public override Vector2 GetSteering(SteeringContext ctx)
    {
        if(pursuitTarget == null || !enabled) return Vector2.zero;

        
        float distance = Vector2.Distance(ctx.position, pursuitTarget.position);
        t = distance / ctx.maxSpeed;
        
        Vector2 target= pursuitTarget.position;
        Vector2 futurePos = ctx.position + velocity * t;
        Vector2 desired = (futurePos - target).normalized * ctx.maxSpeed;
        Vector2 steering = desired - velocity;
        return steering;
    }

    private Vector2 PlayerVelocity()
    {
       return Vector2.zero; 
    }
    
}
