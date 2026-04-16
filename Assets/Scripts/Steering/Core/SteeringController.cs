using UnityEngine;
using System.Collections.Generic;
public class SteeringController : MonoBehaviour
{
    [Header("Agent Config")]
    public float maxSpeed = 5f;
    public float maxForce = 3f;
	public float mass = 1f;
    
    [Header("POCO Behaviours")]
	[SerializeReference]
    public List <SteeringBehaviour> behaviors = new();

    private readonly List<MonoBehaviorSteeringBehavior> monoBehaviors = new();
    private Vector3 velocity;

	public void RegisterMonoBehavior(MonoBehaviorSteeringBehavior b)
		=> monoBehaviors.Add(b);
	
	public void UnregisterMonoBehavior(MonoBehaviorSteeringBehavior b)
		=> monoBehaviors.Remove(b);
	private void Update()
	{
		var ctx = new SteeringContext(transform.position, velocity, maxSpeed, maxForce);	
		Vector3 force = ComputeForce(ctx);
		force = Vector3.ClampMagnitude(force , maxForce);

		velocity += (force / mass) * Time.deltaTime;
		velocity = Vector3.ClampMagnitude(velocity,maxSpeed);
		transform.position += velocity * Time.deltaTime;

		if(velocity.sqrMagnitude > 0.01f)
			transform.forward = Vector3.Lerp(transform.forward, velocity.normalized, Time.deltaTime * 10f);
	}

	private Vector3 ComputeForce(SteeringContext ctx)
	{
		Vector3 total = Vector3.zero;

		foreach (var b in behaviors)
			if (b != null && b.enabled)
				total += b.GetSteering(ctx) * b.weight;

		foreach (var b in monoBehaviors)
			if (b != null && b.enabled)
				total += b.GetSteering(ctx) * b.weight;

		return total;
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
		Gizmos.DrawRay(transform.position, velocity);
    }
}
