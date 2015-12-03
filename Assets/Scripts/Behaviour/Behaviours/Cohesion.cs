using UnityEngine;

namespace Colony.Behaviour.Behaviours
{
    public class Cohesion : Behaviour
    {
        private BehaviourSystem owner;

        public Cohesion(GameObject agent, BehaviourSystem behaviourSystem, float weight)
            : base(agent, BehaviourType.Cohesion, weight)
        {
            owner = behaviourSystem;
        }

        public override Vector2 Compute()
        {
            Vector2 centerOfMass = Vector2.zero;
            Vector2 steeringForce = Vector2.zero;
            int neighborCount = 0;

            foreach(GameObject neighbor in owner.Neighbors)
            {
                if (agent != neighbor)
                {
                    centerOfMass += (Vector2)neighbor.transform.position;
                    neighborCount++;
                }
            }

            if (neighborCount > 0)
            {
                centerOfMass /= neighborCount;
                steeringForce = new Seek(agent, centerOfMass, 1.0f).Compute();
            }

            return steeringForce;
        }
    }
}
