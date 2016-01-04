using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Colony.Hive;

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
        public GameObject beehive;

        public event Action OnAllBeeDead;

        public List<GameObject> Bees { get; private set; }
        public List<GameObject> Larvae { get; private set; }
        public List<GameObject> Enemies { get; private set; }
        public List<GameObject> Resources { get; private set; }
        public List<GameObject> Beehives { get; private set; }

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
	    InitializeLarvae();
        }

        private void InitializeBees()
        {
            Bees = new List<GameObject>();
            GameObject[] workers = GameObject.FindGameObjectsWithTag("WorkerBee");
            Bees.AddRange(workers);
            GameObject[] drones = GameObject.FindGameObjectsWithTag("DroneBee");
            Bees.AddRange(drones);
            GameObject[] queens = GameObject.FindGameObjectsWithTag("QueenBee");
            Bees.AddRange(queens);
        }

        private void InitializeEnemies()
        {
            Enemies = new List<GameObject>();
            GameObject[] wasps = GameObject.FindGameObjectsWithTag("Wasp");
            Enemies.AddRange(wasps);
            GameObject[] hornets = GameObject.FindGameObjectsWithTag("Hornet");
            Enemies.AddRange(hornets);
        }

        private void InitializeResources()
        {
            Resources = new List<GameObject>();
            GameObject[] flowers = GameObject.FindGameObjectsWithTag("Flower");
            Resources.AddRange(flowers);
            GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
            Resources.AddRange(trees);
        }

        private void InitializeBeehives()
        {
            Beehives = new List<GameObject>();
            GameObject[] hives = GameObject.FindGameObjectsWithTag("Beehive");
            Beehives.AddRange(hives);
        }

        private void InitializeLarvae()
        {
            Larvae = new List<GameObject>();
            GameObject[] larvae = GameObject.FindGameObjectsWithTag("Larva");
            Larvae.AddRange(larvae);
        }

        public void CreateWorkerBee(Vector2 position)
        {
            GameObject newBee = (GameObject)Instantiate(workerBee, position, Quaternion.identity);
            Bees.Add(newBee);
        }

        public void CreateQueenBee(Vector2 position)
        {
            GameObject newBee = (GameObject)Instantiate(queenBee, position, Quaternion.identity);
            Bees.Add(newBee);
        }

        public void CreateDroneBee(Vector2 position)
        {
            GameObject newBee = (GameObject)Instantiate(droneBee, position, Quaternion.identity);
            Bees.Add(newBee);
        }

        public void CreateLarva(Vector2 position)
        {
            GameObject newLarva = (GameObject)Instantiate(larva, position, Quaternion.identity);
	    Larvae.Add(newLarva);
        }

        public void CreateBeehive(Vector2 position)
        {
            GameObject hive = (GameObject)Instantiate(beehive, position, Quaternion.identity);
            Beehives.Add(hive);
            hive.GetComponent<Hive.Hive>().Radius = 1;
            hive.SetActive(true);
        }

        public void CreateWasp(Vector2 position)
        {
            GameObject enemy = (GameObject)Instantiate(wasp, position, Quaternion.identity);
            Enemies.Add(enemy);
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
		    case "Larva":
			DestroyLarva(entity);
			break;
                    default:
                        Destroy(entity);
                        break;
                }
            }
        }

        public void DestroyBee(GameObject deadBee)
        {
            Bees.Remove(deadBee);
            Destroy(deadBee);
            if (Bees.Count == 0 && OnAllBeeDead != null)
            {
                OnAllBeeDead();
            }
        }

        public void DestroyEnemy(GameObject deadEnemy)
        {
            Enemies.Remove(deadEnemy);
            Destroy(deadEnemy);
        }

        public void DestroyResource(GameObject deadResource)
        {
            Resources.Remove(deadResource);
            Destroy(deadResource);
        }

        public void DestroyBeehive(GameObject deadBeehive)
        {
            Beehives.Remove(deadBeehive);
            Destroy(deadBeehive);
        }

        public void DestroyLarva(GameObject deadLarva)
        {
            Larvae.Remove(deadLarva);
            Destroy(deadLarva);
        }

        public void DestroyCell(GameObject deadCell)
        {
            //TODO: Implement cell destruction.
        }

        public GameObject[] GetNearbyUnits(Vector2 position, float radius)
        {
            List<GameObject> neighbors = new List<GameObject>();
            foreach (GameObject bee in Bees)
            {
                Vector2 distance = bee.transform.position - (Vector3)position;
                if (distance.sqrMagnitude < radius * radius)
                {
                    neighbors.Add(bee);
                }
            }
            foreach (GameObject enemy in Enemies)
            {
                Vector2 distance = enemy.transform.position - (Vector3)position;
                if (distance.sqrMagnitude < radius * radius)
                {
                    neighbors.Add(enemy);
                }
            }
            return neighbors.ToArray();
        }

        public GameObject[] GetNearbyCells(Vector2 position, float radius)
        {
            List<GameObject> nearbyHives = new List<GameObject>();
            foreach (GameObject hive in Beehives)
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
            if (Beehives.Count == 1)
            {
                return Beehives[0];
            }
            else if (Beehives.Count > 1)
            {
                GameObject closest = Beehives[0];
                Vector2 closestDistance = target - (Vector2)closest.transform.position;

                for (int i = 1; i < Beehives.Count; i++)
                {
                    Vector2 distanceToCheck = target - (Vector2)Beehives[i].transform.position;
                    if (closestDistance.sqrMagnitude > distanceToCheck.sqrMagnitude)
                    {
                        closestDistance = distanceToCheck;
                        closest = Beehives[i];
                    }
                }

                return closest;
            }
            else
            {
                return null;
            }
        }

	private delegate IEnumerable<Selectable> SelectableFilter(List<GameObject> lst);

	public List<Selectable> GetSelectablesIn(Rect rect) {

		SelectableFilter selectables = (lst) => lst.Select(x => x.GetComponent<Selectable>()).Where(x => x != null);

		List<Selectable> sel = new List<Selectable>(selectables(Bees));
		sel.AddRange(selectables(Resources));
		/*foreach (var beehive in Beehives) 
			sel.AddRange(beehive.GetComponent<Hive.Hive>().Cells.Select(x => 
				x.GetComponent<Selectable>()).Where(x => x != null));*/
		sel.AddRange(selectables(Enemies));
		sel.AddRange(selectables(Larvae));
		return sel.Where(x => rect.Contains(x.transform.position)).ToList();
	}

        public bool IsBee(GameObject entity)
        {
            return entity != null
                && (entity.tag == "WorkerBee"
                || entity.tag == "DroneBee"
                || entity.tag == "QueenBee");
        }

        public bool IsEnemy(GameObject entity)
        {
            return entity != null
                && (entity.tag == "Wasp"
                || entity.tag == "Hornet");
        }
    }
}
