using System;
using UnityEngine;
using System.Collections.Generic;
using Colony.Resources;
using Colony.UI;

namespace Colony.Input {

using Cursor = Colony.UI.Cursor;
using Input = UnityEngine.Input;

/// <summary>
/// Implements all the actions performed when an event is caught
/// by MouseMonitor.
/// </summary>
public class MouseActions : MonoBehaviour {
	private HashSet<Selectable> selected = new HashSet<Selectable>();

	public GameObject MoveTarget;

	public event Action<GameObject> ObjectSelected;

	// Make this class a singleton
	public static MouseActions Instance { get; private set; }

	private MouseActions() {
	}

	void Awake() {
		if (Instance == null) {
			Instance = this;
			
		} else {
			GameObject.Destroy(this);
		}
	}

        void Start()
        {
            MouseMonitor.Instance.OnLeftClick += clickSelect;
            MouseMonitor.Instance.OnDrag += dragSelect;
            MouseMonitor.Instance.OnRightClick += dispatchRightClick;
            MouseMonitor.Instance.OnMove += changeCursor;
        }

	public void RemoveSelected(Selectable sel) {
		selected.Remove(sel);
	}

	/// <summary>
	/// Returns all the selected Selectables for which <code>GetComponentInChildren&lt;T&gt;() is non-null</code>
	/// </summary>
	public List<T> GetSelected<T>() {
		List<T> list = new List<T>();
		foreach (Selectable s in selected) {
			var c = s.gameObject.GetComponentInChildren<T>();
			if (c != null)
				list.Add(c);
		}
		return list;
	}

	public void DeselectAll() {
		foreach (Selectable s in selected) {
			s.Deselect();
		}
		selected.Clear();
	}

	/// <summary>
	/// If <code>sel</code> is selected, adds it to <code>selected</code>, else
	/// removes it if necessary.
	/// </summary>
	public void UpdateSelected(Selectable sel) {
		if (sel.IsSelected)
			selected.Add(sel);
		else
			selected.Remove(sel);
	}

	/************ Private ************/

	private void clickSelect(Click click) {
		if (!Input.GetKey(KeyCode.LeftShift))
			DeselectAll();

		// Find out if some Selectable was hit by click
		GameObject obj = Utils.GetObjectAt(click.pos);
		if (obj != null) {
			Selectable sel = obj.GetComponentInChildren<Selectable>();
			// If so, select it
			if (sel != null) {
				sel.SelectToggle();
				UpdateSelected(sel);
				if (ObjectSelected != null) {
					ObjectSelected(obj);
				}
			}
		}
	}

	private void dragSelect(Drag drag) {
		if (!Input.GetKey(KeyCode.LeftShift))
			DeselectAll();

		bool selectedBee = false;
		foreach (Selectable obj in EntityManager.Instance.GetSelectablesIn(drag.spanRect)) {
			if (obj.DragSelectable) {
				var contr = obj.GetComponent<Controllable>();
				if (selectedBee && contr == null)
					continue;
				obj.Select();
				UpdateSelected(obj);
				if (ObjectSelected != null) {
					ObjectSelected(obj.gameObject);
				}
				if (contr != null)
					selectedBee = true;
			}
		}
	}

	/// <summary>
	/// Figures out the action to perform accordingly to the currently selected
	/// units and the clicked object, if any.
	/// </summary>
	/// <param name="click">A struct containing the click parameters</param>
	private void dispatchRightClick(Click click) {
		if (selected.Count > 0) {
			// Check if click was on an interactable unit (resource, etc)
			GameObject obj = Utils.GetObjectAt(click.pos);
			if (obj != null) {
				switch (obj.tag) {
				case "Flower":
					foreach (var bee in GetSelected<Controllable>()) {
						if (bee.canHarvest) {
							bee.DoHarvest(obj);
						}
					}
					break;
				case "Cell":
					{
						var cell = obj.GetComponent<Cell>();
						foreach (var bee in GetSelected<Controllable>()) {
							if (bee.canBreed && cell.CellState == Cell.State.Storage) {
								if (UIController.Instance.resourceManager.RequireResources(Costs.Larva))
									bee.DoBreed(obj);
								else
									TextController.Instance.Add("Not enough resources to breed!");
							} else if (bee.canInkeep && cell.CellState == Cell.State.Refine) {
								bee.DoInkeep(obj);
							} else if (bee.canMove) {
								bee.DoMove(click.pos);
							} 
						}
						break;
					}
				default:
					if (EntityManager.Instance.IsEnemy(obj)) {
						foreach (var bee in GetSelected<Controllable>()) {
							if (bee.canAttack) {
								bee.DoAttack(obj);
							}
						}
					} else {
						moveSelectedUnits(click.pos);
					}
					break;
				}
			} else {
				moveSelectedUnits(click.pos);
			}
		} else {
			moveSelectedUnits(click.pos);
		}
	}

	private void moveSelectedUnits(Vector2 pos) {
		// Draw a point on move target
		if (MoveTarget != null) {
			MoveTarget.transform.position = pos;
			MoveTarget.GetComponent<Animator>().SetTrigger("start");
		}

		// Move each selected moveable object
		foreach (Controllable obj in GetSelected<Controllable>()) {
			if (obj.canMove) {
				obj.DoMove(pos);
			}
		}
	}

	/// <summary>
	/// Updates the cursor sprite according to the object the cursor is over, if any.
	/// </summary>
	/// <param name="move">A struct containing the mouse motion parameters</param>
	private void changeCursor(Move move) {
		uint canHarvest = 1,
		     canAttack  = 1 << 1,
		     canBreed   = 1 << 2,
		     canInkeep  = 1 << 3;
		uint flags = 0;
		foreach (Controllable bee in GetSelected<Controllable>()) {
			if (bee.canHarvest)
				flags |= canHarvest;
			if (bee.canAttack)
				flags |= canAttack;
			if (bee.canBreed)
				flags |= canBreed;
			if (bee.canInkeep)
				flags |= canInkeep;

			if (~flags == 0)
				break;
		}

		if (flags != 0) {
			GameObject obj = Utils.GetObjectAt(move.pos);
			if (obj != null) {
				var cell = obj.GetComponent<Cell>();
				bool harvestable = (flags & canHarvest) != 0 && obj.GetComponentInChildren<ResourceYielder>() != null,
				     breedable   = (flags & canBreed)  != 0 && cell != null && cell.CellState == Cell.State.Storage,
				     inkeepable  = (flags & canInkeep) != 0 && cell != null && cell.CellState == Cell.State.Refine,
				     attackable  = (flags & canAttack) != 0 && EntityManager.Instance.IsEnemy(obj);

				if (harvestable || breedable || inkeepable) {
					Cursor.Instance.SetCursor(Cursor.Type.Click);
					return;
				} else if (attackable) {
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