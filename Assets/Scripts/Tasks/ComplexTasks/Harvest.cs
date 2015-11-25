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
            if (true)
            {

            }
            if (true)
            {
                AddSubtask(new Move(agent, new Vector2(0, 0)));
            }
            if (true)
            {
                AddSubtask(new Extract(agent, resource));
            }
            if (agent.transform.position != resource.transform.position)
            {
                AddSubtask(new Move(agent, resource.transform.position));
            }
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            throw new NotImplementedException();
        }

        public override void Terminate()
        {
            throw new NotImplementedException();
        }
    }

}