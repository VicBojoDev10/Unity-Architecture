using UnityEngine;
using System.Collections;
public class Wander : MonoBehaviorSteeringBehavior
{
    public float wanderRadius = 2f;
    public float wanderDistance = 4f;
    public float wanderJitter = 1f;
    public float updateInterval = 0.2f;

    private Vector2 wanderTarget;

    protected override void Awake()
    {
        base.Awake();
        wanderTarget = Random.insideUnitSphere * wanderRadius;
        wanderTarget.y = 0;
    }

    private void OnEnable()
    {
        StartCoroutine(JitterRoutine());
    }

    private IEnumerator JitterRoutine()
    {
        while (true)
        {
            wanderTarget += new Vector2(
                Random.Range(-1f,1f) * wanderJitter,
                Random.Range(-1f,1f) * wanderJitter
                );
            wanderTarget = wanderTarget.normalized * wanderRadius;
            yield return new WaitForSeconds(updateInterval);
        }
    }

    public override Vector2 GetSteering(SteeringContext ctx)
    {
        if (!enabled) return Vector2.zero;

        Vector2 circleCenter = ctx.velocity.normalized * wanderDistance;
        Vector2 targetWorld = ctx.position + circleCenter + wanderTarget; 
        
        Vector2 desired = (targetWorld - ctx.position).normalized * ctx.maxSpeed;
        return desired - ctx.velocity; 
    }
}
