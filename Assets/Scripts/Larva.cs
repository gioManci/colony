using UnityEngine;
using System;

namespace Colony
{
    public class Larva : MonoBehaviour
    {
        public float workerIncubationTime;
        public float droneIncubationTime;
        public float queenIncubationTime;

        private float elapsedTime;
        private bool countdownStarted;
        private float incubationTime;
        private string beeType;

        void Awake()
        {
            elapsedTime = 0.0f;
            countdownStarted = false;
        }

        void Update()
        {
            if (countdownStarted)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= incubationTime)
                {
                    CreateBee();
                    EntityManager.Instance.DestroyEntity(gameObject);
                }
            }
            //ATTENTION: For debug purposes only, must be removed!
            else
            {
                StartGrowing("WorkerBee");
            }
        }

        public void StartGrowing(string beeType)
        {
            if (!countdownStarted)
            {
                this.beeType = beeType;

                switch (beeType)
                {
                    case "WorkerBee":
                        incubationTime = workerIncubationTime;
                        break;
                    case "DroneBee":
                        incubationTime = droneIncubationTime;
                        break;
                    case "QueenBee":
                        incubationTime = queenIncubationTime;
                        break;
                    default:
                        throw new Exception("Invalid bee type: " + beeType);
                }

                countdownStarted = true;
            }
        }

        private void CreateBee()
        {
            switch (beeType)
            {
                case "WorkerBee":
                    EntityManager.Instance.CreateWorkerBee(gameObject.transform.position);
                    break;
                case "DroneBee":
                    EntityManager.Instance.CreateDroneBee(gameObject.transform.position);
                    break;
                case "QueenBee":
                    EntityManager.Instance.CreateQueenBee(gameObject.transform.position);
                    break;
                default:
                    throw new Exception("Impossible to create a bee of type: " + beeType);
            }
        }
    }
}