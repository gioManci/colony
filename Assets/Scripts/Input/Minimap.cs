using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace Colony.Input {

public class Minimap : MonoBehaviour, IPointerClickHandler {

	public Camera MMCamera;
	public Texture MMTexture;

	public void OnPointerClick(PointerEventData data) {
		moveCameraToClick(data.position);
	}

	private void moveCameraToClick(Vector3 pos) {
		// Convert main screen coordinates to minimap relative
		// screen coordinates: x_rel_mm = x_orig - minimap.x
		Vector3 mmPos = pos - (Vector3)gameObject.transform.position;
		RaycastHit hit;
		float mmRatio = 2f * MMCamera.orthographicSize / MMTexture.width;
		Camera.main.transform.Translate(mmPos * mmRatio);
	}
}

}