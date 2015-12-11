using System;
using UnityEngine;

namespace Colony.Tasks.ComplexTasks
{
    public class Arbitrate : ComplexTask
    {
        private Stats stats;
        private TargetSystem targetSystem;

        public Arbitrate(GameObject agent) : base(agent, TaskType.Arbitrate)
        {
            stats = agent.GetComponent<Stats>();
            targetSystem = new TargetSystem(agent);
        }

        public override void Activate()
        {
            status = Status.Active;

            // if no targets are nearby, wander
            if (!targetSystem.HasTarget)
            {
                //AddSubtask(new Explore(agent, /*max wander time*/5.0f));
            }
            else
            {

            }
            
            // if wandering for too long, stop
            // if an enemy is found, hunt and attack
            // if a beehive is found, loot it
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            Status subtasksStatus = ProcessSubtasks();
            if (subtasksStatus == Status.Completed || subtasksStatus == Status.Failed)
            {
                status = Status.Inactive;
            }

            return status;
        }

        public override void Terminate()
        {
            
        }
    }
}
