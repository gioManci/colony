using UnityEngine;
using System.Collections;
using Colony.Tasks.BasicTasks;
using Colony.Tasks.ComplexTasks;

namespace Colony.Tasks
{
    public class WorkerBeeBrain : MonoBehaviour
    {
        private ComplexTask brain;

        void Start()
        {
            brain = new Think(gameObject);
        }

        void Update()
        {
            if (brain != null)
            {
                brain.Process();
            }
        }

        public void DoMove(Vector2 position)
        {
            brain.RemoveAllSubtasks();
            brain.AddSubtask(new Move(gameObject, position));
        }

        public void DoHarvest(GameObject resource)
        {
            brain.RemoveAllSubtasks();
            brain.AddSubtask(new Harvest(gameObject, resource));
        }

        public void DoBuild()
        {
        }

        public void DoRepair()
        {
        }

        public void DoAttack()
        {

        }
    }
}
