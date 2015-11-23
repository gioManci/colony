using UnityEngine;
using System.Collections;

namespace Colony.Behaviour {

public abstract class Behaviour
{
    protected BehaviourType type;

    protected GameObject actor;

    public BehaviourType Type
    {
        get
        {
            return type;
        }
    }

    public Behaviour(GameObject actor, BehaviourType behaviourType)
    {
        this.actor = actor;
        type = behaviourType;
    }

    public abstract Vector2 Compute();
}

}
