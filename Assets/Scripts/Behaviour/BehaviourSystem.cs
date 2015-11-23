using UnityEngine;
using System;
using System.Collections.Generic;

namespace Colony.Behaviour {

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
        Vector2 force, resultingForce = Vector2.zero;

        foreach (Behaviour behaviour in behaviours)
        {
            if (behaviour != null)
            {
                force = behaviour.Compute();
                if (!TryAddForce(ref resultingForce, force))
                {
                    return resultingForce;
                }
            }
        }

        return resultingForce;
    }

    /// <summary>
    /// Tries to add the current force to the cumulative one. If the cumulative force has already reached
    /// the maxiumum amount, it returns false. Else it adds the current force clamping it if it exceeds
    /// the maximum amount.
    /// </summary>
    /// <param name="cumulativeForce">The cumulative force to be applied to the object.</param>
    /// <param name="currentForce">The current force to be added.</param>
    /// <returns>False if the cumulative force has reached the maximum amount possible.</returns>
    private bool TryAddForce(ref Vector2 cumulativeForce, Vector2 currentForce)
    {
        //TODO: 5.0f is the maximum force that can be applied to the object. Substitute with constant.
        float forceGap = 5.0f - cumulativeForce.magnitude;

        //If the cumulative force exceeds the maximum that can be applied, return false.
        if (forceGap <= 0)
        {
            return false;
        }

        //If the current force to be applied is within the gap, simply adds it. Else, clamps it to the max.
        if (currentForce.magnitude < forceGap)
        {
            cumulativeForce += currentForce;
        }
        else
        {
            cumulativeForce += currentForce.normalized * forceGap;
        }

        return true;
    }
}

}
