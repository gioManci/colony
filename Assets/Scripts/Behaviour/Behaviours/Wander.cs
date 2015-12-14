using UnityEngine;

namespace Colony.Behaviour.Behaviours
{
    public class Wander : Behaviour
    {
        private float wanderRadius;
        private float wanderDistance;
        private float wanderJitter;
        private Vector2 wanderTarget;

        //This must be removed after the prototype
        private GameObject trick;

        public Wander(GameObject agent, float wanderRadius, float wanderDistance, float wanderJitter, float weight)
            : base(agent, BehaviourType.Wander, weight)
        {
            this.wanderDistance = wanderDistance;
            this.wanderJitter = wanderJitter;
            this.wanderRadius = wanderRadius;

            wanderTarget = Random.insideUnitCircle * wanderRadius;

            Transform trickTransform = agent.transform.Find("TransformTrick");
            if (trickTransform == null)
            {
                trick = new GameObject("TransformTrick");
                trick.transform.SetParent(agent.transform);
            }
            else
            {
                trick = trickTransform.gameObject;
            }
        }

        public override Vector2 Compute()
        {
            //The jitter is time dependent
            float jitter = wanderJitter * Time.deltaTime;
            wanderTarget += Random.insideUnitCircle * jitter;
            wanderTarget.Normalize();
            wanderTarget *= wanderRadius;
            Vector3 localTarget = wanderTarget + new Vector2(0.0f, wanderDistance);

            //Gets the world position of the target
            trick.transform.localPosition = localTarget;
            Vector3 worldTarget = trick.transform.position;

            //Trying to build a transformation matrix from local to world
            //Matrix4x4 transform = Matrix4x4.TRS(
            //    agent.transform.position,
            //    Quaternion.LookRotation(agent.transform.up, agent.transform.right),
            //    Vector3.one);
            //target = transform.MultiplyVector(target);

            Debug.DrawLine(Vector2.zero, worldTarget);

            return worldTarget - agent.transform.position;
        }
    }
}
