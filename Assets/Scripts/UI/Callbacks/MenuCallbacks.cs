using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuCallbacks : MonoBehaviour {

	public void ReturnToGame() {
		gameObject.SetActive(false);
		Time.timeScale = 1f;
	}

	public void ExitGame() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("menu");
    }
}
