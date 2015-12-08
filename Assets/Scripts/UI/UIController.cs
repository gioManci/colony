using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Colony.Resources;

namespace Colony.UI {

public class UIController : MonoBehaviour {

	public GameObject hiveButtonsRoot; 

	private Text[] resourceTexts = new Text[Enum.GetNames(typeof(ResourceType)).Length];

	// Make this class a singleton
	public static UIController Instance { get; private set; }

	private UIController() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}

	void Start() {
		foreach (var type in Enum.GetValues(typeof(ResourceType))) {
			var obj = GameObject.Find(type.ToString() + "Text");
			if (obj != null)
				resourceTexts[(int)type] = obj.GetComponent<Text>();
		}
	}

	public void SetResource(ResourceType type, int amount) {
		resourceTexts[(int)type].text = amount.ToString();
	}

	public void SetHiveButtonsVisible(bool visible) {
		hiveButtonsRoot.SetActive(visible);
	}
}

}
