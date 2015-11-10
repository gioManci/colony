using UnityEngine;
using System.Collections;

public class MouseMonitor : MonoBehaviour {
	enum MouseBtn : int { Left = 0, Right = 1, Middle = 2 };

	void Update() {
		if (Input.GetMouseButtonDown((int)MouseBtn.Left)) {
			Debug.Log("deselect");
			BroadcastMessage("Deselect");
		} else if (Input.GetMouseButtonDown((int)MouseBtn.Right)) {
			Debug.Log("Move");
			// Move each selected moveable object
			foreach (Moveable obj in GetComponentsInChildren<Moveable>()) {
				Selectable sel = obj.gameObject.GetComponentInChildren<Selectable>();
				if (sel != null && sel.IsSelected)
					obj.Move(transform.parent.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition));
			}
		}
	}
}