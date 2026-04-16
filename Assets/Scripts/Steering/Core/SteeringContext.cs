using UnityEngine;

public class SteeringContext
{
    public Vector3 position;
    public Vector3 velocity;
    public float maxSpeed;
    public float maxForce;

    public SteeringContext(Vector3 pos, Vector3 vel, float spd, float frc)
    {
        position = pos; 
        velocity = vel;
        maxSpeed = spd;
        maxForce = frc;
    }
}
