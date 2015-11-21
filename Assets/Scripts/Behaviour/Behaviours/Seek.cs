using UnityEngine;
using System.Collections;
using System;

public class Seek : Behaviour
{
    private Vector2 target;

    public Seek(GameObject actor, Vector2 target) : base(actor, BehaviourType.Seek)
    {
        this.target = target;
    }

    public override Vector2 Compute()
    {
        Rigidbody2D rigidbody = actor.GetComponent<Rigidbody2D>();
        Vector2 position = actor.transform.position;
        Vector2 distance = target - position;
        Vector2 normalized = distance.normalized;
        //TODO: 1.0f is the max speed. It must be substituted with a constant.
        Vector2 desiredVelocity = normalized * 1.0f;
        return desiredVelocity - rigidbody.velocity;
    }
}
