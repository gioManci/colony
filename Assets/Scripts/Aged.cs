using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Colony {

[RequireComponent(typeof(Selectable))]
public class Aged : MonoBehaviour {
	public float Lifespan;

	public float Age { set; get; }

	public bool DestroyOnExpire = true;
	public bool Active = true;

	private Image ageBar;

	void Start() {
		Age = Lifespan;
		ageBar = GetComponent<Selectable>().SelectionSprite.transform.GetChild(0).gameObject.GetComponent<Image>();
		Debug.Assert(ageBar != null, "Age bar is null!");
	}

	void Update() {
		if (!Active)
			return;
		Age -= Time.deltaTime;
		if (Age < 0) {
			if (DestroyOnExpire) {
				// Add code to handle destruction
				EntityManager.Instance.DestroyEntity(gameObject);
			}
		} else {
			updateAgeBar(Age / Lifespan);
		}
	}

	private void updateAgeBar(float percentage) {
		ageBar.fillAmount = percentage;
	}
}

}