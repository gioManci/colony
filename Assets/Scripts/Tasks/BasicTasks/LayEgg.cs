using System;
using UnityEngine;

namespace Colony.Tasks.BasicTasks
{
    public class LayEgg : Task
    {
        private GameObject breedingCell;

        public LayEgg(GameObject agent, GameObject breedingCell) : base(agent, TaskType.LayEgg)
        {
            this.breedingCell = breedingCell;
        }

        public override void Activate()
        {
            status = Status.Active;
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            EntityManager.Instance.CreateLarva(breedingCell.transform.position);
            status = Status.Completed;

            return status;
        }

        public override void Terminate()
        {

        }
    }
}