using Colony.Resources;
using Colony.Tasks.BasicTasks;
using System;
using UnityEngine;

namespace Colony.Tasks.ComplexTasks
{
    public class Loot : ComplexTask
    {
        private GameObject cell;
        private HiveWarehouse hive;

        public Loot(GameObject agent, GameObject cell) : base(agent, TaskType.Loot)
        {
            this.cell = cell;
            hive = cell.GetComponentInParent<HiveWarehouse>();
        }

        public override void Activate()
        {
            status = Status.Active;
            RemoveAllSubtasks();

            AddSubtask(new StealResources(agent, hive));
            AddSubtask(new Move(agent, cell.transform.position, 0.5f));
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            if (!IsCurrentSubtask(TaskType.Move))
            {
                Vector2 toTarget = cell.transform.position - agent.transform.position;
                if (toTarget.sqrMagnitude >= 0.5f * 0.5f)
                {
                    AddSubtask(new Move(agent, cell.transform.position, 0.5f));
                }
            }

            Status subtasksStatus = ProcessSubtasks();
            if (subtasksStatus == Status.Completed)
            {
                status = Status.Completed;
            }

            return status;
        }
    }
}
