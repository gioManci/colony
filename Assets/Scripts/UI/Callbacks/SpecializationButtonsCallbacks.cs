using UnityEngine;
using System.Collections;
using Colony.Specializations;

namespace Colony.UI {

public class SpecializationButtonsCallbacks : MonoBehaviour {
	
	public void ForagerBee() {
		evolve(SpecializationType.Forager);
	}

	public void GuardBee() {
		evolve(SpecializationType.Guard);
	}

	public void InkeeperBee() {
		evolve(SpecializationType.Inkeeper);
	}

	private void evolve(SpecializationType type) {
		foreach (GameObject bee in EntityManager.Instance.Bees) {
			if (bee.tag == "WorkerBee" && bee.GetComponent<Selectable>().IsSelected) {
				var spec = bee.GetComponent<Stats>();
				if (spec.Spec.Type != SpecializationType.None)
					continue;

				// TODO: check resources
				spec.Specialize(type);
				Debug.Log("specializing in " + type);
				UIController.Instance.SetBottomPanel(UIController.BPType.Text);
			}
		}
	}
}

}