using Colony.Tasks.BasicTasks;
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
            RemoveAllSubtasks();

            //Default behaviour: explore.
            AddSubtask(new Explore(agent, 5.0f, 5.0f));

            //targetSystem.Update();

            //if (targetSystem.HasTarget)
            //{
            //    if (targetSystem.CurrentTarget.tag == "Cell")
            //    {
            //        //Loot hive
            //    }
            //    else
            //    {
            //        AddSubtask(new Attack(agent, targetSystem.CurrentTarget));
            //    }
            //}
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            //if (targetSystem.LastUpdate - Time.time >= stats.ReactionTime)
            //{
            //    targetSystem.Update();
            //}

            //// if it finds a target and is exploring, attack!
            //if (targetSystem.HasTarget && IsCurrentSubtask(TaskType.Explore))
            //{
            //    if (targetSystem.CurrentTarget.tag == "Cell")
            //    {
            //        //Loot hive
            //    }
            //    else
            //    {
            //        AddSubtask(new Attack(agent, targetSystem.CurrentTarget));
            //    }
            //}

            //if (!targetSystem.HasTarget && IsCurrentSubtask(TaskType.Attack))
            //{
            //    subtasks.Pop();
            //}

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
