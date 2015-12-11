using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Colony.Events {

public class EventManager : MonoBehaviour {
	// Make this class a singleton
	public static EventManager Instance { get; private set; }

	private GameObject popupPanel;

	private EventManager() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}

	void Start() {
		popupPanel = GameObject.Find("PopupPanel");
	}

	public void LaunchEvent(Event evt) {
		showPopup(evt.Text);
	}

	private void showPopup(string text) {
		popupPanel.GetComponent<Text>().text = text;
		popupPanel.SetActive(true);
	}
}

}
