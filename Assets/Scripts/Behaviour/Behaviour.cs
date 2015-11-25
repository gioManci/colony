using UnityEngine;
using System.Collections;

namespace Colony.Behaviour
{
    /// <summary>
    /// Basic abstract class that represents a generic behaviour.
    /// </summary>
    public abstract class Behaviour
    {
        private BehaviourType type;

        /// <summary>
        /// The agent that owns this behaviour.
        /// </summary>
        protected GameObject agent;

        /// <summary>
        /// The weight of this behaviour in the resultant force calculation.
        /// </summary>
        protected float weight;

        /// <summary>
        /// Gets the type of this behaviour.
        /// </summary>
        public BehaviourType Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Gets or sets the weight associated to this behaviour.
        /// </summary>
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

        /// <summary>
        /// Initializes a new Behaviour with the specified parameters.
        /// </summary>
        /// <param name="agent">The agent that owns this behaviour.</param>
        /// <param name="behaviourType">The type of this behaviour.</param>
        /// <param name="weight">The weight associated to this behaviour. It is used in the calculation of the
        /// resulting force to apply to the agent.</param>
        public Behaviour(GameObject agent, BehaviourType behaviourType, float weight)
        {
            this.agent = agent;
            type = behaviourType;
            this.weight = weight;
        }

        /// <summary>
        /// Computes the steering force due to this behaviour.
        /// </summary>
        /// <returns>The steering force caused by this behaviour.</returns>
        public abstract Vector2 Compute();
    }

}
