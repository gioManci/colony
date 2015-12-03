using UnityEngine;

namespace Colony.Behaviour.Behaviours
{
    public class Alignment : Behaviour
    {
        private BehaviourSystem owner;

        public Alignment(GameObject agent, BehaviourSystem behaviourSystem, float weight)
            : base(agent, BehaviourType.Alignment, weight)
        {
            owner = behaviourSystem;
        }

        public override Vector2 Compute()
        {
            Vector2 averageHeading = Vector2.zero;
            int neighborCount = 0;

            foreach (GameObject neighbor in owner.Neighbors)
            {
                if (agent != neighbor)
                {
                    averageHeading += (Vector2)neighbor.transform.up;
                    neighborCount++;
                }
            }

            if (neighborCount > 0)
            {
                averageHeading /= neighborCount;
                averageHeading -= (Vector2)agent.transform.up;
            }

            return averageHeading;
        }
    }
}
