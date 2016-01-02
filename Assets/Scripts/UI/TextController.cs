using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace Colony.UI {

public class TextController : MonoBehaviour {
	private const int QUEUE_LEN = 10;

	public GameObject TextPanel;
	public Text TextTemplate;

	private Queue<EventText> texts;

	// Make this class a singleton
	public static TextController Instance { get; private set; }

	private TextController() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}

	public void Add(EventText text) {
		if (texts.Count > QUEUE_LEN)
			texts.Dequeue();
		texts.Enqueue(text);
		updateGUI();
	}

	private void updateGUI() {
		int hOffset = 0;
		foreach (var text in texts.Reverse()) {
			var pos = TextPanel.transform.position + new Vector3(0, hOffset, 0);
			var txt = GameObject.Instantiate(TextTemplate);
			txt.text = text.Txt;
			txt.transform.parent = transform;
		}
	}
}

}