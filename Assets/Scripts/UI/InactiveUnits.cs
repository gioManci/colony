using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Colony.Input;

namespace Colony.UI {

public class InactiveUnits : MonoBehaviour {

	private Text inactiveText;
	private List<GameObject> inactives = new List<GameObject>();
	private int inactiveIdx = 0;

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
		inactiveText = GetComponentInChildren<Text>();
		Debug.Assert(inactiveText != null, "Inactive Text is null in InactiveUnits!");
	}

	void Update() {
		int count = 0;
		inactives.Clear();
		foreach (GameObject bee in EntityManager.Instance.Bees) {
			if (bee.GetComponent<Controllable>().IsInactive()) {
				++count;
				inactives.Add(bee);
			}
		}
		inactiveText.text = count.ToString();
	}

	public void CycleUnits() {
		if (inactiveIdx >= inactives.Count)
			inactiveIdx = 0;

		var unit = inactives[inactiveIdx++];
		var pos = unit.transform.position;
		MouseActions.Instance.DeselectAll();
		unit.GetComponent<Selectable>().Select();
		MouseActions.Instance.UpdateSelected(unit.GetComponent<Selectable>());
		Camera.main.transform.position = new Vector3(pos.x, pos.y, Camera.main.transform.position.z);
	}
}

}