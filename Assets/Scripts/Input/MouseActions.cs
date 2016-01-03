using UnityEngine;
using System.Collections.Generic;
using Colony.Tasks;
using Colony.Resources;
using Colony;
using Colony.UI;

namespace Colony.Input {
using Cursor = Colony.UI.Cursor;
using Input = UnityEngine.Input;

// Implements all the actions performed when an event is caught
// by MouseMonitor.
public class MouseActions : MonoBehaviour {
	private HashSet<Selectable> selected = new HashSet<Selectable>();

	private GameObject moveTarget;

	// Make this class a singleton
	public static MouseActions Instance { get; private set; }

	private MouseActions() {
	}

	void Awake() {
		if (Instance == null) {
			Instance = this;
			MouseMonitor.OnLeftClick += clickSelect;
			MouseMonitor.OnDrag += dragSelect;
			MouseMonitor.OnRightClick += dispatchRightClick;
			MouseMonitor.OnMove += changeCursor;
		} else {
			GameObject.Destroy(this);
		}
	}

	public void RemoveSelected(Selectable sel) {
		selected.Remove(sel);
	}

	public List<T> GetSelected<T>() {
		List<T> list = new List<T>();
		foreach (Selectable s in selected) {
			var c = s.gameObject.GetComponentInChildren<T>();
			if (c != null)
				list.Add(c);
		}
		return list;
	}

	/************ Private ************/

	private void updateSelected(Selectable sel) {
		if (sel.IsSelected)
			selected.Add(sel);
		else
			selected.Remove(sel);
	}

	private void clickSelect(Click click) {
		if (!Input.GetKey(KeyCode.LeftShift))
			deselectAll();

		// Find out if some Selectable was hit by click
		var obj = Utils.GetObjectAt(click.pos);
		if (obj != null) {
			Selectable sel = obj.GetComponentInChildren<Selectable>();
			// If so, select it
			if (sel != null) {
				sel.SelectToggle();
				updateSelected(sel);
			}
		}
	}

	private void dragSelect(Drag drag) {
		if (!Input.GetKey(KeyCode.LeftShift))
			deselectAll();

		foreach (Selectable obj in EntityManager.Instance.GetSelectablesIn(drag.spanRect)) {
			if (!obj.dragSelectable)
				continue;

			var go = obj.gameObject;
			if (!go.GetComponent<Renderer>().isVisible)
				continue;

			if (drag.spanRect.Contains(go.transform.position)) {
				obj.Select();
				updateSelected(obj);
			}
		}
	}

	private void dispatchRightClick(Click click) {
		if (selected.Count > 0) {
			// Check if click was on an interactable unit (resource, etc)
			var obj = Utils.GetObjectAt(click.pos);
			if (obj != null) {
				if ("Flower".Equals(obj.tag)) {
					foreach (var bee in GetSelected<Controllable>()) {
						if (bee.canHarvest) {
							bee.DoHarvest(obj);
						}
					}
				}
				if ("Cell".Equals(obj.tag)) {
					foreach (var bee in GetSelected<Controllable>()) {
						if (bee.canBreed && UIController.Instance.resourceManager.RequireResources(Costs.Larva)) {
							bee.DoBreed(obj);
						} else if (bee.canMove) {
							bee.DoMove(click.pos);
						}
					}
				}
				if (EntityManager.Instance.IsEnemy(obj)) {
					foreach (var bee in GetSelected<Controllable>()) {
						if (bee.canAttack) {
							bee.DoAttack(obj);
						}
					}
				}
				//moveSelectedUnits(click.pos);
			} else {
				moveSelectedUnits(click.pos);
			}
		} else {
			moveSelectedUnits(click.pos);
		}
	}

	private void moveSelectedUnits(Vector2 pos) {
		// Draw a point on move target
		if (moveTarget != null)
			GameObject.Destroy(moveTarget);
		moveTarget = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		moveTarget.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
		moveTarget.transform.position = pos;

		// Move each selected moveable object
		foreach (Controllable obj in GetSelected<Controllable>()) {
			if (obj.canMove) {
				obj.DoMove(pos);
			}
		}
	}

	private void deselectAll() {
		foreach (Selectable s in selected) {
			s.Deselect();
		}
		selected.Clear();
	}

	private void changeCursor(Move move) {
		uint CanHarvest = 1, 
		     CanAttack = 1 << 1,
		     CanBreed = 1 << 2;
		uint flags = 0;
		foreach (Controllable bee in GetSelected<Controllable>()) {
			if (bee.canHarvest)
				flags |= CanHarvest;
			if (bee.canAttack)
				flags |= CanAttack;
			if (bee.canBreed)
				flags |= CanBreed;
					
			if (flags != 0)
				break;
		}

		if (flags != 0) {
			var obj = Utils.GetObjectAt(move.pos);
			if (obj != null) {
				if (((flags & CanHarvest) != 0 && obj.GetComponentInChildren<ResourceYielder>() != null)
				      || ((flags & CanBreed) != 0 && obj.GetComponent<Cell>() != null)) {
					Cursor.Instance.SetCursor(Cursor.Type.Click); 
					return;
				} else if ((flags & CanAttack) != 0 && EntityManager.Instance.IsEnemy(obj)) {
					Cursor.Instance.SetCursor(Cursor.Type.Attack);	
					return;
				}
			}
		} 
		if (Cursor.Instance.CursType != Cursor.Type.Normal)
			Cursor.Instance.SetCursor(Cursor.Type.Normal); 
	}
}
}
