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

	public void Select() {
		selectionSprite.SetActive(selected = true);
	}

	public void Deselect() {
		selectionSprite.SetActive(selected = false);
	}
}