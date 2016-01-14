using System;
using UnityEngine;

namespace Colony.Tasks.BasicTasks
{
    class Colonize : Task
    {
        public Colonize(GameObject agent) : base(agent, TaskType.Colonize)
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
            ActivateIfInactive();

            //TODO: Check if it's possible to build a hive
            EntityManager.Instance.CreateBeehive(agent.transform.position);

            return status = Status.Completed;
        }

        public override void Terminate()
        {
            
        }
    }
}
