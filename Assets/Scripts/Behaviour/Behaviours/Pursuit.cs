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
            Vector2 toTarget = target.transform.position - agent.transform.position;
            float relativeHeading = Vector2.Dot(agent.transform.up, target.transform.up);

            //If the target is within 20 degrees, simply seek to the target's position.
            if (Vector2.Dot(toTarget, agent.transform.up) > 0
                && relativeHeading < seekLimit)
            {
                return Seek(target.transform.position);
            }

            //Else, predict target's position and seek there.
            float lookAheadTime = toTarget.magnitude / (stats.Speed + targetsRigidbody.velocity.magnitude);

            return Seek((Vector2)target.transform.position + targetsRigidbody.velocity * lookAheadTime);
        }

        private Vector2 Seek(Vector2 position)
        {
            Vector2 distanceVector = target.transform.position - agent.transform.position;
            Vector2 desiredVelocity = distanceVector.normalized * stats.Speed;
            return desiredVelocity - rigidbody.velocity;
        }
    }
}
