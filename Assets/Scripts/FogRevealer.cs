using UnityEngine;
using System.Collections;

namespace Colony {

// An unit which unravels the Fog of War
public class FogRevealer : MonoBehaviour {

	private const float REVEAL_DELAY = 2f;

	public float RevealRadius = 5f;

	private FogOfWar fow;
	private float count = REVEAL_DELAY;

	void Start() {
		fow = GameObject.FindObjectOfType<FogOfWar>();
	}

	// Update is called once per frame
	void Update() {
		count -= Time.deltaTime;
		if (count <= 0) {
			fow.Reveal(transform.position, RevealRadius);
			count = REVEAL_DELAY;
		}
	}
}

}