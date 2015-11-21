using UnityEngine;
using System;
using System.Collections.Generic;

public class SteeringBehaviour : MonoBehaviour
{
    private BehaviourSystem behaviourSystem;
    private Rigidbody2D rigidbody2d;

    void Start()
    {
        behaviourSystem = new BehaviourSystem();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 forceToApply = behaviourSystem.CalculateResultingForce();

        if (forceToApply.sqrMagnitude < 0.001)
        {
            rigidbody2d.velocity *= 0.5f;
        }

        Vector2 acceleration = forceToApply / rigidbody2d.mass;

        rigidbody2d.velocity += acceleration * Time.deltaTime;

        if (rigidbody2d.velocity.magnitude > 1.0f)
        {
            rigidbody2d.velocity = rigidbody2d.velocity.normalized * 1.0f;
        }

        rigidbody2d.position += rigidbody2d.velocity * Time.deltaTime;

        /*if (rigidbody.velocity.sqrMagnitude < 0.0001)
        {
            transform.up.no
        }*/
    }

    public void StartSeek(Vector2 target)
    {
        behaviourSystem.AddBehaviour(new Seek(gameObject, target));
    }

    public void StopSeek()
    {
        behaviourSystem.RemoveBehaviour(BehaviourType.Seek);
    }
}
