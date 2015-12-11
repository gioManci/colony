using Colony.Tasks.BasicTasks;
using System;
using UnityEngine;
namespace Colony.Tasks.ComplexTasks
{
    public class Attack : ComplexTask
    {
        private GameObject enemy;

        public Attack(GameObject agent, GameObject enemy) : base(agent, TaskType.Attack)
        {
            this.enemy = enemy;
        }

        public override void Activate()
        {
            status = Status.Active;
            RemoveAllSubtasks();

            AddSubtask(new Hit(agent, enemy));
            AddSubtask(new Move(agent, enemy.transform.position, 1.0f));
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        public override Status Process()
        {
            if (enemy == null)
            {
                status = Status.Completed;
            }
            else
            {
                ActivateIfInactive();
                Status subtasksStatus = ProcessSubtasks();
                if (subtasksStatus == Status.Completed)
                {
                    status = Status.Inactive;
                }
            }

            return status;
        }

        public override void Terminate()
        {
            
        }

        private void HandleFailure()
        {
            if (subtasks.Peek().Type == TaskType.Hit)
            {
                if (enemy == null)
                {
                    status = Status.Completed;
                }
                else if (Vector2.Distance(agent.transform.position, enemy.transform.position) > 1.0f)
                {

                }
            }
        }
    }
}
