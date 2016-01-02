using UnityEngine;
using System;
using Colony.UI;
using Colony.Resources;

namespace Colony
{
    [RequireComponent(typeof(Selectable))]
    [RequireComponent(typeof(Aged))]
    public class Larva : MonoBehaviour
    {
        public float workerIncubationTime;
        public float droneIncubationTime;
        public float queenIncubationTime;

        private float elapsedTime;
        private bool countdownStarted;
        private float incubationTime;
        private string beeType;
        private Aged aged;

        void Awake()
        {
            elapsedTime = 0.0f;
            countdownStarted = false;
        }

	void Start() {
		var sel = GetComponent<Selectable>();
		sel.OnSelect += () => UIController.Instance.SetButtonsVisible(UIController.ButtonType.Larva);
		sel.OnDeselect += () => UIController.Instance.SetButtonsVisible(UIController.ButtonType.None);
        aged = GetComponent<Aged>();
        aged.Active = false;
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
                aged.Age = aged.Lifespan = incubationTime;
                aged.Active = true;
		UIController.Instance.resourceManager.RemoveResources(Costs.Larva);
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
