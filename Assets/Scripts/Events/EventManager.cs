using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Colony.Events {

public class EventManager : MonoBehaviour {

	public enum PopupStyle {
		Ok,
		YesNo
	}

	private List<Event> ongoing = new List<Event>();

	// Make this class a singleton
	public static EventManager Instance { get; private set; }

	public GameObject PopupPanel;

	private Dictionary<PopupStyle, GameObject> buttons = new Dictionary<PopupStyle, GameObject>();

	private EventManager() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}

	void Start() {
		buttons[PopupStyle.Ok] = PopupPanel.transform.FindChild("OkButtons").gameObject;
		buttons[PopupStyle.YesNo] = PopupPanel.transform.FindChild("YesNoButtons").gameObject;
	}
	
	void Update() {
		for (int i = ongoing.Count - 1; i >= 0; --i) {
			var evt = ongoing[i];
			if (evt.Tick()) {
				showPopup(evt.Happen(), PopupStyle.Ok);
				ongoing.RemoveAt(i);
			}
		}
	}

	public void HidePopup() {
		PopupPanel.SetActive(false);
		// unpause the game
		Time.timeScale = 1f;
	}

	public void LaunchEvent(Event evt) {
		if (evt.Style == PopupStyle.YesNo)
			bindYesNo(evt.Yes, evt.No);
		if (evt.IsImmediate) {
			showPopup(evt.Happen(), evt.Style);
		} else {
			showPopup(evt.Text + "\r\n\r\n(This will happen in: " + toReadable((int)evt.Timeout) + ")", evt.Style);
			if (!evt.Canceled)
				ongoing.Add(evt);
		}
	}

	private void bindYesNo(UnityAction yes, UnityAction no) {
		foreach (Transform child in buttons[PopupStyle.YesNo].transform) {
			if (child.gameObject.name == "YesButton") {
				var btn = child.gameObject.GetComponent<Button>();
				btn.onClick.RemoveAllListeners(); 
				btn.onClick.AddListener(yes);
			} else if (child.gameObject.name == "NoButton") {
				var btn = child.gameObject.GetComponent<Button>();
				btn.onClick.RemoveAllListeners(); 
				btn.onClick.AddListener(no);
			}
		}
	}

	private void showPopup(string text, PopupStyle style) {
		// pause the game
		Time.timeScale = 0f;
		foreach (var pair in buttons) {
			pair.Value.SetActive(pair.Key == style);
		}
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