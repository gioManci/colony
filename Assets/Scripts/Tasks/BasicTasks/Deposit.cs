using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Colony.Tasks.BasicTasks
{
    public class Deposit : Task
    {
        public Deposit(GameObject agent) : base(agent, TaskType.Deposit)
        {

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
