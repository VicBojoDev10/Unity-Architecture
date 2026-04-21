using UnityEngine;

public abstract class Pursuit : SteeringBehaviour
{
    [Header ("Valores")]
    public float t;
    public Transform pursuitTarget;
    public Rigidbody2D rb;
    public Vector2 velocity => rb.linearVelocity;

    public void Awake() => rb.GetComponent<Rigidbody2D>();
    public override Vector2 GetSteering(SteeringContext ctx)
    {
        if(pursuitTarget == null || !enabled) return Vector2.zero;

        float distance = Vector2.Distance(ctx.position, pursuitTarget.position);
        t = distance / ctx.maxSpeed;

       // Vector2 futurePos = pursuitTarget.position + velocity * t;
       // Vector2 desired = (futurePos - pursuitTarget.position).normalized * ctx.maxSpeed;
       // Vector2 steering = desired - velocity;
        return Vector2.zero; //steering
    }
}
