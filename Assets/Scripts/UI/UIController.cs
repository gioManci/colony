using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Colony.Resources;

namespace Colony.UI {

public class UIController : MonoBehaviour {

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
		SetResource(ResourceType.Nectar, 100);
	}

	public void SetResource(ResourceType type, int amount) {
		if (resourceTexts[(int)type] != null) {
			resourceTexts[(int)type].text = amount.ToString();
		} else {
			var txt = GameObject.Find(type.ToString() + "Text").GetComponent<Text>();
			if (txt == null)
				throw new Exception("Called SetResource with unknown type #" + (int)type);

			txt.text = amount.ToString();
			resourceTexts[(int)type] = txt;
			Debug.Log("set " + type + " to " + amount);
		}
	}
}

}
