using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Colony.Tasks.BasicTasks
{
    public class Extract : Task
    {
        private GameObject resource;

        public Extract(GameObject agent, GameObject resource) : base(agent, TaskType.Extract)
        {
            this.resource = resource;
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
            //TODO: Check if the bag is full
            if (false)
            {
                return Status.Completed;
            }
            //TODO: Check if resource is depleted
            /*if (false)
            {

            }
            if ()*/
            return status;
        }

        public override void Terminate()
        {
            
        }
    }
}
