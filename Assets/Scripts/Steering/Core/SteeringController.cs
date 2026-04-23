using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class SteeringController : MonoBehaviour
{
    [Header("Agent Config")]
    public float maxSpeed = 5f;
    public float maxForce = 3f;
    
    [Header("POCO Behaviours")]
	[SerializeReference]
    public List <SteeringBehaviour> behaviors = new();

    private readonly List<MonoBehaviorSteeringBehavior> monoBehaviors = new();
	private Rigidbody2D rb = new Rigidbody2D();
    private Vector2 velocity = new Vector2();
    
    private SteeringContext ctx = new SteeringContext();

    public void RegisterMonoBehavior(MonoBehaviorSteeringBehavior b)
		=> monoBehaviors.Add(b);
	
	public void UnregisterMonoBehavior(MonoBehaviorSteeringBehavior b)
		=> monoBehaviors.Remove(b);
    private void Awake() => rb = GetComponent<Rigidbody2D>();

    public void Start()
    {
	    velocity = rb.linearVelocity;
    }

    private void FixedUpdate()
	{
		Vector2 pos2d = transform.position;
		var ctx = new SteeringContext(pos2d, rb.linearVelocity, maxSpeed, maxForce);
		Vector2 force = ComputeForce(ctx);
		force = Vector2.ClampMagnitude(force, maxForce);
		if (force.sqrMagnitude < 0.0001f && rb.linearVelocity.sqrMagnitude < 0.0001f)
			return;
		rb.AddForce(force);
		rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);

		if(rb.linearVelocity.sqrMagnitude > 0.01f)
		{
			float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x)
										* Mathf.Rad2Deg - 90f;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}

	private Vector2 ComputeForce(SteeringContext ctx)
	{
		Vector2 total = Vector2.zero;

		foreach (var b in behaviors)
			if (b != null && b.enabled)
				total += b.GetSteering(ctx) * b.weight;

		foreach (var b in monoBehaviors)
			if (b != null && b.enabled)
				total += b.GetSteering(ctx) * b.weight;

		return total;
	}
}
