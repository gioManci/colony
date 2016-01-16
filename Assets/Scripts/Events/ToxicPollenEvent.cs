using UnityEngine;
using System.Collections;

namespace Colony.Events {

public class ToxicPollenEvent : Event {

	public float MinPercBeesInvolved = 0.1f;
	public float MaxPercBeesInvolved = 0.5f;

	public ToxicPollenEvent() : base() {
		IsImmediate = true;
		Level = 2;
	}

	public override string Happen() {
		float perc = Random.Range(MinPercBeesInvolved, MaxPercBeesInvolved);
		int nBees = Mathf.Clamp((int)(perc * EntityManager.Instance.Bees.Count), 1, EntityManager.Instance.Bees.Count);

		// Shuffle the list of bees to kill random ones
		foreach (GameObject beeObj in EntityManager.Instance.GetRandomBees(nBees)) {
			var aged = beeObj.GetComponent<Aged>();
			aged.Age -= (int)(Random.Range(0.2f, 0.8f) * aged.Lifespan);
		}

		return "The pollen of the flowers was intoxicated by pollution!" +
			"\r\n" + nBees + " of our bees had their lifespan decreased!";
	}
}

}