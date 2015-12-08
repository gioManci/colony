using UnityEngine;
using System.Collections;
using Colony.Tasks.ComplexTasks;
using Colony.Tasks.BasicTasks;

namespace Colony.Tasks
{
    /// <summary>
    /// Handles the tasks of the object to which it is attached. Allows to set simple or complex goals to
    /// achieve, then the systems executes them and eventually decompose them into simple ones.
    /// </summary>
    public class QueenBeeBrain : MonoBehaviour
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
            brain.AddSubtask(new Move(gameObject, position));
        }
    }
}