using UnityEngine;
using System.Collections;
using System;

public class Harvest : ComplexTask
{
    public Harvest(GameObject owner) : base(owner, TaskType.Harvest)
    {
    }

    public override void Activate()
    {
        throw new NotImplementedException();
    }

    public override void OnMessage()
    {
        throw new NotImplementedException();
    }

    public override Status Process()
    {
        throw new NotImplementedException();
    }

    public override void Terminate()
    {
        throw new NotImplementedException();
    }
}
