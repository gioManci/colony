using UnityEngine;
using System;
using Colony.UI;
using Colony.Resources;

namespace Colony {
[RequireComponent(typeof(Selectable))]
[RequireComponent(typeof(Aged))]
public class Larva : MonoBehaviour {
	public float WorkerIncubationTime;
	public float DroneIncubationTime;
	public float QueenIncubationTime;

	private float elapsedTime;
	private bool countdownStarted;
	private float incubationTime;
	private string beeType;
	private Aged aged;

	public GameObject BreedingCell;

	public bool IsGrowing { get { return countdownStarted; } }

	void Awake() {
		elapsedTime = 0.0f;
		countdownStarted = false;
	}

	void Start() {
		var sel = GetComponent<Selectable>();
		sel.OnSelect += () => {
			if (!countdownStarted)
				UIController.Instance.SetBottomPanel(UIController.BPType.Larva);
			else {
				UIController.Instance.SetBPText("Growing into " + beeType);
				UIController.Instance.SetBottomPanel(UIController.BPType.Text);
			}
		};
		sel.OnDeselect += () => UIController.Instance.SetBottomPanel(UIController.BPType.None);
		aged = GetComponent<Aged>();
		aged.Active = false;
		transform.Translate(new Vector3(0, 0, -Units.ZIndex.Larva));
	}

	void Update() {
		if (countdownStarted) {
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= incubationTime) {
				CreateBee();
				EntityManager.Instance.DestroyEntity(gameObject);
			}
		}
	}

	public void StartGrowing(string beeType) {
		Debug.Assert(BreedingCell != null, "Breeding cell is null for larva!");

		if (!countdownStarted) {
			this.beeType = beeType;

			switch (beeType) {
			case "WorkerBee":
				incubationTime = WorkerIncubationTime;
				break;
			case "DroneBee":
				incubationTime = DroneIncubationTime;
				break;
			case "QueenBee":
				incubationTime = QueenIncubationTime;
				break;
			default:
				throw new Exception("Invalid bee type: " + beeType);
			}

			countdownStarted = true;
			aged.Age = aged.Lifespan = incubationTime;
			aged.Active = true;
			GetComponent<Selectable>().AlwaysShowSelectionSprite = true;
			UIController.Instance.resourceManager.RemoveResources(Costs.Larva);
			UIController.Instance.SetBPText("Growing into " + beeType);
			UIController.Instance.SetBottomPanel(UIController.BPType.Text);
		}
	}

	private void CreateBee() {
		switch (beeType) {
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
		BreedingCell.GetComponent<Cell>().CellState = Cell.State.Storage;
	}
}
}
