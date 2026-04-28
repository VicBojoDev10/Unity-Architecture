using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : SteeringBehaviour
{
    [Header("Posiciones")]
    private Transform agent;

    private List<Transform> neighbors = new List<Transform>();
    public override Vector2 GetSteering(SteeringContext ctx)
    {
        //forEveryNeighbor
        foreach(Transform neighbor in neighbors)
        {
            //relVel = agente.vel − vecino.vel
            Rigidbody2D neighborBehaviour = neighbor.GetComponent<Rigidbody2D>();
            Vector2 relVel = ctx.velocity - neighborBehaviour.velocity;
            Vector2 relPos = ctx.position - neighborBehaviour.position;
            float t_min = Vector2.Dot(-relPos, -relVel) / Vector2.Dot(relVel,relVel);
            
            //posA_fut = agente.pos + agente.vel * t_min
            Vector2 posA_fut = ctx.position + ctx.velocity * t_min;
            Vector2 posB_fut = neighborBehaviour.position + neighborBehaviour.velocity * t_min;
            Vector2 dist_fut = posA_fut - posB_fut;
            float threat = Mathf.Abs(dist_fut.magnitude);
            
            //mostThreatening = vecino con menor t_min entre amenazas
            if (threat < t_min)
            {
                Vector2 offset = posA_fut - posB_fut;
                Vector2 steering = offset.normalized * ctx.maxForce;
                return steering;
            }
        }
        return Vector2.zero;
    }
}
