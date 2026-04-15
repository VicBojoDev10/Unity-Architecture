using UnityEngine;

//[RequireComponent(typeof(SteeringController))]
public abstract class MonoBehaviorSteeringBehavior : MonoBehaviour
{
    [Range(0f, 1f)]
    public float weight = 1f;
    public new bool enabled = true;

   // protected SteeringController controller;

    protected virtual void Awake()
    {
        //controller = GetComponent<SteeringController>();
        //controller.RegisterMonoBehaviour(this);
    }

    protected virtual void OnDestroy()
    {
        //controller?.UnregisterMonoBehaviour(this);
    }

    public abstract Vector3 GetSteering(SteeringContext ctx);
}
