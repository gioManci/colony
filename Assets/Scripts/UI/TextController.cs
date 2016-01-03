using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace Colony.UI {

public class TextController : MonoBehaviour {
	private const int QUEUE_LEN = 6;
	private const float TEXT_TIMEOUT = 10f;

	public GameObject TextPanel;
	public Text TextTemplate;

	private Queue<Text> texts = new Queue<Text>();

	// Make this class a singleton
	public static TextController Instance { get; private set; }

	private TextController() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}

	public void Add(string msg) {
		if (texts.Count == QUEUE_LEN) {
			var t = texts.Dequeue();
			if (t != null)
				GameObject.Destroy(t.gameObject);
		}
		var text = GameObject.Instantiate(TextTemplate);
		text.text = msg;
		text.transform.SetParent(TextPanel.transform, false);
		texts.Enqueue(text);
		GameObject.Destroy(text.gameObject, TEXT_TIMEOUT);
		updateGUI();
	}

	private void updateGUI() {
		
//		foreach (var text in texts.Reverse()) {
//			var pos = TextPanel.transform.position + new Vector3(0, hOffset, 0);
//			var txt = GameObject.Instantiate(TextTemplate);
//			txt.text = text.Txt;
//			txt.transform.SetParent(TextPanel.transform);
//		}
	}
}

}