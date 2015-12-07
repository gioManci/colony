using UnityEngine;
using System;
using System.Collections.Generic;
using Colony.Behaviour.Behaviours;

namespace Colony.Behaviour
{
    public class SteeringBehaviour : MonoBehaviour
    {
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

            if (forceToApply.sqrMagnitude < 0.1)
            {
                rigidbody2d.velocity *= 0.5f;
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
                if (rigidbody2d.velocity.sqrMagnitude > 0.0001)
                {
                    gameObject.transform.up = rigidbody2d.velocity.normalized;
                }
            }
        }

        public void StartSeek(Vector2 target)
        {
            behaviourSystem.AddBehaviour(new Seek(gameObject, target, 1.0f));
        }

        public void StopSeek()
        {
            behaviourSystem.RemoveBehaviour(BehaviourType.Seek);
        }

        public void StartArrive(Vector2 target)
        {
            behaviourSystem.AddBehaviour(new Arrive(gameObject, target, 0.9f, 1.0f));
        }

        public void StopArrive()
        {
            behaviourSystem.RemoveBehaviour(BehaviourType.Arrive);
        }

        public void StartSeparation()
        {
            behaviourSystem.AddBehaviour(new Separation(gameObject, behaviourSystem, 0.1f));
            behaviourSystem.NeighborUsers++;
        }

        public void StopSeparation()
        {
            behaviourSystem.RemoveBehaviour(BehaviourType.Separation);
            behaviourSystem.NeighborUsers--;
        }

        public void StartAlignment()
        {
            behaviourSystem.AddBehaviour(new Alignment(gameObject, behaviourSystem, 0.01f));
            behaviourSystem.NeighborUsers++;
        }

        public void StopAlignment()
        {
            behaviourSystem.RemoveBehaviour(BehaviourType.Alignment);
            behaviourSystem.NeighborUsers--;
        }

        public void StartCohesion()
        {
            behaviourSystem.AddBehaviour(new Cohesion(gameObject, behaviourSystem, 0.01f));
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
    }
}