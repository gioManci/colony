using UnityEngine;
using System.Collections.Generic;
using InputEvents;

public class MouseActions : MonoBehaviour {

	private HashSet<Selectable> selectables = new HashSet<Selectable>();
	private HashSet<Moveable> moveables = new HashSet<Moveable>();

	// Make this class a singleton
	public static MouseActions Instance { get; private set; }
	
	private MouseActions() {}
	
	void Awake() {
		if (Instance == null) {
			Instance = this;
			MouseMonitor.OnLeftClick += clickSelect;
			MouseMonitor.OnDrag += dragSelect;
			MouseMonitor.OnRightClick += moveSelectedUnits;
		} else {
			GameObject.Destroy(this);
		}
	}

	public void AddSelectable(Selectable obj) {
		selectables.Add(obj);
	}

	public void RemoveSelectable(Selectable obj) {
		selectables.Remove(obj);
	}

	public void AddMoveable(Moveable obj) {
		moveables.Add(obj);
	}

	public void RemoveMoveable(Moveable obj) {
		moveables.Remove(obj);
	}

	private void clickSelect(Click click) {
		Debug.Log("click select");

		if (!Input.GetKey(KeyCode.LeftShift))
			deselectAll();
		
		// Find out if some Selectable was hit by click
		RaycastHit2D hit = Physics2D.Raycast(click.Pos, Vector2.zero);
		Selectable sel = hit.collider?.gameObject?.GetComponentInChildren<Selectable>();
		// If so, select it
		if (sel != null)
			sel.SelectToggle();
	}
	
	private void dragSelect(Drag drag) {
		Debug.Log("drag select (rect = " + drag.SpanRect + ")");

		if (!Input.GetKey(KeyCode.LeftShift))
			deselectAll();

		foreach (Selectable obj in selectables) {
			var go = obj.gameObject;
			Debug.Log("object in position " +go.transform.position);
			if (drag.SpanRect.Contains(go.transform.position))
				obj.Select();
		}
	}

	private void moveSelectedUnits(Click click) {
		// Move each selected moveable object
		foreach (Moveable obj in moveables) {
			Selectable sel = obj.gameObject.GetComponentInChildren<Selectable>();
			if (sel != null && sel.IsSelected)
				obj.Move(click.Pos);
		}
	}

	private void deselectAll() {
		foreach (Selectable s in selectables)
			s.Deselect();
	}
}
