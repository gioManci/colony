using UnityEngine;
using System.Collections;
using System;

public class Think : ComplexTask
{
    public Think(GameObject owner) : base(owner, TaskType.Think) { }

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

        ProcessSubtasks();

        return status;
    }

    public override void Terminate() { }

    public bool IsCurrentSubtask(TaskType taskType)
    {
        if (subtasks.Count > 0)
        {
            return subtasks.Peek().Type == taskType;
        }
        return false;
    }
}
