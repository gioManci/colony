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
	public GameObject bpText;
    	public GameObject tooltipText;

	public enum BPType {
		None, Hive, Larva, Queen, Text
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

	public void SetBottomPanel(BPType type) {
		hiveButtonsRoot.SetActive(type == BPType.Hive);
		queenButtonsRoot.SetActive(type == BPType.Queen);
		larvaButtonsRoot.SetActive(type == BPType.Larva);
		bpText.SetActive(type == BPType.Text);
	}

	public void SetResourceBPText(ResourceYielder res) {
		string txt = "Resources left:";
		foreach (ResourceType type in Enum.GetValues(typeof(ResourceType))) {
			txt += "\r\n" + type.ToString() + ": " + res.Resources[type];
		}
		setBPText(txt);
	}

	public void SetBeeLoadText(BeeLoad load) {
		string txt = "Load:";
		foreach (ResourceType type in Enum.GetValues(typeof(ResourceType))) {
			txt += "\r\n" + type.ToString() + ": " + load.Load[type];
		}
		setBPText(txt);
	}

	public void SetBPRefining(bool refining) {
		SetBottomPanel(BPType.Hive);
		GameObject.Find("RefineButton").GetComponentInChildren<Text>().text =
			refining ? "Stop refining" : "Refine";
	}

	private void setBPText(string text) {
		var txt = bpText.GetComponentInChildren<Text>();
		txt.text = text;
	}

	private void updateResources() {
		for (int i = 0; i < resourceTexts.Length; ++i) {
			if (resourceTexts[i] != null)
				resourceTexts[i].text = resourceManager.GetResource((ResourceType)i).ToString();
		}
	}
}

}
