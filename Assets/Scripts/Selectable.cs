using UnityEngine;
using System.Collections;
using System;
using Colony.Input;

namespace Colony {

public class Selectable : MonoBehaviour {
	// The sprite of the selection wheel
	public GameObject SelectionSprite;
	// The sprite viewed on the minimap, if any
	public GameObject MinimapSprite;
	// This object can be selected by dragging
	public bool DragSelectable = true;
	public bool AlwaysShowSelectionSprite;

	public event Action OnSelect, OnDeselect;

	private bool selected;
	private SpriteRenderer minimapSpriteRenderer;
	private Color origMinimapSpriteColor;

	void Start() {
		if (MinimapSprite != null) {
			minimapSpriteRenderer = MinimapSprite.GetComponent<SpriteRenderer>();
			origMinimapSpriteColor = minimapSpriteRenderer.color;
		}
	}

	public bool IsSelected {
		get { return selected; }
	}

	void OnDestroy() {
		MouseActions.Instance.RemoveSelected(this);
	}

	public void Select() {
		SelectionSprite.SetActive(selected = true);
		if (MinimapSprite != null) {
			minimapSpriteRenderer.color = Color.white;
		}
		if (OnSelect != null)
			OnSelect();
	}

	public void Deselect() {
		selected = false;

		if (!AlwaysShowSelectionSprite)
			SelectionSprite.SetActive(false);
		
		if (MinimapSprite != null)
			minimapSpriteRenderer.color = origMinimapSpriteColor;
		
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
