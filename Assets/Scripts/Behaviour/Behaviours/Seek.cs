using UnityEngine;
using System.Collections;
using System;

public class Seek : Behaviour
{
    private Vector2 target;
    private Rigidbody2D rigidbody;

    public Seek(GameObject actor, Vector2 target) : base(actor, BehaviourType.Seek)
    {
        this.target = target;
        rigidbody = actor.GetComponent<Rigidbody2D>();
    }

    public override Vector2 Compute()
    {
        Vector2 position = actor.transform.position;
        Vector2 distanceVector = target - position;
        //TODO: 1.0f is the max speed. It must be substituted with a constant.
        Vector2 desiredVelocity = distanceVector.normalized * 2.0f;
        return desiredVelocity - rigidbody.velocity;
    }
}
