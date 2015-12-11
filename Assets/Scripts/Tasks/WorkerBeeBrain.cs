using UnityEngine;
using Colony.Tasks.BasicTasks;
using Colony.Tasks.ComplexTasks;

namespace Colony.Tasks
{
    /// <summary>
    /// Handles the tasks of the object to which it is attached. Allows to set simple or complex goals to
    /// achieve, then the systems executes them and eventually decompose them into simple ones.
    /// </summary>
    public class WorkerBeeBrain : MonoBehaviour
    {
        private ComplexTask brain;

        void Awake()
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

        /// <summary>
        /// Tells the game object to move to the specified position.
        /// </summary>
        /// <param name="position">The position where to move.</param>
        public void DoMove(Vector2 position)
        {
            brain.RemoveAllSubtasks();
            brain.AddSubtask(new Move(gameObject, position, 0.5f));
        }

        /// <summary>
        /// Tells the game object to harvest from the specified resource.
        /// </summary>
        /// <param name="resource">The resource to harvest from.</param>
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
