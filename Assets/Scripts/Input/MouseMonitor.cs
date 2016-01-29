using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

namespace Colony.Input {

using Input = UnityEngine.Input;

public class MouseMonitor : MonoBehaviour {

	public Texture2D selectTexture;
	public static Rect selection = new Rect();

	/// <summary>
	/// Threshold above which the mouse input is considered
	/// a "drag" rather than a "click"
	/// </summary>
	private const float MinDragSpan = 2f;

	private bool dragging = false;
	private Vector2 dragStartPos;

	private Minimap minimap;

	// Callbacks
	public  event Action<Click> OnLeftClick, OnRightClick;
	public  event Action<Drag> OnDrag, OnDragEnd;
	public  event Action<Move> OnMove; 

	// Make this class a singleton
	public static MouseMonitor Instance { get; private set; }

	private MouseMonitor() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}

	void Start() {
		minimap = GameObject.FindObjectOfType<Minimap>();
	}

	void Update() {
		var eventSystem = EventSystem.current;
		bool isGUIClick = eventSystem.IsPointerOverGameObject()
		                 && eventSystem.currentSelectedGameObject != null;
		
		if (Input.GetMouseButtonDown(0) && !minimap.HasMouseOver) {
			dragging = true;
			dragStartPos = (Vector2)Input.mousePosition;

		} else if (Input.GetMouseButtonUp(0)) {
			dragging = false;
			var dragEndPos = (Vector2)Input.mousePosition;
			var dragSpan = (dragEndPos - dragStartPos).magnitude;

			if (selection.width < 0) {
				selection.x += selection.width;
				selection.width = -selection.width;
			}
			if (selection.height < 0) {
				selection.y += selection.height;
				selection.height = -selection.height;
			}

			if (dragSpan < MinDragSpan) {
				if (!isGUIClick && !minimap.HasMouseOver && OnLeftClick != null) {
					OnLeftClick(new Click(
						Camera.main.ScreenToWorldPoint(dragStartPos)));
				}
			} else if (OnDragEnd != null) {
				OnDragEnd(new Drag(
					Camera.main.ScreenToWorldPoint(dragStartPos),
					Camera.main.ScreenToWorldPoint(dragEndPos)));
			}
		} else if (Input.GetMouseButtonDown(1)) {
			if (OnRightClick != null)
				OnRightClick(new Click(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
		}

		if (dragging) {
			var spos = ScreenToRectPoint(dragStartPos);
			var epos = ScreenToRectPoint(Input.mousePosition);
			selection = new Rect(spos.x, spos.y, epos.x - spos.x, epos.y - spos.y);
			if (OnDrag != null && !isGUIClick) {
				OnDrag(new Drag(
					Camera.main.ScreenToWorldPoint(dragStartPos),
					Camera.main.ScreenToWorldPoint(Input.mousePosition)));
			}
		} else {
			if (OnMove != null)
				OnMove(new Move(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
		}
	}

	void OnGUI() {
		// Draw the selection rectangle
		if (dragging) {
			GUI.color = new Color(1, 1, 1, 0.5f);
			GUI.DrawTexture(selection, selectTexture);
		}
	}

	public static Vector2 ScreenToRectPoint(Vector2 pt) {
		return new Vector2(pt.x, Screen.height - pt.y);
	}
}

}
