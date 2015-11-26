using UnityEngine;
using System.Collections;
using System;
using Colony.Tasks;
using Colony.Behaviour;

namespace Colony.Tasks.BasicTasks
{
    /// <summary>
    /// Represents the goal of moving to a given position.
    /// </summary>
    public class Move : Task
    {
        private Vector2 target;
        private SteeringBehaviour steering;

        /// <summary>
        /// Creates a new Move task specifying the agent that will perform this task and the target position.
        /// </summary>
        /// <param name="agent">The agent that will perform this task.</param>
        /// <param name="position">The target position.</param>
        public Move(GameObject agent, Vector2 position) : base(agent, TaskType.Move)
        {
            target = position;
            steering = agent.GetComponent<SteeringBehaviour>();
        }

        public override void Activate()
        {
            status = Status.Active;
            steering.StartArrive(target);
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            if (Vector2.Distance(target, (Vector2)agent.transform.position) < 0.1)
            {
                status = Status.Completed;
            }

            return status;
        }

        public override void Terminate()
        {
            steering.StopArrive();
        }
    }

}
