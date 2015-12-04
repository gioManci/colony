using UnityEngine;
using System.Collections.Generic;
using Colony.Tasks;

namespace Colony
{
    public class EntityManager : MonoBehaviour
    {
        public static EntityManager Instance { get; private set; }
        public GameObject workerBee;
        public GameObject droneBee;
        public GameObject queenBee;
        public GameObject wasp;
        public GameObject hornet;
        public GameObject flower;
        public GameObject tree;

        private List<GameObject> bees;
        private List<GameObject> enemies;
        private List<GameObject> resources;

        void Start()
        {
            Instance = this;
            
            InitializeBees();
            InitializeEnemies();
            InitializeResources();
        }

        private void InitializeBees()
        {
            bees = new List<GameObject>();
            WorkerBeeBrain[] workers = FindObjectsOfType<WorkerBeeBrain>();
            foreach (WorkerBeeBrain workerBee in workers)
            {
                bees.Add(workerBee.gameObject);
            }
        }

        private void InitializeEnemies()
        {
            enemies = new List<GameObject>();
        }

        private void InitializeResources()
        {
            resources = new List<GameObject>();
        }

        public void CreateWorkerBee(Vector2 position)
        {
            GameObject newBee = (GameObject)Instantiate(workerBee, position, Quaternion.identity);
            bees.Add(newBee);
        }

        public void CreateQueenBee(Vector2 position)
        {
            GameObject newBee = (GameObject)Instantiate(queenBee, position, Quaternion.identity);
            bees.Add(newBee);
        }

        public void CreateDroneBee(Vector2 position)
        {
            GameObject newBee = (GameObject)Instantiate(droneBee, position, Quaternion.identity);
            bees.Add(newBee);
        }

        public void DestroyEntity(GameObject entity)
        {
            Destroy(entity);
        }

        public GameObject[] GetNearbyUnits(Vector2 position, float radius)
        {
            List<GameObject> neighbors = new List<GameObject>();
            foreach (GameObject bee in bees)
            {
                Vector2 distance = bee.transform.position - (Vector3)position;
                if (distance.sqrMagnitude < radius * radius)
                {
                    neighbors.Add(bee);
                }
            }
            return neighbors.ToArray();
        }
    }
}