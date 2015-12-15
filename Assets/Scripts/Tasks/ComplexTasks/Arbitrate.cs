using Colony.Resources;
using Colony.Tasks.BasicTasks;
using System;
using UnityEngine;

namespace Colony.Tasks.ComplexTasks
{
    using Random = UnityEngine.Random;

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
            RemoveAllSubtasks();

            targetSystem.Update();

            if (targetSystem.HasTarget)
            {
                if (targetSystem.CurrentTarget.tag == "Cell")
                {
                    AddSubtask(new Loot(agent, targetSystem.CurrentTarget));
                }
                else
                {
                    AddSubtask(new Attack(agent, targetSystem.CurrentTarget));
                }
            }
            else
            {
                AddSubtask(new Explore(agent, Random.Range(4.0f, 8.0f), Random.Range(3.0f, 6.0f)));
            }
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            if (Time.time - targetSystem.LastUpdate >= stats.ReactionTime)
            {
                targetSystem.Update();

                // if it finds a target and is exploring, attack!
                if (targetSystem.HasTarget && (IsCurrentSubtask(TaskType.Explore) || targetSystem.HasChangedTarget))
                {
                    RemoveAllSubtasks();

                    if (targetSystem.CurrentTarget.tag == "Cell")
                    {
                        AddSubtask(new Loot(agent, targetSystem.CurrentTarget));
                    }
                    else
                    {
                        AddSubtask(new Attack(agent, targetSystem.CurrentTarget));
                    }
                }
            }

            Status subtasksStatus = ProcessSubtasks();
            if (subtasksStatus == Status.Completed)
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
