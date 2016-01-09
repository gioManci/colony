using UnityEngine;
using System.Collections;
using Colony;
using Colony.UI;

public class Cell : MonoBehaviour {

	public enum State { Storage, CreateEgg, Refine };

	public enum RefinedResource {
		None,
		Honey,
		RoyalJelly
	}

	public GameObject eggSprite, storageSprite, refineSprite;

	private State state = State.Storage;
	private RefinedResource refined;

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
                    sel.OnSelect += () => UIController.Instance.SetBPRefining(CellState == State.Refine);
                    sel.OnDeselect += () => UIController.Instance.SetBottomPanel(UIController.BPType.None);
                }
		//aged = gameObject.GetComponent<Aged>();
		//aged.DestroyOnExpire = false;
	}

	public void CreateEgg() {
		storageSprite.SetActive(false);
		refineSprite.SetActive(false);
		eggSprite.SetActive(true);
		state = State.CreateEgg;
	}

	public void UseAsStorage() {
		storageSprite.SetActive(true);
		refineSprite.SetActive(false);
		eggSprite.SetActive(false);
		state = State.Storage;
	}

	public void Refine(RefinedResource what) {
		if (what == RefinedResource.None) {
			UseAsStorage();
			return;
		}
		storageSprite.SetActive(false);
		refineSprite.SetActive(true);
		eggSprite.SetActive(false);
		state = State.Refine;
		refined = what;
	}
}
