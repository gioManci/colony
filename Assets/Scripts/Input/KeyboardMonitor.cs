using UnityEngine;
using System.Collections;
using Colony.UI;

namespace Colony.Input {

using Input = UnityEngine.Input;

public class KeyboardMonitor : MonoBehaviour {

	public GameObject Menu;

	public bool IsPromptActive { get; private set; }

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (IsPromptActive) {
				UIController.Instance.MsgPrompt.Close(false);
				IsPromptActive = !IsPromptActive;
			} else {
				Menu.SetActive(!Menu.activeSelf);
				Time.timeScale = Menu.activeSelf ? 0f : 1f;
			}
		} else if (Input.GetKeyDown(KeyCode.Return)) {
			if (IsPromptActive) {
				UIController.Instance.MsgPrompt.Close(true);
			} else {
				UIController.Instance.MsgPrompt.Show();
			}
			IsPromptActive = !IsPromptActive;
		} else if (Input.GetKeyDown(KeyCode.Space)) {
			// Center camera on selected unit
			var sel = MouseActions.Instance.GetSelected<Controllable>();
			if (sel.Count > 0)
				Camera.main.transform.position = sel[0].transform.position - new Vector3(0, 0, 1);
		}
	}
}

}