using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Colony;
using Colony.Input;
using Colony.Resources;
using Colony.Hive;

namespace Colony.UI {
using RefinedResource = Cell.RefinedResource;

public class HiveButtonsCallbacks : MonoBehaviour {

//	private ResourceManager rm;

	// Use this for initialization
	void Start() {
//		rm = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();	
	}

	public void RefineHoney() {
		refine(RefinedResource.Honey);
	}

	public void RefineRoyalJelly() {
		refine(RefinedResource.RoyalJelly);
	}

	public void RefineBoth() {
		refine(RefinedResource.Honey | RefinedResource.RoyalJelly);
	}

	public void StopRefining() {
		refine(RefinedResource.None);
	}
		
	private void refine(RefinedResource what) {
		foreach (Cell cell in MouseActions.Instance.GetSelected<Cell>()) {
			switch (cell.CellState) {
			case Cell.State.CreateEgg:
				TextController.Instance.Add("Cannot use cell for refining while larva is in!");
				break;
			default:
				cell.Refine(what);
				UIController.Instance.SetBPRefining(what != RefinedResource.None);
				break;
//			case Cell.State.Refine:
//				cell.UseAsStorage();
//				UIController.Instance.SetBPRefining(false);
//				break;
//			case Cell.State.Storage:
//				cell.Refine();
//				UIController.Instance.SetBPRefining(true);
//				break;
			}
		}

	}
}

}