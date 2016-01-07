using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Colony;
using Colony.Input;
using Colony.Resources;
using Colony.Hive;
using Colony.UI;

public class HiveButtonsCallbacks : MonoBehaviour {

//	private ResourceManager rm;

	// Use this for initialization
	void Start() {
//		rm = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();	
	}

	public void Refine() {
		foreach (Cell cell in MouseActions.Instance.GetSelected<Cell>()) {
			switch (cell.CellState) {
			case Cell.State.CreateEgg:
				TextController.Instance.Add("Cannot use cell for refining while larva is in!");
				break;
			case Cell.State.Refine:
				cell.UseAsStorage();
				UIController.Instance.SetBPRefining(false);
				break;
			case Cell.State.Storage:
				cell.Refine();
				UIController.Instance.SetBPRefining(true);
				break;
			}
		}
	}
}
