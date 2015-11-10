using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {
	// The sprite of the selection wheel
	public GameObject selectionSprite;
	private bool selected;

	public bool IsSelected {
		get {
			return selected;
		}
	}

	void OnMouseUp() {
		Debug.Log(gameObject + " was selected");
		selectionSprite.SetActive(selected = true);
	}

	void Deselect() {
		selectionSprite.SetActive(selected = false);
	}
}