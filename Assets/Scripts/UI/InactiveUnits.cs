using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Colony.Input;

namespace Colony.UI {

public class InactiveUnits : MonoBehaviour {

	private Text inactiveText;
	private Text guardText;
	private Text foragerText;
	private Text inkeeperText;
	private List<GameObject> inactives = new List<GameObject>();
	private int cycleIdx = 0;

	// Make this class a singleton
	public static InactiveUnits Instance { get; private set; }

	private InactiveUnits() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}
		
	// Use this for initialization
	void Start() {
		inactiveText = GameObject.Find("InactiveText").GetComponent<Text>();
		guardText = GameObject.Find("GuardsText").GetComponent<Text>();
		foragerText = GameObject.Find("ForagerText").GetComponent<Text>();
		inkeeperText = GameObject.Find("InkeeperText").GetComponent<Text>();
		Debug.Assert(inactiveText != null, "Inactive Text is null in InactiveUnits!");
	}

	void Update() {
		int count = 0,
		    guardCount = 0,
		    foragerCount = 0,
		    inkeeperCount = 0;
		inactives.Clear();
		foreach (GameObject bee in EntityManager.Instance.Bees) {
			if (bee.GetComponent<Controllable>().IsInactive()) {
				++count;
				inactives.Add(bee);
			}
			switch (bee.GetComponent<Stats>().Spec.Type) {
			case Specializations.SpecializationType.Guard:
				++guardCount;
				break;
			case Specializations.SpecializationType.Forager:
				++foragerCount;
				break;
			case Specializations.SpecializationType.Inkeeper:
				++inkeeperCount;
				break;
			}
		}
		inactiveText.text = count.ToString();
		guardText.text = guardCount.ToString();
		foragerText.text = foragerCount.ToString();
		inkeeperText.text = inkeeperCount.ToString();
	}

	public void CycleInactives() {
		cycleUnits(inactives);
	}

	public void CycleGuards() {
		cycleUnits(EntityManager.Instance.GuardBees);
	}

	public void CycleForagers() {
		cycleUnits(EntityManager.Instance.ForagerBees);
	}

	public void CycleInkeepers() {
		cycleUnits(EntityManager.Instance.InkeeperBees);
	}

	private void cycleUnits(List<GameObject> lst) {
		if (lst.Count == 0)
			return;
		
		if (cycleIdx >= lst.Count)
			cycleIdx = 0;

		var unit = lst[cycleIdx++];
		var pos = unit.transform.position;
		MouseActions.Instance.DeselectAll();
		unit.GetComponent<Selectable>().Select();
		MouseActions.Instance.UpdateSelected(unit.GetComponent<Selectable>());
		Camera.main.transform.position = new Vector3(pos.x, pos.y, Camera.main.transform.position.z);
	}
}

}