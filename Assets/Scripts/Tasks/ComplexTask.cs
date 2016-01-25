using UnityEngine;
using System.Collections.Generic;
using System;

namespace Colony.Tasks
{

    /// <summary>
    /// Abstract class that describes a complex task. A complex task is a task that needs other subtasks to 
    /// be completed in order to terminate.
    /// </summary>
    public abstract class ComplexTask : Task
    {
        /// <summary>
        /// The stack of subtasks to execute.
        /// </summary>
        protected Stack<Task> subtasks;

        /// <summary>
        /// Initializes a new complex task with the specified paramenters.
        /// </summary>
        /// <param name="agent">The agent that performs the task.</param>
        /// <param name="taskType">The type of the task.</param>
        public ComplexTask(GameObject agent, TaskType taskType) : base(agent, taskType)
        {
            subtasks = new Stack<Task>();
        }

        public Task CurrentSubtask
        {
            get
            {
                return (subtasks.Count == 0) ? null : subtasks.Peek();
            }
        }

        /// <summary>
        /// Removes the completed tasks and executes the most recent one.
        /// </summary>
        /// <returns>The status after the processing of the subtasks.</returns>
        protected Status ProcessSubtasks()
        {
            while (subtasks.Count > 0 && (subtasks.Peek().IsComplete || subtasks.Peek().HasFailed))
            {
                subtasks.Pop().Terminate();
            }

            if (subtasks.Count > 0)
            {
                Status subtaskStatus = subtasks.Peek().Process();
                if (subtaskStatus == Status.Completed && subtasks.Count > 1)
                {
                    return Status.Active;
                }
                return subtaskStatus;
            }
            else
            {
                return Status.Completed;
            }
        }

        /// <summary>
        /// Adds the specified task to the top of the subtask list. It will be the first one to be executed.
        /// </summary>
        /// <param name="subtask">The subtask to add.</param>
        public void AddSubtask(Task subtask)
        {
            subtasks.Push(subtask);
        }

        /// <summary>
        /// Removes all the current subtasks.
        /// </summary>
        public void RemoveAllSubtasks()
        {
            foreach (Task task in subtasks)
            {
                task.Terminate();
            }
            subtasks.Clear();
        }

        public bool IsCurrentSubtask(TaskType type)
        {
            return (CurrentSubtask == null) ? false : CurrentSubtask.Type == type;
        }

	public override void Terminate() {
		RemoveAllSubtasks();
	}
    }
}
