using UnityEngine;
using System;
using System.Collections.Generic;
using Colony.Behaviour.Behaviours;

namespace Colony.Behaviour
{
    public class SteeringBehaviour : MonoBehaviour
    {
        //Seek parameters
        public float seekWeight = 1.0f;

        //Arrive parameters
        public float arriveWeight = 1.0f;
        public float arriveDeceleration = 0.9f;

        //Flocking parameters
        public float alignmentWeight = 0.01f;
        public float cohesionWeight = 0.01f;
        public float separationWeight = 0.1f;

        //Wander parameters
        public float wanderWeight = 1.0f;
        public float wanderRadius = 0.5f;
        public float wanderDistance = 1.0f;
        public float wanderJitter = 1.0f;

        //Pursuit parameters
        public float pursuitWeight = 1.0f;

        private BehaviourSystem behaviourSystem;
        private Rigidbody2D rigidbody2d;
        private Stats stats;

        void Start()
        {
            behaviourSystem = new BehaviourSystem(gameObject);
            rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
            stats = gameObject.GetComponent<Stats>();

            //TODO: This should be moved outside this class
            StartFlocking();
        }

        void Update()
        {
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.up, Color.green);
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.right, Color.red);
            Debug.DrawRay(rigidbody2d.position, rigidbody2d.velocity, Color.yellow);

            Vector2 forceToApply = behaviourSystem.CalculateResultingForce();

            if (forceToApply.sqrMagnitude < 0.001)
            {
                rigidbody2d.velocity *= 0.8f;
            }
            else
            {
                Vector2 acceleration = forceToApply / rigidbody2d.mass;
                rigidbody2d.velocity += acceleration * Time.deltaTime;

                if (rigidbody2d.velocity.magnitude > stats.Speed)
                {
                    rigidbody2d.velocity = rigidbody2d.velocity.normalized * stats.Speed;
                }
                rigidbody2d.position += rigidbody2d.velocity * Time.deltaTime;

                //Rotation
                if (rigidbody2d.velocity.sqrMagnitude > 0.001)
                {
                    gameObject.transform.up = rigidbody2d.velocity.normalized;
                }
            }
        }

        public bool IsSeeking
        {
            get
            {
                return behaviourSystem.IsBehaviourOn(BehaviourType.Seek);
            }
        }

        public bool IsArriving
        {
            get
            {
                return behaviourSystem.IsBehaviourOn(BehaviourType.Arrive);
            }
        }

        public bool IsAligning
        {
            get
            {
                return behaviourSystem.IsBehaviourOn(BehaviourType.Alignment);
            }
        }

        public bool IsCohesioning
        {
            get
            {
                return behaviourSystem.IsBehaviourOn(BehaviourType.Cohesion);
            }
        }

        public bool IsSeparating
        {
            get
            {
                return behaviourSystem.IsBehaviourOn(BehaviourType.Separation);
            }
        }

        public bool IsFlocking
        {
            get
            {
                return behaviourSystem.IsBehaviourOn(BehaviourType.Alignment)
                    && behaviourSystem.IsBehaviourOn(BehaviourType.Cohesion)
                    && behaviourSystem.IsBehaviourOn(BehaviourType.Separation);
            }
        }

        public bool IsWandering
        {
            get
            {
                return behaviourSystem.IsBehaviourOn(BehaviourType.Wander);
            }
        }

        public bool IsChasing
        {
            get
            {
                return behaviourSystem.IsBehaviourOn(BehaviourType.Pursuit);
            }
        }

        public void StartSeek(Vector2 target)
        {
            behaviourSystem.AddBehaviour(new Seek(gameObject, target, seekWeight));
        }

        public void StopSeek()
        {
            behaviourSystem.RemoveBehaviour(BehaviourType.Seek);
        }

        public void StartArrive(Vector2 target)
        {
            behaviourSystem.AddBehaviour(new Arrive(gameObject, target, arriveDeceleration, arriveWeight));
        }

        public void StopArrive()
        {
            behaviourSystem.RemoveBehaviour(BehaviourType.Arrive);
        }

        public void StartSeparation()
        {
            behaviourSystem.AddBehaviour(new Separation(gameObject, behaviourSystem, separationWeight));
            behaviourSystem.NeighborUsers++;
        }

        public void StopSeparation()
        {
            behaviourSystem.RemoveBehaviour(BehaviourType.Separation);
            behaviourSystem.NeighborUsers--;
        }

        public void StartAlignment()
        {
            behaviourSystem.AddBehaviour(new Alignment(gameObject, behaviourSystem, alignmentWeight));
            behaviourSystem.NeighborUsers++;
        }

        public void StopAlignment()
        {
            behaviourSystem.RemoveBehaviour(BehaviourType.Alignment);
            behaviourSystem.NeighborUsers--;
        }

        public void StartCohesion()
        {
            behaviourSystem.AddBehaviour(new Cohesion(gameObject, behaviourSystem, cohesionWeight));
            behaviourSystem.NeighborUsers++;
        }

        public void StopCohesion()
        {
            behaviourSystem.RemoveBehaviour(BehaviourType.Cohesion);
            behaviourSystem.NeighborUsers--;
        }

        public void StartFlocking()
        {
            StartSeparation();
            StartAlignment();
            StartCohesion();
        }

        public void StopFlocking()
        {
            StopSeparation();
            StopAlignment();
            StopCohesion();
        }

        public void StartWander()
        {
            behaviourSystem.AddBehaviour(
                new Wander(gameObject, wanderRadius, wanderDistance, wanderJitter, wanderWeight));
        }

        public void StopWander()
        {
            behaviourSystem.RemoveBehaviour(BehaviourType.Wander);
        }

        public void StartPursuit(GameObject target)
        {
            behaviourSystem.AddBehaviour(new Pursuit(gameObject, target, pursuitWeight));
        }

        public void StopPursuit()
        {
            behaviourSystem.RemoveBehaviour(BehaviourType.Pursuit);
        }
    }
}