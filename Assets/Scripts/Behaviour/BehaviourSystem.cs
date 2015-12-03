using UnityEngine;
using System;
using System.Collections.Generic;

namespace Colony.Behaviour
{
    /// <summary>
    /// Represents a steering behaviours systems. Allows to plug in different behaviours and calculate the
    /// resulting force applied to an object due to them.
    /// </summary>
    public class BehaviourSystem
    {
        private Behaviour[] behaviours;
        private GameObject[] neighbors;
        private int neighborUsers;

        //TODO: This variable should be removed to decouple the behaviour system from the game objects.
        private GameObject owner;

        /// <summary>
        /// The number of behaviours in the list needing the neighbors vector to compute its force.
        /// </summary>
        public int NeighborUsers
        {
            get
            {
                return neighborUsers;
            }
            set
            {
                if (value < 0)
                {
                    neighborUsers = 0;
                }
                else
                {
                    neighborUsers = value;
                }
            }
        }

        /// <summary>
        /// Gets the last updated list of neighbors.
        /// </summary>
        public GameObject[] Neighbors
        {
            get
            {
                return neighbors;
            }
        }

        /// <summary>
        /// Creates a new behaviour system.
        /// </summary>
        public BehaviourSystem(/*TODO: This parameter should be removed*/ GameObject owner)
        {
            int behavioursNumber = Enum.GetValues(typeof(BehaviourType)).Length;
            behaviours = new Behaviour[behavioursNumber];
            neighbors = new GameObject[0];
            NeighborUsers = 0;
            //entityManager = GameObject.FindObjectOfType<EntityManager>();

            //TODO: This variable should be removed.
            this.owner = owner;
        }

        /// <summary>
        /// Adds the behaviour to the list of behaviours to compute. It overwrites an existing one of the
        /// same type, if any.
        /// </summary>
        /// <param name="behaviour">The behaviour to add.</param>
        public void AddBehaviour(Behaviour behaviour)
        {
            behaviours[(int)behaviour.Type] = behaviour;
        }

        /// <summary>
        /// Removes from the list the behaviour specified by behaviourType. If no behaviour of such type exists,
        /// nothing happens.
        /// </summary>
        /// <param name="behaviourType">The type of the behaviour to remove.</param>
        public void RemoveBehaviour(BehaviourType behaviourType)
        {
            behaviours[(int)behaviourType] = null;
        }

        /// <summary>
        /// Returns the resulting force applied to this agent by evaluating all the active behaviours.
        /// </summary>
        /// <returns></returns>
        public Vector2 CalculateResultingForce()
        {
            Vector2 force, resultingForce = Vector2.zero;

            if (NeighborUsers > 0)
            {
                neighbors = EntityManager.Instance.GetNearbyUnits(owner.transform.position, 1.0f);
            }

            foreach (Behaviour behaviour in behaviours)
            {
                if (behaviour != null)
                {
                    force = behaviour.Compute() * behaviour.Weight;
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
