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
        private float errorMargin;

        /// <summary>
        /// Creates a new Move task specifying the agent that will perform this task, the target position and
        /// the admissible error.
        /// </summary>
        /// <param name="agent">The agent that will perform this task.</param>
        /// <param name="position">The target position.</param>
        /// <param name="errorMargin">The maximum admitted error.</param>
        public Move(GameObject agent, Vector2 position, float errorMargin) : base(agent, TaskType.Move)
        {
            this.errorMargin = errorMargin;
            target = position;
            steering = agent.GetComponent<SteeringBehaviour>();
        }

        public override void Activate()
        {
            status = Status.Active;
            steering.StopFlocking();
            steering.StartArrive(target);
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            if (Vector2.Distance(target, agent.transform.position) < errorMargin)
            {
                status = Status.Completed;
            }

            return status;
        }

        public override void Terminate()
        {
            steering.StopArrive();
            steering.StartFlocking();
        }
    }

}
