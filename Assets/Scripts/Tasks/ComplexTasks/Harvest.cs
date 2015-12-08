using UnityEngine;
using System.Collections;
using System;
using Colony.Tasks;
using Colony.Tasks.BasicTasks;
using Colony.Resources;

namespace Colony.Tasks.ComplexTasks
{

    public class Harvest : ComplexTask
    {
        private GameObject resource;
        private BeeLoad load;

        public Harvest(GameObject agent, GameObject resource) : base(agent, TaskType.Harvest)
        {
            this.resource = resource;
            load = agent.GetComponent<BeeLoad>();
        }

        public override void Activate()
        {
            status = Status.Active;
            RemoveAllSubtasks();

            GameObject closestHive = EntityManager.Instance.GetClosestHive(resource.transform.position);

            if (load.IsFull)
            {
                AddSubtask(new Deposit(agent, closestHive));
                AddSubtask(new Move(agent, closestHive.transform.position));
            }
            else
            {
                AddSubtask(new Deposit(agent, closestHive));
                AddSubtask(new Move(agent, closestHive.transform.position));
                AddSubtask(new Extract(agent, resource));
                AddSubtask(new Move(agent, resource.transform.position));
            }
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
                status = Status.Inactive;
            }

            if (subtasksStatus == Status.Failed)
            {
                status = Status.Completed;
                RemoveAllSubtasks();
                //TODO: Handle search for resources.
            }

            return status;
        }

        public override void Terminate()
        {
            
        }
    }

}