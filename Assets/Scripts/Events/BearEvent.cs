using UnityEngine;
using System.Collections.Generic;
using Colony;

namespace Colony.Events {

class BearEvent : Event {
	public BearEvent() {
		Text = "A bear is approaching your beehive to steal you honey!";
		Happen = consequences;
	}

	private string consequences() {
		// FIXME: when we have specializations, this will
		// depend on number of warrior bees in the hive;
		// for now, just kill a random number of bees.
		int nBees = Mathf.Clamp(0, EntityManager.Instance.Bees.Count,
				(int)Utils.NextGaussian(5f, 1f));

		// Shuffle the list of bees to kill random ones
		var listCopy = new List<GameObject>(EntityManager.Instance.Bees);
		listCopy.Sort((x, y) => 1 - 2 * Random.Range(0, 1));
		foreach (GameObject bee in listCopy.GetRange(0, nBees))
			EntityManager.Instance.DestroyBee(bee);

		return "You lost " + nBees.ToString() + " bees!";
	}
}

}
