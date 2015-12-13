using UnityEngine;
using System;

namespace Colony.Tasks.ComplexTasks
{
    /// <summary>
    /// Represents the root task of any brain. All the tasks assigned to an agent are subtasks of this one.
    /// When no task is assigned to an agent, this is the only one in the tasks hierarchy.
    /// </summary>
    public class Think : ComplexTask
    {
        /// <summary>
        /// Creates a new Think task specifying tha agent to which it belongs.
        /// </summary>
        /// <param name="agent">The agent that owns this task.</param>
        public Think(GameObject agent) : base(agent, TaskType.Think) { }

        /// <summary>
        /// Sets the current task to active.
        /// </summary>
        public override void Activate()
        {
            status = Status.Active;
        }

        public override void OnMessage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tries to execute this task and returns the status.
        /// </summary>
        /// <returns>The status of this task.</returns>
        public override Status Process()
        {
            ActivateIfInactive();

            ProcessSubtasks();

            return status;
        }

        /// <summary>
        /// Terminates this task [Has no effect on this task]
        /// </summary>
        public override void Terminate() { }
    }
}