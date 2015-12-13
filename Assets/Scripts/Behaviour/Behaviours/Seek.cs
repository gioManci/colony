using UnityEngine;

namespace Colony.Behaviour.Behaviours
{
    public class Seek : Behaviour
    {
        private Vector2 target;
        private Rigidbody2D rigidbody;
        private Stats stats;

        public Seek(GameObject agent, Vector2 target, float weight)
                : base(agent, BehaviourType.Seek, weight)
        {
            this.target = target;
            rigidbody = agent.GetComponent<Rigidbody2D>();
            stats = agent.GetComponent<Stats>();
        }

        public override Vector2 Compute()
        {
            Vector2 distanceVector = target - (Vector2)agent.transform.position;
            Vector2 desiredVelocity = distanceVector.normalized * stats.Speed;
            return desiredVelocity - rigidbody.velocity;
        }
    }
}
