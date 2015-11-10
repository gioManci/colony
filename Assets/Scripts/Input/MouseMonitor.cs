using UnityEngine;
using System.Collections;
using System;
using InputEvents;

public class MouseMonitor : MonoBehaviour {
	enum MouseBtn : int { Left = 0, Right = 1, Middle = 2 };

	private const float MinDragSpan = 2f;

	private bool dragging = false;
	private Vector2 dragStartPos;
	private GameObject target;

	public static event Action<Click> OnLeftClick, OnRightClick;
	public static event Action<Drag> OnDrag;

	// Make this class a singleton
	public static MouseMonitor Instance { get; private set; }

	private MouseMonitor() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}

	void Update() {
		if (Input.GetMouseButtonDown((int)MouseBtn.Left)) {
			dragging = true;
			dragStartPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

		} else if (dragging && Input.GetMouseButtonUp((int)MouseBtn.Left)) {
			dragging = false;
			var dragEndPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var dragSpan = (dragEndPos - dragStartPos).magnitude;
			Debug.Log("dragSpan = " + dragSpan);
			if (dragSpan < MinDragSpan && OnLeftClick != null)
				OnLeftClick(new Click(dragStartPos));
			else if (OnDrag != null)
				OnDrag(new Drag(dragStartPos, dragEndPos));

		} else if (Input.GetMouseButtonDown((int)MouseBtn.Right)) {
			/// DEBUG
			Debug.Log("Move");
			if (target != null)
				GameObject.Destroy(target);
			target = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			target.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
			target.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			///

			if (OnRightClick != null)
				OnRightClick(new Click(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
		}
	}
}