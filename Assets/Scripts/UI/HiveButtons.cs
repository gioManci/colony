using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Colony.Input;
using Colony.Resources;

public class HiveButtons : MonoBehaviour {

	// Use this for initialization
	void Start() {
	
	}

	public void CreateEgg() {
		List<Cell> selectedCells = MouseActions.Instance.GetSelected<Cell>();
		foreach (Cell cell in selectedCells) {
			if (!requireResources(new ResourceSet())) {
				// UI message
				break;
			}
			cell.CreateEgg();
		}
	}

	public void UseAsStorage() {
		Debug.Log("use storage");
	}

	public void Refine() {
		Debug.Log("refine");
	}

	private bool requireResources(ResourceSet res) {
		for (int i = 0; i < Enum.GetValues(typeof(ResourceType)).Length; ++i) {
			// if resourceManager hasn't enough resources
			// return false
		}
		return true;
	}
}
