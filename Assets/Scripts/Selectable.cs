using UnityEngine;
using System.Collections;
using System;
using Colony.Input;

namespace Colony {

public class Selectable : MonoBehaviour {
	// The sprite of the selection wheel
	public GameObject selectionSprite;
	// This object can be selected by dragging
	public bool dragSelectable = true;

	public event Action OnSelect, OnDeselect;

	private bool selected;

	public bool IsSelected {
		get { return selected; }
	}

	void OnDestroy() {
		MouseActions.Instance.RemoveSelected(this);
	}

	public void Select() {
		selectionSprite.SetActive(selected = true);
		if (OnSelect != null)
			OnSelect();
	}

	public void Deselect() {
		selectionSprite.SetActive(selected = false);
		if (OnDeselect != null)
			OnDeselect();
	}

	public bool SelectToggle() {
		if (selected) Deselect();
		else Select();
		return selected;
	}
}

}
