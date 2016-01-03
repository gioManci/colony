using UnityEngine;
using System.Collections;
using System.Linq;
using Colony.Input;
using Colony.Resources;

namespace Colony.UI {

public class QueenButtonsCallbacks : MonoBehaviour {
	public void Colonize() {
		var queens = MouseActions.Instance.GetSelected<Controllable>().Where(x => x.gameObject.tag == "QueenBee");
		foreach (var queen in queens) {
			if (!UIController.Instance.resourceManager.RequireResources(Costs.NewHive)) {
				TextController.Instance.Add("Not enough resources to create hive!");
				return;
			}
          		UIController.Instance.resourceManager.RemoveResources(Costs.NewHive);
			queen.DoColonize();
		}
	}
}

}
