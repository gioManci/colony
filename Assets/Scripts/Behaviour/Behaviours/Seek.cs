using UnityEngine;
using System.Collections;
using System;

namespace Colony.Behaviour.Behaviours {

public class Seek : Behaviour
{
    private Vector2 target;
    private Rigidbody2D rigidbody;

    public Seek(GameObject actor, Vector2 target, float weight)
            : base(actor, BehaviourType.Seek, weight)
    {
        this.target = target;
        rigidbody = actor.GetComponent<Rigidbody2D>();
    }

    public override Vector2 Compute()
    {
        Vector2 distanceVector = target - (Vector2)agent.transform.position;
        //TODO: 3.0f is the max speed. It must be substituted with a constant.
        Vector2 desiredVelocity = distanceVector.normalized * 3.0f;
        return desiredVelocity - rigidbody.velocity;
    }
}

}
