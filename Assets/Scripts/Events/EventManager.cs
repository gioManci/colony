using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Colony.Events {

public class EventManager : MonoBehaviour {
	private List<Event> ongoing = new List<Event>();

	// Make this class a singleton
	public static EventManager Instance { get; private set; }

	public GameObject PopupPanel;

	private EventManager() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}
	
	void Update() {
		for (int i = ongoing.Count - 1; i >= 0; --i) {
			var evt = ongoing[i];
			if (evt.Tick()) {
				showPopup(evt.Happen());
				ongoing.RemoveAt(i);
			}
		}
	}

	public void LaunchEvent(Event evt) {
		showPopup(evt.Text + "This will happen in: " + toReadable((int)evt.Timeout));
		ongoing.Add(evt);
	}

	private void showPopup(string text) {
		// pause the game
		Time.timeScale = 0f;
		PopupPanel.SetActive(true);
		Text t = PopupPanel.GetComponentInChildren<Text>();
		Debug.Assert(t != null, "text is null!");
		t.text = text;
	}

	private string toReadable(int f) {
		string r = "";
		int i = f / 60;
		r += i + ":";
		f -= i * 60;
		if (f > 9)
			r += f.ToString();
		else
			r += "0" + f.ToString();
		r += " m";
		return r;
	}
}

}
