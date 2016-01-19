using Colony.Specializations;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Colony
{

    using Random = UnityEngine.Random;

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

        public event Action<GameObject> BeeCreated;
        public event Action<GameObject> WorkerBeeCreated;
        public event Action<GameObject> QueenBeeCreated;
        public event Action<GameObject> DroneBeeCreated;
        public event Action<GameObject> DestroyingBee;
        public event Action<GameObject> DestroyingWorkerBee;
        public event Action<GameObject> DestroyingQueenBee;
        public event Action<GameObject> DestroyingDroneBee;
        public event Action<GameObject> DestroyingEnemy;
        public event Action<GameObject> DestroyingWasp;
        public event Action<GameObject> DestroyingHornet;

        public List<GameObject> Bees { get; private set; }
        public List<GameObject> Larvae { get; private set; }
        public List<GameObject> Enemies { get; private set; }
        public List<GameObject> Resources { get; private set; }
        public List<GameObject> Beehives { get; private set; }
        public List<GameObject> ForagerBees { get; private set; }
        public List<GameObject> GuardBees { get; private set; }
        public List<GameObject> InkeeperBees { get; private set; }

        private Canvas worldCanvas;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            worldCanvas = GameObject.Find("WorldCanvas").GetComponent<Canvas>();

            InitializeBeehives();
            InitializeBees();
            InitializeEnemies();
            InitializeResources();
            InitializeLarvae();
            Stats.BeeSpecialized += OnBeeSpecialized;
        }

        private void OnBeeSpecialized(GameObject bee)
        {
            Stats stats = bee.GetComponent<Stats>();
            switch (stats.Specialization)
            {
                case SpecializationType.Forager:
                    ForagerBees.Add(bee);
                    break;
                case SpecializationType.Guard:
                    GuardBees.Add(bee);
                    break;
                case SpecializationType.Inkeeper:
                    InkeeperBees.Add(bee);
                    break;
                default:
                    break;
            }
        }

        private void InitializeBees()
        {
            Bees = new List<GameObject>();
            ForagerBees = new List<GameObject>();
            GuardBees = new List<GameObject>();
            InkeeperBees = new List<GameObject>();
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

        public GameObject CreateWorkerBee(Vector2 position)
        {
            GameObject newBee = (GameObject)Instantiate(workerBee, position, Quaternion.identity);
            Bees.Add(newBee);
            newBee.transform.SetParent(worldCanvas.transform);

            if (BeeCreated != null)
            {
                BeeCreated(newBee);
            }
            if (WorkerBeeCreated != null)
            {
                WorkerBeeCreated(newBee);
            }
            return newBee;
        }

        public GameObject CreateQueenBee(Vector2 position)
        {
            GameObject newBee = (GameObject)Instantiate(queenBee, position, Quaternion.identity);
            Bees.Add(newBee);
            newBee.transform.SetParent(worldCanvas.transform);

            if (BeeCreated != null)
            {
                BeeCreated(newBee);
            }
            if (QueenBeeCreated != null)
            {
                QueenBeeCreated(newBee);
            }
            return newBee;
        }

        public GameObject CreateDroneBee(Vector2 position)
        {
            GameObject newBee = (GameObject)Instantiate(droneBee, position, Quaternion.identity);
            Bees.Add(newBee);
            newBee.transform.SetParent(worldCanvas.transform);

            if (BeeCreated != null)
            {
                BeeCreated(newBee);
            }
            if (DroneBeeCreated != null)
            {
                DroneBeeCreated(newBee);
            }
            return newBee;
        }

        public GameObject CreateLarva(Vector2 position)
        {
            GameObject newLarva = (GameObject)Instantiate(larva, position, Quaternion.identity);
            Larvae.Add(newLarva);
            newLarva.transform.SetParent(worldCanvas.transform);
            return newLarva;
        }

        public GameObject CreateBeehive(Vector2 position)
        {
            GameObject hive = (GameObject)Instantiate(beehive, position, Quaternion.identity);
            Beehives.Add(hive);
            hive.GetComponent<Hive.Hive>().Radius = 1;
            hive.SetActive(true);
            return hive;
        }

        public GameObject CreateWasp(Vector2 position)
        {
            GameObject enemy = (GameObject)Instantiate(wasp, position, Quaternion.identity);
            Enemies.Add(enemy);
            return enemy;
        }

	public GameObject CreateHornet(Vector2 position)
	{
		GameObject enemy = (GameObject)Instantiate(hornet, position, Quaternion.identity);
		Enemies.Add(enemy);
		return enemy;
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
            if (deadBee != null)
            {
                switch (deadBee.tag)
                {
                    case "WorkerBee":
                        DestroyWorker(deadBee);
                        break;
                    case "QueenBee":
                        DestroyQueen(deadBee);
                        break;
                    case "DroneBee":
                        DestroyDrone(deadBee);
                        break;
                    default:
                        throw new Exception("The object to destroy is not a bee");
                }
            }
        }

        private void DestroyWorker(GameObject worker)
        {
            Bees.Remove(worker);
            Stats stats = worker.GetComponent<Stats>();
            switch (stats.Specialization)
            {
                case SpecializationType.Forager:
                    ForagerBees.Remove(worker);
                    break;
                case SpecializationType.Guard:
                    GuardBees.Remove(worker);
                    break;
                case SpecializationType.Inkeeper:
                    InkeeperBees.Remove(worker);
                    break;
                default:
                    break;
            }
            if (DestroyingBee != null)
            {
                DestroyingBee(worker);
            }
            if (DestroyingWorkerBee != null)
            {
                DestroyingWorkerBee(worker);
            }
            Destroy(worker);
        }

        private void DestroyQueen(GameObject queen)
        {
            Bees.Remove(queen);
            if (DestroyingBee != null)
            {
                DestroyingBee(queen);
            }
            if (DestroyingQueenBee != null)
            {
                DestroyingQueenBee(queen);
            }
            Destroy(queen);
        }

        private void DestroyDrone(GameObject drone)
        {
            Bees.Remove(drone);
            if (DestroyingBee != null)
            {
                DestroyingBee(drone);
            }
            if (DestroyingDroneBee != null)
            {
                DestroyingDroneBee(drone);
            }
            Destroy(drone);
        }

        public void DestroyEnemy(GameObject deadEnemy)
        {
            if (deadEnemy != null)
            {
                switch (deadEnemy.tag)
                {
                    case "Wasp":
                        DestroyWasp(deadEnemy);
                        break;
                    case "Hornet":
                        DestroyHornet(deadEnemy);
                        break;
                    default:
                        throw new Exception("The object to destroy is not an enemy.");
                }
            }
        }

        private void DestroyWasp(GameObject deadWasp)
        {
            Enemies.Remove(deadWasp);
            if (DestroyingEnemy != null)
            {
                DestroyingEnemy(deadWasp);
            }
            if (DestroyingWasp != null)
            {
                DestroyingWasp(deadWasp);
            }
            Destroy(deadWasp);
        }

        private void DestroyHornet(GameObject deadHornet)
        {
            Enemies.Remove(deadHornet);
            if (DestroyingEnemy != null)
            {
                DestroyingEnemy(deadHornet);
            }
            if (DestroyingHornet != null)
            {
                DestroyingHornet(deadHornet);
            }
            Destroy(deadHornet);
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

        public List<Selectable> GetSelectablesIn(Rect rect)
        {

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

	public List<GameObject> GetRandomBees(int n, SpecializationType type = SpecializationType.None) {
		switch (type) {
		case SpecializationType.Guard:
			return getRandom(GuardBees, n);
		case SpecializationType.Forager:
			return getRandom(ForagerBees, n);
		case SpecializationType.Inkeeper:
			return getRandom(InkeeperBees, n);
		}
		return getRandom(Bees, n);
	}

	public List<GameObject> GetRandomResources(int n) {
		return getRandom(Resources, n);
	}

	private List<GameObject> getRandom(List<GameObject> b, int n) {
		// Shuffle the list of bees to kill random ones
		return new List<GameObject>(b).OrderBy(x => Guid.NewGuid()).Take(n).ToList();
	}

        public int GetBeeCount(string tag = null)
        {
            if (tag == null)
            {
                return Bees.Count;
            }

            int count = 0;
            foreach (GameObject bee in Bees)
            {
                if (bee.tag == tag)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
