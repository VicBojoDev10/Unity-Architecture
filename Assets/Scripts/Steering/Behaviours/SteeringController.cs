using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class SteeringController : ScriptableObject
{
    [Header("Agent Config")]
    public float maxSpeed = 5f;
    public float maxForce = 3f;
    
    [Header("POCO Behaviours")]
    [SerializReference]
    public List <Steeringbehaviour> behaviors = new();

    private readonly List<MonoBehaviorSteeringBehavior> monoBehaviors = new();
    private Rigidbody2D rb;
}
