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
            throw new NotImplementedException();
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
