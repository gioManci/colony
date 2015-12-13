using System;
using UnityEngine;

namespace Colony.Behaviour.Behaviours
{
    public class Arrive : Behaviour
    {
        private Vector2 target;
        private float deceleration;
        private Stats stats;

        public Arrive(GameObject agent, Vector2 target, float deceleration, float weight)
            : base(agent, BehaviourType.Arrive, weight)
        {
            this.target = target;
            this.deceleration = deceleration;
            stats = agent.GetComponent<Stats>();
        }

        public override Vector2 Compute()
        {
            Vector2 distanceVector = target - (Vector2)agent.transform.position;

            float distance = distanceVector.magnitude;

            if (distance > 0.5)
            {
                float speed = distance / deceleration;

                if (speed > stats.Speed)
                {
                    speed = stats.Speed;
                }

                Vector2 desiredVelocity = distanceVector * speed / distance;

                return desiredVelocity - agent.GetComponent<Rigidbody2D>().velocity;
            }

            return Vector2.zero;
        }
    }
}