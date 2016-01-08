using UnityEngine;
using System.Collections;

public class KeyboardMonitor : MonoBehaviour {

	public GameObject Menu;

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Menu.SetActive(!Menu.activeSelf);
			Time.timeScale = Menu.activeSelf ? 0f : 1f;
		}
	}
}
