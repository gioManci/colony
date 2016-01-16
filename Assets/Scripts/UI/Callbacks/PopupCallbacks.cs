using UnityEngine;
using System.Collections;
using Colony.Events;

namespace Colony.UI {

public class PopupCallbacks : MonoBehaviour {

	public void HidePopup() {
		EventManager.Instance.HidePopup();
	}
}

}
