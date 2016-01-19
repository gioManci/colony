using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Colony.Resources;
using Colony.Input;

namespace Colony.UI {

public class UIController : MonoBehaviour {

	private GameObject hiveButtonsRoot; 
	private GameObject refineButtons;
	private GameObject stopRefineButtons;
	private GameObject larvaButtonsRoot;
	private GameObject queenButtonsRoot;
	private GameObject specButtonsRoot;
	private GameObject bpText;

	public GameObject TooltipPanel;
	public Prompt MsgPrompt { get; private set; }

	public enum BPType {
		None  = 1 << 1,
		Hive  = 1 << 2,
		Larva = 1 << 3,
		Queen = 1 << 4,
		Text  = 1 << 5,
		Spec  = 1 << 6
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
		// Find ALL the buttons!
		refineButtons = GameObject.Find("RefineButtons");
		refineButtons.SetActive(false);
		stopRefineButtons = GameObject.Find("StopRefineButtons");
		stopRefineButtons.SetActive(false);	
		hiveButtonsRoot = GameObject.Find("HiveButtons");
		hiveButtonsRoot.SetActive(false);
		queenButtonsRoot = GameObject.Find("QueenButtons");
		queenButtonsRoot.SetActive(false);
		larvaButtonsRoot = GameObject.Find("LarvaButtons");
		larvaButtonsRoot.SetActive(false);
		bpText = GameObject.Find("BPText");
		bpText.SetActive(false);
		TooltipPanel = GameObject.Find("TooltipPanel");
		specButtonsRoot = GameObject.Find("SpecializationButtons");
		specButtonsRoot.SetActive(false);
		MsgPrompt = GameObject.Find("MessagePrompt").GetComponent<Prompt>();
		MsgPrompt.gameObject.SetActive(false);
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
		hiveButtonsRoot.SetActive((type & BPType.Hive) != 0);
		queenButtonsRoot.SetActive((type & BPType.Queen) != 0);
		larvaButtonsRoot.SetActive((type & BPType.Larva) != 0);
		bpText.SetActive((type & BPType.Text) != 0);
		specButtonsRoot.SetActive((type & BPType.Spec) != 0);
	}

	public void SetResourceBPText(ResourceYielder res) {
		string txt = "Resources left: (yield)\r\n";
		foreach (ResourceType type in Enum.GetValues(typeof(ResourceType))) {
			// The following line is painful to watch; should've been a
			// !type.IsRaw, but we haven't implemented Nectar yet, so 
			// this is a quick way to discard it too.
			if (type == ResourceType.Nectar)
				break;
			txt += "\r\n" + String.Format("{0,-9} {1,-5:D} ({2:D})", type.ToString() + ":",
				res.Resources[type], res.YieldAmount(type));
		}
		SetBPText(txt);
	}

	public void SetBeeLoadText(GameObject bee) {
		var spec = bee.GetComponent<Stats>().Specialization;
		string txt = (spec == Specializations.SpecializationType.None ? "Worker" : spec.ToString())
			+ " Bee\r\n\r\nLoad:";
		var load = bee.GetComponent<BeeLoad>();
		foreach (ResourceType type in Enum.GetValues(typeof(ResourceType))) {
			// See comment in SetResourceBPText
			if (type == ResourceType.Nectar)
				break;
			txt += "\r\n" + type.ToString() + ": " + load.Load[type];
		}
		SetBPText(txt);
	}

	public void SetBPRefining(Cell.RefinedResource resource) {
		bool refining = resource != Cell.RefinedResource.None;
		var what = refining ? BPType.Hive | BPType.Text : BPType.Hive;
		if (refining) {
			// /!\ Horrible workaround follows
			if ((int)resource > 1 << Enum.GetNames(typeof(Cell.RefinedResource)).Length)
				SetBPText("Refining both");
			else
				SetBPText("Refining " + resource);
		}
		SetBottomPanel(what);
		refineButtons.SetActive(!refining);
		stopRefineButtons.SetActive(refining);
	}

	public void SetBPText(string text) {
		var txt = bpText.GetComponentInChildren<Text>();
		Debug.Assert(txt != null, "null");
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
