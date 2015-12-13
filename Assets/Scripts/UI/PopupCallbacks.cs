using UnityEngine;
using System.Collections;

namespace Colony.UI {

public class PopupCallbacks : MonoBehaviour {

	public void HidePopup() {
		GameObject.Find("PopupPanel").SetActive(false);
		// unpause the game
		Time.timeScale = 1f;
	}
}

}
