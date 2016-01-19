using UnityEngine;
using System.Collections;

namespace Colony.Events {

public class ToxicPollenEvent : Event {

	public ToxicPollenEvent() : base() {
		IsImmediate = true;
		Image = spawner.ToxicPollenSprite;
		Level = 2;
	}

	public override string Happen() {
		float perc = Random.Range(spawner.ToxicPollenMinPercBeesInvolved, spawner.ToxicPollenMaxPercBeesInvolved);
		int nBees = Mathf.Clamp((int)(perc * EntityManager.Instance.Bees.Count), 1, EntityManager.Instance.Bees.Count);

		// Shuffle the list of bees to kill random ones
		foreach (GameObject beeObj in EntityManager.Instance.GetRandomBees(nBees)) {
			var aged = beeObj.GetComponent<Aged>();
			aged.Age -= (int)(Random.Range(spawner.ToxicPollenMinLifespanDecreased,
				spawner.ToxicPollenMaxLifespanDecreased) * aged.Lifespan);
		}

		return "The pollen of the flowers was intoxicated by pollution!" +
			"\r\n" + nBees + " of our bees had their lifespan decreased!";
	}
}

}