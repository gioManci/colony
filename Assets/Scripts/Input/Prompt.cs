using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;
using Colony.Resources;
using Colony.UI;
using Colony.Events;

namespace Colony.Input {

using Event = Colony.Events.Event;

public class Prompt : MonoBehaviour {

	private InputField input;

	void Start() {
		input = GetComponent<InputField>();
		Debug.Assert(input != null, "input is null!");
	}

	public bool IsActive {
		get { return gameObject.activeSelf; }
	}

	public void Show() {
		Time.timeScale = 0f;
		gameObject.SetActive(true);
		input = GetComponent<InputField>();
		EventSystem.current.SetSelectedGameObject(gameObject, null);
		input.OnPointerClick(new PointerEventData(EventSystem.current));
	}

	public void Close(bool process) {
		Time.timeScale = 1f;
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
			cheatAddResource((int)ResourceType.Honey, tokens);
			break;
		case "w":
		case "water":
			cheatAddResource((int)ResourceType.Water, tokens);
			break;
		case "rj":
		case "royaljelly":
			cheatAddResource((int)ResourceType.RoyalJelly, tokens);
			break;
		case "p":
		case "pollen":
			cheatAddResource((int)ResourceType.Pollen, tokens);
			break;
		case "n":
		case "nectar":
			cheatAddResource((int)ResourceType.Nectar, tokens);
			break;
		case "a":
		case "all":
			cheatAddResource(-1, tokens);
			break;
		case "e":
		case "event":
			cheatSpawnEvent(tokens);
			break;
		case "quit":
			Application.Quit();
			return;
		default:
			TextController.Instance.Add("Cheats:");
			TextController.Instance.Add("Resources: w|h|rj|p|a [amount]");
			TextController.Instance.Add("Events: event <event> [timeout]");
			break;
		}
	}

	private void cheatAddResource(int type, string[] tokens) {
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

	private void cheatSpawnEvent(string[] tokens) {
		if (tokens.Length < 2) {
			TextController.Instance.Add("Available events: bear, skunk, attack, rain, toxic");
			return;
		}

		float timeout = -1;
		if (tokens.Length > 2) {
			try {
				timeout = float.Parse(tokens[2]);
			} catch {}
		}

		Event evt = null;
		switch (tokens[1].ToLower()) {
		case "bear":
			evt = new BearEvent();
			break;
		case "skunk":
			evt = new SkunkEvent();
			break;
		case "attack":
			evt = new AttackEvent();
			break;
		case "rain":
			evt = new RainEvent();
			break;
		case "toxic":
		case "toxicpollen":
			evt = new ToxicPollenEvent();
			break;
		default:
			return;
		}

		if (timeout >= 0)
			evt.Timeout = timeout;

		EventManager.Instance.LaunchEvent(evt);
	}
}

}