using System;
using UnityEngine;

namespace Colony.Tasks.BasicTasks
{
    public class LayEgg : Task
    {
        public LayEgg(GameObject agent) : base(agent, TaskType.LayEgg)
        {

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
            throw new NotImplementedException();
        }

        public override void Terminate()
        {

        }
    }
}