using UnityEngine;
using System.Collections;
using Colony.Events;

namespace Colony.UI {

public class PopupCallbacks : MonoBehaviour {

	public void HidePopup() {
		EventManager.Instance.PopupPanel.SetActive(false);
		// unpause the game
		Time.timeScale = 1f;
	}
}

}
