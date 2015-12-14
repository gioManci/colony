using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Colony;
using Colony.Input;
using Colony.Resources;
using Colony.Hive;

public class HiveButtons : MonoBehaviour {

	// Use this for initialization
	void Start() {
	
	}

	public void CreateEgg() {
		foreach (var hiveObj in EntityManager.Instance.Beehives) {
			Hive hive = hiveObj.GetComponent<Hive>();
			foreach (var cellObj in hive.Cells) {
				Cell cell = cellObj.GetComponent<Cell>();
				if (!requireResources(new ResourceSet())) {
					// UI message
					break;
				}
				cell.CreateEgg();
			}
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
