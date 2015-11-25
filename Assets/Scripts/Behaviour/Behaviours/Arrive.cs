using System;
using UnityEngine;

namespace Colony.Behaviour.Behaviours
{
    public class Arrive : Behaviour
    {
        private Vector2 target;
        private float deceleration;

        public Arrive(GameObject agent, Vector2 target, float deceleration, float weight)
            : base(agent, BehaviourType.Arrive, weight)
        {
            this.target = target;
            this.deceleration = deceleration;
        }

        public override Vector2 Compute()
        {
            Vector2 distanceVector = target - (Vector2)agent.transform.position;

            float distance = distanceVector.magnitude;

            if (distance > 0)
            {
                float speed = distance / deceleration;

                //TODO: Substitute 3.0f with the max speed constant
                if (speed > 3.0f)
                {
                    speed = 3.0f;
                }

                Vector2 desiredVelocity = distanceVector * speed / distance;

                return desiredVelocity - agent.GetComponent<Rigidbody2D>().velocity;
            }

            return Vector2.zero;
        }
    }
}
