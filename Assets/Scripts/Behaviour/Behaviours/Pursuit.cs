using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Colony.Behaviour.Behaviours
{
    public class Pursuit : Behaviour
    {
        private GameObject target;
        private Rigidbody2D rigidbody, targetsRigidbody;
        private Stats stats;

        /// <summary>
        /// Approximately equal to -Cos(20°)
        /// </summary>
        private const float seekLimit = -0.94f;

        public Pursuit(GameObject agent, GameObject target, float weight) : base(agent, BehaviourType.Pursuit, weight)
        {
            this.target = target;
            rigidbody = agent.GetComponent<Rigidbody2D>();
            targetsRigidbody = target.GetComponent<Rigidbody2D>();
            stats = agent.GetComponent<Stats>();
        }

        public override Vector2 Compute()
        {
            if (target != null)
            {
                Vector2 toTarget = target.transform.position - agent.transform.position;

                if (toTarget.sqrMagnitude > 0.5f * 0.5f)
                {
                    float relativeHeading = Vector2.Dot(agent.transform.up, target.transform.up);

                    //If the target is within 20 degrees, simply seek to the target's position.
                    if (Vector2.Dot(toTarget, agent.transform.up) > 0
                        && relativeHeading < seekLimit)
                    {
                        return Arrive(target.transform.position);
                    }

                    //Else, predict target's position and seek there.
                    float lookAheadTime = toTarget.magnitude / (stats.Speed + targetsRigidbody.velocity.magnitude);

                    return Arrive((Vector2)target.transform.position + targetsRigidbody.velocity * lookAheadTime);
                }
            }

            return Vector2.zero;
        }

        private Vector2 Arrive(Vector2 position)
        {
            Vector2 distanceVector = target.transform.position - agent.transform.position;

            float distance = distanceVector.magnitude;

            if (distance > 0.5)
            {
                float speed = distance / 0.9f;

                if (speed > stats.Speed)
                {
                    speed = stats.Speed;
                }

                Vector2 desiredVelocity = distanceVector * speed / distance;

                return desiredVelocity - rigidbody.velocity;
            }

            return Vector2.zero;
        }
    }
}
