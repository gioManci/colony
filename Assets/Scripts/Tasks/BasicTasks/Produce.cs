using Colony.Behaviour;
using UnityEngine;

namespace Colony.Tasks.BasicTasks
{
    public class Produce : Task
    {
        private Cell refiningCell;
        private SteeringBehaviour behaviour;
        private bool failedOnActivate = false;

        public Produce(GameObject agent, GameObject refiningCell) : base(agent, TaskType.Produce)
        {
            this.refiningCell = refiningCell.GetComponent<Cell>();
            behaviour = agent.GetComponent<SteeringBehaviour>();
        }

        public override void Activate()
        {
            status = Status.Active;
            if (refiningCell.Inkeeper != null)
            {
                failedOnActivate = true;
            }
            else
            {
                refiningCell.Inkeeper = agent;
                behaviour.StopFlocking();
            }
        }

        public override void OnMessage()
        {
            
        }

        public override Status Process()
        {
            ActivateIfInactive();
            if (failedOnActivate)
            {
                return Status.Failed;
            }
            return status;
        }

        public override void Terminate()
        {
            if (!failedOnActivate)
            {
                refiningCell.Inkeeper = null;
                behaviour.StartFlocking();
            }
        }
    }
}
