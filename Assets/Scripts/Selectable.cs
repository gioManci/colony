using UnityEngine;
using System.Collections;
using Colony.Input;

namespace Colony {

public class Selectable : MonoBehaviour {
	// The sprite of the selection wheel
	public GameObject selectionSprite;
	private bool selected;

	public bool IsSelected {
		get { return selected; }
	}

	void Start() {
		MouseActions.Instance.AddSelectable(this);
	}

	void OnDestroy() {
		MouseActions.Instance.RemoveSelectable(this);
	}

	public void Select() {
		selectionSprite.SetActive(selected = true);
	}

	public void Deselect() {
		selectionSprite.SetActive(selected = false);
	}

	public bool SelectToggle() {
		if (selected) Deselect();
		else Select();
		return selected;
	}
}

}
