using Colony.Tasks.BasicTasks;
using System;
using UnityEngine;

namespace Colony.Tasks.ComplexTasks
{
    public class Refine : ComplexTask
    {
        private Cell refiningCell;

        public Refine(GameObject agent, GameObject refiningCell) : base(agent, TaskType.Inkeep)
        {
            this.refiningCell = refiningCell.GetComponent<Cell>();
        }

        public override void Activate()
        {
            status = Status.Active;
            RemoveAllSubtasks();

            //Do something only if the cell is empty
            if (refiningCell.Inkeeper == null)
            {
                AddSubtask(new Produce(agent, refiningCell.gameObject));
                AddSubtask(new Move(agent, refiningCell.transform.position, 0.5f));
            }
        }

        public override void OnMessage()
        {
            
        }

        public override Status Process()
        {
            ActivateIfInactive();

            Status subtasksStatus = ProcessSubtasks();

            if (subtasksStatus == Status.Completed)
            {
                status = Status.Completed;
            }

            if (subtasksStatus == Status.Failed)
            {
                status = Status.Failed;
            }

            return status;
        }
    }
}
