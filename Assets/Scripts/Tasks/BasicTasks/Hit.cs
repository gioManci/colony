using System;
using UnityEngine;

namespace Colony.Tasks.BasicTasks
{
    public class Hit : Task
    {
        private GameObject target;
        private Stats stats;

        public Hit(GameObject agent, GameObject target) : base(agent, TaskType.Hit)
        {
            this.target = target;
            stats = agent.GetComponent<Stats>();
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

            //TODO: Substitute constant
            if (target == null || Vector2.Distance(agent.transform.position, target.transform.position) > 1.0f)
            {
                status = Status.Failed;
            }
            else
            {
                //TODO: Get the enemy's battling system component and notify it of the hit.
                Life life = target.GetComponent<Life>();
                life.Decrease(stats.Damage);
                status = Status.Completed;
            }

            return status;
        }

        public override void Terminate()
        {
            
        }
    }
}
