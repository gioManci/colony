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

            foreach (GameObject neighbor in owner.Neighbors)
            {
                averageHeading += (Vector2)neighbor.transform.up;
            }

            if (owner.Neighbors.Count > 0)
            {
                averageHeading /= owner.Neighbors.Count;
                averageHeading -= (Vector2)agent.transform.up;
            }

            return averageHeading;
        }
    }
}
