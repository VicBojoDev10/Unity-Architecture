using UnityEngine;
using System.Collections;
public class Wander : MonoBehaviorSteeringBehavior
{
    public float wanderRadius = 2f;
    public float wanderDistance = 4f;
    public float wanderJitter = 1f;
    public float updateInterval = 0.2f;

    private Vector3 wanderTarget;

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
            wanderTarget += new Vector3(
                Random.Range(-1f,1f) * wanderJitter, 0f,
                Random.Range(-1f,1f) * wanderJitter
                );
            wanderTarget = wanderTarget.normalized * wanderRadius;
            yield return new WaitForSeconds(updateInterval);
        }
    }

    public override Vector3 GetSteering(SteeringContext ctx)
    {
        if (!enabled) return Vector3.zero;

        Vector3 circleCenter = ctx.velocity.normalized * wanderDistance;
        Vector3 targetWorld = ctx.position + circleCenter + wanderTarget; 
        
        Vector3 desired = (targetWorld - ctx.position).normalized * ctx.maxSpeed;
        return desired - ctx.velocity;
    }
}
