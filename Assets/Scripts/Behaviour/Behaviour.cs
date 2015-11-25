using UnityEngine;
using System.Collections;

namespace Colony.Behaviour
{

    public abstract class Behaviour
    {
        private BehaviourType type;

        protected GameObject agent;
        protected float weight;

        public BehaviourType Type
        {
            get
            {
                return type;
            }
        }

        public float Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        public Behaviour(GameObject actor, BehaviourType behaviourType, float weight)
        {
            this.agent = actor;
            type = behaviourType;
            this.weight = weight;
        }

        public abstract Vector2 Compute();
    }

}
