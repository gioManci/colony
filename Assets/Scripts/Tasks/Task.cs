using UnityEngine;
using System.Collections;

namespace Colony.Tasks
{
    /// <summary>
    /// Basic abstract class that describes a Task.
    /// </summary>
    public abstract class Task
    {
        /// <summary>
        /// Describes the status of a task. It can either be: Active, Inactive, Completed or Failed.
        /// </summary>
        public enum Status { Active, Inactive, Completed, Failed }

        /// <summary>
        /// The GameObject that is performing this task.
        /// </summary>
        protected GameObject agent;

        /// <summary>
        /// The status of this task.
        /// </summary>
        protected Status status;

        /// <summary>
        /// The type of this task.
        /// </summary>
        protected TaskType type;

        /// <summary>
        /// Gets the type of the current task.
        /// </summary>
        public TaskType Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Returns true if the current task has succesfully been completed.
        /// </summary>
        public bool IsComplete { get { return status == Status.Completed; } }

        /// <summary>
        /// Returns true if the current task is active.
        /// </summary>
        public bool IsActive { get { return status == Status.Active; } }

        /// <summary>
        /// Returns true if the current task is not active.
        /// </summary>
        public bool IsInactive { get { return status == Status.Inactive; } }

        /// <summary>
        /// Returns true if the current task has failed.
        /// </summary>
        public bool HasFailed { get { return status == Status.Failed; } }

        /// <summary>
        /// Initializes a new task with the specified paramenters.
        /// </summary>
        /// <param name="agent">The agent that performs the task.</param>
        /// <param name="taskType">The type of the task.</param>
        public Task(GameObject agent, TaskType taskType)
        {
            this.agent = agent;
            type = taskType;
            status = Status.Inactive;
        }

        /// <summary>
        /// Function that is called when the task is activated.
        /// </summary>
        public abstract void Activate();

        /// <summary>
        /// Function that is called every step when the task is running.
        /// </summary>
        /// <returns>The status of the task after processing.</returns>
        public abstract Status Process();

        /// <summary>
        /// Function that is called when the task is terminated.
        /// </summary>
        public abstract void Terminate();

        public abstract void OnMessage();

        /// <summary>
        /// If the task is inactive, reactivate it.
        /// </summary>
        public void ActivateIfInactive()
        {
            if (IsInactive)
            {
                Activate();
            }
        }

        /// <summary>
        /// If the current task has failed, sets its status to inactive in order to reactivate it in the next update step.
        /// </summary>
        public void ReactivateIfFailed()
        {
            if (HasFailed)
            {
                status = Status.Inactive;
            }
        }
    }
}