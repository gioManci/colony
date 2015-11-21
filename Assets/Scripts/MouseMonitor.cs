using UnityEngine;
using System.Collections;

public class MouseMonitor : MonoBehaviour {
	enum MouseBtn : int { Left = 0, Right = 1, Middle = 2 };

	void Update() {
		if (Input.GetMouseButtonDown((int)MouseBtn.Left)) {
			BroadcastMessage("Deselect");
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(
				Input.mousePosition), Vector2.zero);
			Selectable sel = hit.collider?.gameObject?.GetComponent<Selectable>();
			if (sel != null) {
				sel.Select();
			}
		} else if (Input.GetMouseButtonDown((int)MouseBtn.Right)) {
			Debug.Log("Move");
			// Move each selected moveable object
			foreach (Moveable obj in GetComponentsInChildren<Moveable>()) {
				Selectable sel = obj.gameObject.GetComponentInChildren<Selectable>();
                if (sel != null && sel.IsSelected)
                {
                    WorkerBeeBrain brain = obj.GetComponent<WorkerBeeBrain>();
                    brain.DoMove(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    //obj.Move();
                }
			}
		}
	}
}