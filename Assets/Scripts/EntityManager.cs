using UnityEngine;
using System.Collections.Generic;

namespace Colony
{
    public class EntityManager : MonoBehaviour
    {
        public static EntityManager Instance { get; private set; }

        //Prefabs
        public GameObject workerBee;
        public GameObject droneBee;
        public GameObject queenBee;
        public GameObject larva;
        public GameObject egg;
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
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

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

        public void CreateLarva(Vector2 position)
        {
            Instantiate(larva, position, Quaternion.identity);
        }

        public void DestroyEntity(GameObject entity)
        {
            if (entity != null)
            {
                switch (entity.tag)
                {
                    case "WorkerBee":
                    case "DroneBee":
                    case "QueenBee":
                        DestroyBee(entity);
                        break;
                    case "Wasp":
                    case "Hornet":
                        DestroyEnemy(entity);
                        break;
                    case "Flower":
                    case "Tree":
                        DestroyResource(entity);
                        break;
                    case "Cell":
                        DestroyCell(entity);
                        break;
                    case "Beehive":
                        DestroyBeehive(entity);
                        break;
                    default:
                        Destroy(entity);
                        break;
                }
            }
        }

        public void DestroyBee(GameObject deadBee)
        {
            bees.Remove(deadBee);
            Destroy(deadBee);
        }

        public void DestroyEnemy(GameObject deadEnemy)
        {
            enemies.Remove(deadEnemy);
            Destroy(deadEnemy);
        }

        public void DestroyResource(GameObject deadResource)
        {
            resources.Remove(deadResource);
            Destroy(deadResource);
        }

        public void DestroyBeehive(GameObject deadBeehive)
        {
            beehives.Remove(deadBeehive);
            Destroy(deadBeehive);
        }

        public void DestroyCell(GameObject deadCell)
        {
            //TODO: Implement cell destruction.
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

        public GameObject[] GetNearbyCells(Vector2 position, float radius)
        {
            List<GameObject> nearbyHives = new List<GameObject>();
            foreach (GameObject hive in beehives)
            {
                Hive.Hive hiveComponent = hive.GetComponent<Hive.Hive>();
                foreach (GameObject hiveCell in hiveComponent.Cells)
                {
                    Vector2 distance = hiveCell.transform.position - (Vector3)position;
                    if (distance.sqrMagnitude < radius * radius)
                    {
                        nearbyHives.Add(hiveCell);
                    }
                }
            }
            return nearbyHives.ToArray();
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

        public bool IsBee(GameObject entity)
        {
            return entity != null
                && (entity.tag == "WorkerBee"
                || entity.tag == "DroneBee"
                || entity.tag == "QueenBee");
        }
    }
}