using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace Colony.Input {

public class Minimap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public Camera MMCamera;
	public Texture MMTexture;

	public bool HasMouseOver { get; private set; }

	private Boundaries bounds;

	void Start() {
		bounds = GameObject.FindWithTag("Ground").GetComponent<Boundaries>();
	}

	public void OnPointerEnter(PointerEventData data) {
		HasMouseOver = true;
	}

	public void OnPointerExit(PointerEventData data) {
		HasMouseOver = false;
	}

	public void OnPointerClick(PointerEventData data) {
		switch (data.button) {
		case PointerEventData.InputButton.Left:
			moveCameraToClick(data.position);
			break;
		case PointerEventData.InputButton.Right:
			moveUnitsToClick(data.position);
			break;
		}
	}

	private void moveCameraToClick(Vector3 pos) {
		var cpos = convertCoords(pos);
		if (cpos != null)
			Camera.main.transform.Translate(cpos.Value - Camera.main.transform.position);
	}

	private void moveUnitsToClick(Vector3 pos) {
		var cpos = convertCoords(pos);
		if (cpos != null) {
			// Move each selected moveable object
			foreach (Controllable obj in MouseActions.Instance.GetSelected<Controllable>()) {
				if (obj.canMove) {
					obj.DoMove(cpos.Value);
				}
			}
		}
	}
		
	private Vector3? convertCoords(Vector3 pos) {
		// Convert main screen coordinates to minimap relative
		// screen coordinates: x_rel_mm = x_orig - minimap.x
		Vector3 mmPos = pos - (Vector3)gameObject.transform.position;
		float mmRatio = 2f * MMCamera.orthographicSize / MMTexture.width;

		// Don't go past world boundaries
		var newPos = Camera.main.transform.position + mmPos * mmRatio;
		if (newPos.x < -bounds.WorldSize / 2f || newPos.x > bounds.WorldSize / 2f
		    || newPos.y < -bounds.WorldSize / 2f || newPos.y > bounds.WorldSize / 2f)
			return null;

		return newPos;
	}
}

}