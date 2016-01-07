using UnityEngine;
using System.Collections;
using Colony;
using Colony.UI;

public class Cell : MonoBehaviour {

	public enum State { Storage, CreateEgg, Refine };

	public GameObject eggSprite, storageSprite, refineSprite;

	private State state = State.Storage;

	public State CellState {
		get { return state; }
		set { state = value; }
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

	public void Refine() {
		storageSprite.SetActive(false);
		refineSprite.SetActive(true);
		eggSprite.SetActive(false);
		state = State.Refine;
	}
}
