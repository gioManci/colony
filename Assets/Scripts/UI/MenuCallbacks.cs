using UnityEngine;
using System.Collections;

public class MenuCallbacks : MonoBehaviour {

	public void ReturnToGame() {
		gameObject.SetActive(false);
	}
}
