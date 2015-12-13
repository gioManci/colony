using Colony.Behaviour;
using System;
using UnityEngine;

namespace Colony.Tasks.BasicTasks
{
    public class Explore : Task
    {
        private SteeringBehaviour steeringBehaviour;
        private float wanderTime = 0.0f;
        private float idleTime = 0.0f;
        private float maxWanderTime;
        private float maxIdleTime;

        public Explore(GameObject agent, float maxWanderTime, float maxIdleTime) : base(agent, TaskType.Explore)
        {
            this.maxWanderTime = maxWanderTime;
            this.maxIdleTime = maxIdleTime;
            steeringBehaviour = agent.GetComponent<SteeringBehaviour>();
        }
        public override void Activate()
        {
            status = Status.Active;
            steeringBehaviour.StartWander();
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            if (steeringBehaviour.IsWandering)
            {
                wanderTime += Time.deltaTime;

                if (wanderTime >= maxWanderTime)
                {
                    steeringBehaviour.StopWander();
                    wanderTime = 0.0f;
                }
            }
            else
            {
                idleTime += Time.deltaTime;

                if (idleTime >= maxIdleTime)
                {
                    status = Status.Inactive;
                    idleTime = 0.0f;
                }
            }

            return status;
        }

        public override void Terminate()
        {
            steeringBehaviour.StopWander();
        }
    }
}
