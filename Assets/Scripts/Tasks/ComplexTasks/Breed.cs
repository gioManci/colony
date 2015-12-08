using Colony.Tasks.BasicTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Colony.Tasks.ComplexTasks
{
    public class Breed : ComplexTask
    {
        private GameObject breedingCell;

        public Breed(GameObject agent, GameObject breedingCell) : base(agent, TaskType.Breed)
        {
            this.breedingCell = breedingCell;
        }

        public override void Activate()
        {
            status = Status.Active;
            RemoveAllSubtasks();

            AddSubtask(new LayEgg(agent));
            AddSubtask(new Move(agent, breedingCell.transform.position));
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            Status subtasksStatus = ProcessSubtasks();

            if (subtasksStatus == Status.Completed)
            {
                status = Status.Completed;
            }

            if (status == Status.Failed)
            {
                //TODO: Handle failure
                status = Status.Completed;
            }

            return status;
        }

        public override void Terminate()
        {
            
        }
    }
}
