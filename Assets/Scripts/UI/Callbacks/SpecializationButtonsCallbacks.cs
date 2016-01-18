using UnityEngine;
using System.Collections;
using Colony.Specializations;
using Colony.Resources;

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
				if (UIController.Instance.resourceManager.RequireResources(Costs.Get("Spec" + type))) {
					spec.Specialize(type);
					UIController.Instance.resourceManager.RemoveResources(Costs.Get("Spec" + type));
				} else
					TextController.Instance.Add("Not enough resource to evolve into " + type + "!");
				UIController.Instance.SetBottomPanel(UIController.BPType.Text);
			}
		}
	}
}

}