using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace Colony.UI {

// Class controlling the UI messages
public class TextController : MonoBehaviour {
	// The maximum number of messages to display at once
	private const int QUEUE_LEN = 6;
	// Number of seconds after which messages expire
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

	// Adds a message to the queue. Messages will self-destroy after
	// TEXT_TIMEOUT seconds.
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
	}
}

}