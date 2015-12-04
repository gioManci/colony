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
        private float timeFromLastExtraction;

        public Extract(GameObject agent, GameObject resource) : base(agent, TaskType.Extract)
        {
            this.resource = resource;
            timeFromLastExtraction = 0.0f;
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
            timeFromLastExtraction += Time.deltaTime;

            //TODO: Check if the bag is full
            if (false)
            {
                return Status.Completed;
            }
            //TODO: Check if resource is depleted
            if (false)
            {
                return Status.Failed;
            }
            //TODO: Substitute 1.0f with constant
            if (timeFromLastExtraction > 1.0f)
            {

            }
            return status;
        }

        public override void Terminate()
        {
            
        }
    }
}
