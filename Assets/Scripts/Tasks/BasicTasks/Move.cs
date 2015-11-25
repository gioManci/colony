using UnityEngine;
using System.Collections;
using System;
using Colony.Tasks;
using Colony.Behaviour;

namespace Colony.Tasks.BasicTasks
{

    public class Move : Task
    {
        private Vector2 target;
        private SteeringBehaviour steering;

        public Move(GameObject agent, Vector2 position) : base(agent, TaskType.Move)
        {
            target = position;
            steering = agent.GetComponent<SteeringBehaviour>();
        }

        public override void Activate()
        {
            status = Status.Active;
            steering.StartArrive(target);
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            ActivateIfInactive();

            if (Vector2.Distance(target, (Vector2)agent.transform.position) < 0.1)
            {
                status = Status.Completed;
            }

            return status;
        }

        public override void Terminate()
        {
            steering.StopArrive();
        }
    }

}
