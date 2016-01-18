using UnityEngine;
using System.Collections;
using Colony;
using Colony.UI;

public class Cell : MonoBehaviour {

	public enum State { Storage, CreateEgg, Refine };

	public enum RefinedResource {
		None       = 1 << 1,
		Honey      = 1 << 2,
		RoyalJelly = 1 << 3
	}

	public GameObject RefineSprite, RefineHoneySprite, RefineRoyalJellySprite;

	private State state = State.Storage;
	private RefinedResource refined;

	public GameObject Inkeeper { get; set; }

	public State CellState {
		get { return state; }
		set { state = value; }
	}

	public RefinedResource Refined {
		get {
			return state != State.Refine 
				? RefinedResource.None 
				: refined;
		}
		private set { refined = value; }
	}

	// Use this for initialization
	void Start() {
		var sel = gameObject.GetComponent<Selectable>();
                if (sel != null)
                {
                    sel.OnSelect += () => UIController.Instance.SetBPRefining(Refined);
                    sel.OnDeselect += () => UIController.Instance.SetBottomPanel(UIController.BPType.None);
                }
	}

	public void CreateEgg() {
		RefineSprite.SetActive(false);
		state = State.CreateEgg;
	}

	public void UseAsStorage() {
		RefineSprite.SetActive(false);
		state = State.Storage;
	}

	public void Refine(RefinedResource what) {
		if (what == RefinedResource.None) {
			UseAsStorage();
			return;
		}
		RefineSprite.SetActive(true);
		RefineHoneySprite.SetActive((what & RefinedResource.Honey) != 0);
		RefineRoyalJellySprite.SetActive((what & RefinedResource.RoyalJelly) != 0);
		state = State.Refine;
		refined = what;
	}
}