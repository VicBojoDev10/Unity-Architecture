using UnityEngine;

public interface ISteeringBehavior 
{
    public abstract class SteeringBehaviour : ISteeringBehavior {}
    public abstract class MonoBehaviorSteeringBehavior : MonoBehaviour, ISteeringBehavior {}
}
