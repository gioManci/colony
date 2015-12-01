using UnityEngine;
using System.Collections;
using System;
using Colony.Tasks;
using Colony.Tasks.BasicTasks;

namespace Colony.Tasks.ComplexTasks
{

    public class Harvest : ComplexTask
    {
        private GameObject resource;

        public Harvest(GameObject agent, GameObject resource) : base(agent, TaskType.Harvest)
        {
            this.resource = resource;
        }

        public override void Activate()
        {
            status = Status.Active;
            RemoveAllSubtasks();
            //TODO: Check if bag is full
            if (true)
            {
                AddSubtask(new Deposit(agent));
                AddSubtask(new Move(agent, new Vector2(0, 0)));
            }
            else
            {
                AddSubtask(new Deposit(agent));
                AddSubtask(new Move(agent, new Vector2(0, 0)));
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

            return status;
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }
    }

}