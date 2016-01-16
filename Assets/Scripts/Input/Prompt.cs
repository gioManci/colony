using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;
using Colony.Resources;
using Colony.UI;

namespace Colony.Input {

public class Prompt : MonoBehaviour {

	private InputField input;

	void Start() {
		input = GetComponent<InputField>();
	}

	public bool IsActive {
		get { return gameObject.activeSelf; }
	}

	public void Show() {
		gameObject.SetActive(true);
		EventSystem.current.SetSelectedGameObject(gameObject, null);
		input.OnPointerClick(new PointerEventData(EventSystem.current));
	}

	public void Close(bool process) {
		gameObject.SetActive(false);
		if (process) {
			processCommand(input.text);
		}
		input.text = "";
	}

	private void processCommand(string txt) {
		if (txt.Length < 1)
			return;

		string[] tokens = txt.Split();
		int type = -1;

		switch (tokens[0].ToLower()) {
		case "h":
		case "honey":
			type = (int)ResourceType.Honey;
			break;
		case "w":
		case "water":
			type = (int)ResourceType.Water;
			break;
		case "rj":
		case "royaljelly":
			type = (int)ResourceType.RoyalJelly;
			break;
		case "p":
		case "pollen":
			type = (int)ResourceType.Pollen;
			break;
		case "n":
		case "nectar":
			type = (int)ResourceType.Nectar;
			break;
		}

		int amount = 1000;
		if (tokens.Length > 1) {
			try {
				amount = int.Parse(tokens[1]);
			} catch {}
		}

		if (type < 0) {
			UIController.Instance.resourceManager.AddResources(
				new ResourceSet()
					.With(ResourceType.Honey, amount)
					.With(ResourceType.Water, amount)
					.With(ResourceType.Pollen, amount)
					.With(ResourceType.RoyalJelly, amount)
					.With(ResourceType.Nectar, amount));
		} else {
			UIController.Instance.resourceManager.AddResource((ResourceType)type, amount);
		}
	}
}

}