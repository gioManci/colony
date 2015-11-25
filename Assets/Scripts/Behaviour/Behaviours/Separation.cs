using UnityEngine;

namespace Colony.Behaviour.Behaviours
{
    public class Separation : Behaviour
    {
        private BehaviourSystem owner;

        public Separation(GameObject agent, BehaviourSystem behaviourSystem, float weight)
            : base(agent, BehaviourType.Separation, weight)
        {
            owner = behaviourSystem;
        }

        public override Vector2 Compute()
        {
            Vector2 steeringForce = Vector2.zero;

            foreach(GameObject neighbor in owner.Neighbors)
            {
                if (agent != neighbor)
                {
                    Vector2 toAgent = agent.transform.position - neighbor.transform.position;
                    steeringForce += toAgent.normalized / toAgent.magnitude;
                }
            }

            return steeringForce;
        }
    }
}
