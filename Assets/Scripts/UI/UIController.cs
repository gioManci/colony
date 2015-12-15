using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Colony.Resources;

namespace Colony.UI {

public class UIController : MonoBehaviour {

	public GameObject hiveButtonsRoot; 
	public GameObject larvaButtonsRoot;
	public GameObject queenButtonsRoot;
    public GameObject tooltipText;

	public enum ButtonType {
		None, Hive, Larva, Queen
	}

	private Text[] resourceTexts = new Text[Enum.GetNames(typeof(ResourceType)).Length];
	public ResourceManager resourceManager;

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
		// Subscribe to ResourceManager's events
		if (resourceManager == null) {
			var rm = GameObject.Find("ResourceManager");
			resourceManager = rm.GetComponent<ResourceManager>();
		}
		resourceManager.OnResourceChange += updateResources;
		updateResources();
	}

	public void SetResource(ResourceType type, int amount) {
		resourceTexts[(int)type].text = amount.ToString();
	}

	public void SetButtonsVisible(ButtonType type) {
		hiveButtonsRoot.SetActive(type == ButtonType.Hive);
		queenButtonsRoot.SetActive(type == ButtonType.Queen);
		larvaButtonsRoot.SetActive(type == ButtonType.Larva);
	}

	private void updateResources() {
		for (int i = 0; i < resourceTexts.Length; ++i) {
			if (resourceTexts[i] != null)
				resourceTexts[i].text = resourceManager.GetResource((ResourceType)i).ToString();
		}
	}
}

}
