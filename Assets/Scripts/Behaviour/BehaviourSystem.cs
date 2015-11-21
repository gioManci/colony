using UnityEngine;
using System;
using System.Collections.Generic;

public class BehaviourSystem
{
    private Behaviour[] behaviours;

	public BehaviourSystem()
	{
        int behavioursNumber = Enum.GetValues(typeof(BehaviourType)).Length;
        behaviours = new Behaviour[behavioursNumber];
	}

    public void AddBehaviour(Behaviour behaviour)
    {
        behaviours[(int)behaviour.Type] = behaviour;
    }

    public void RemoveBehaviour(BehaviourType behaviourType)
    {
        behaviours[(int)behaviourType] = null;
    }

    public Vector2 CalculateResultingForce()
    {
        Vector2 resultingForce = Vector2.zero;

        foreach (Behaviour behaviour in behaviours)
        {
            if (behaviour != null)
            {
                resultingForce = behaviour.Compute();
            }
        }

        return resultingForce;
    }
}
