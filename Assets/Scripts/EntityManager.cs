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
        private List<GameObject> beehives;

        void Start()
        {
            Instance = this;

            InitializeBeehives();
            InitializeBees();
            InitializeEnemies();
            InitializeResources();
        }

        private void InitializeBees()
        {
            bees = new List<GameObject>();
            GameObject[] workers = GameObject.FindGameObjectsWithTag("WorkerBee");
            bees.AddRange(workers);
            GameObject[] drones = GameObject.FindGameObjectsWithTag("DroneBee");
            bees.AddRange(drones);
            GameObject[] queens = GameObject.FindGameObjectsWithTag("QueenBee");
            bees.AddRange(queens);
        }

        private void InitializeEnemies()
        {
            enemies = new List<GameObject>();
            GameObject[] wasps = GameObject.FindGameObjectsWithTag("Wasp");
            enemies.AddRange(wasps);
            GameObject[] hornets = GameObject.FindGameObjectsWithTag("Hornet");
            enemies.AddRange(hornets);
        }

        private void InitializeResources()
        {
            resources = new List<GameObject>();
            GameObject[] flowers = GameObject.FindGameObjectsWithTag("Flower");
            resources.AddRange(flowers);
            GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
            resources.AddRange(trees);
        }

        private void InitializeBeehives()
        {
            beehives = new List<GameObject>();
            GameObject[] hives = GameObject.FindGameObjectsWithTag("Beehive");
            beehives.AddRange(hives);
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

        public void DestroyBee(GameObject deadBee)
        {
            bees.Remove(deadBee);
            Destroy(deadBee);
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

        public GameObject GetClosestHive(Vector2 target)
        {
            if (beehives.Count == 1)
            {
                return beehives[0];
            }
            else if (beehives.Count > 1)
            {
                GameObject closest = beehives[0];
                Vector2 closestDistance = target - (Vector2)closest.transform.position;

                for (int i = 1; i < beehives.Count; i++)
                {
                    Vector2 distanceToCheck = target - (Vector2)beehives[i].transform.position;
                    if (closestDistance.sqrMagnitude > distanceToCheck.sqrMagnitude)
                    {
                        closestDistance = distanceToCheck;
                        closest = beehives[i];
                    }
                }

                return closest;
            }
            else
            {
                return null;
            }
        }
    }
}