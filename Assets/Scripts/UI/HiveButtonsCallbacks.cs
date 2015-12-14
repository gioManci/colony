using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Colony;
using Colony.Input;
using Colony.Resources;
using Colony.Hive;

public class HiveButtonsCallbacks : MonoBehaviour {

	private ResourceManager rm;

	// Use this for initialization
	void Start() {
		rm = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();	
	}

	public void CreateEgg() {
		foreach (var hiveObj in EntityManager.Instance.Beehives) {
			Hive hive = hiveObj.GetComponent<Hive>();
			foreach (var cellObj in hive.Cells) {
				Cell cell = cellObj.GetComponent<Cell>();
				if (!rm.RequireResources(new ResourceSet())) {
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
}
