using UnityEngine;
using System.Collections;

public class MenuCallbacks : MonoBehaviour {

	public void ReturnToGame() {
		gameObject.SetActive(false);
		Time.timeScale = 1f;
	}

	public void ExitGame() {
		Application.Quit();
	}
}
