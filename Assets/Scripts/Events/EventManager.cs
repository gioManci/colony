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

	private GameObject popupPanel;

	private Dictionary<PopupStyle, GameObject> buttons = new Dictionary<PopupStyle, GameObject>();

	private EventManager() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}

	void Start() {
		popupPanel = GameObject.Find("PopupPanel");
		buttons[PopupStyle.Ok] = GameObject.Find("OkButtons");
		buttons[PopupStyle.YesNo] = GameObject.Find("YesNoButtons");
		popupPanel.SetActive(false);
	}
	
	void Update() {
		for (int i = ongoing.Count - 1; i >= 0; --i) {
			var evt = ongoing[i];
			if (evt.Tick()) {
				showPopup(evt.Happen(), PopupStyle.Ok, evt.Image);
				ongoing.RemoveAt(i);
			}
		}
	}

	public void HidePopup() {
		popupPanel.SetActive(false);
		// unpause the game
		Time.timeScale = 1f;
	}

	public void LaunchEvent(Event evt) {
		if (evt.Style == PopupStyle.YesNo)
			bindYesNo(evt.Yes, evt.No);
		if (evt.IsImmediate) {
			showPopup(evt.Happen(), evt.Style, evt.Image);
		} else {
			TimeSpan dur = TimeSpan.FromSeconds((int)evt.Timeout);
			showPopup(evt.Text + "\r\n\r\n(This will happen in: " + String.Format("{0:D2}:{1:D2}", dur.Minutes, dur.Seconds) + ")",
				evt.Style, evt.Image);
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

	private void showPopup(string text, PopupStyle style, Sprite image) {
		// pause the game
		Time.timeScale = 0f;
		foreach (var pair in buttons) {
			pair.Value.SetActive(pair.Key == style);
		}
		popupPanel.SetActive(true);
		Text t = popupPanel.GetComponentInChildren<Text>();
		Debug.Assert(t != null, "text is null!");
		t.text = text;
		popupPanel.transform.FindChild("EventImage").GetComponent<Image>().overrideSprite = image;
	}
}

}